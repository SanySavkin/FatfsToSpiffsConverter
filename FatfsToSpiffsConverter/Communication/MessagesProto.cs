using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Timers;
using System.Threading.Tasks;
using System.Threading;
using System.Reflection;
using System.Windows.Forms;

namespace FatfsToSpiffsConverter.Communication
{
    public enum ErrorList{
        GLOB_ERR_NONE = 0,
        GLOB_ERR_ALLOCATE_MEMORY = 1,
        GLOB_ERR_SPIFFS_MOUNTING = 2,
        GLOB_ERR_FLASH_PARAMETERS = 3,
        GLOB_ERR_SPIFFS_FORMATING = 4,
        GLOB_ERR_LONG_FILE_NAME = 5,
        GLOB_ERR_FILE_READ = 6,
        GLOB_ERR_FILE_WRITE = 7,
        GLOB_ERR_NOT_A_FILES = 8,
        GLOB_ERR_HASH = 9,
    }

   public enum MessagesId
    {
        MSG_ID_WRITE_START = 0x12340,
        MSG_ID_WRITE_START_REPLY,
        MSG_ID_WRITE,
        MSG_ID_WRITE_REPLY,
        MSG_ID_WRITE_END,
        MSG_ID_WRITE_END_REPLY,
        MSG_ID_ERROR,
        MSG_ID_GET_FLASH_TYPE,
        MSG_ID_WRITE_FOLDER,
        MSG_ID_SPIFFS_SETTINGS,
    }
    
    public struct MessageError
    {
        public uint codeError;
    }

    public struct MessageFlashType
    {
        public uint typeId;
    }

    public struct MessageSettings
    {
        public uint flashSize;
        public uint spiffsAddr;
        public uint eraseSize;
        public uint pageSize;
        public uint logBlockSize;
        public uint allowFormating;
    }

    public struct MessageWriteFolder
    {
        public string srcPath;
        public string dstPath;
    }   

    public class MessagesProto: IProto
    {
        private ComPortProcess comPort;
        private Task m_currentThread;
        private CancellationTokenSource m_source = new CancellationTokenSource();
        private static MessagesProto m_instance;
        private static readonly object m_lock = new object();
        public ConcurrentQueue<byte> messagesQueueRx = new ConcurrentQueue<byte>();
        private byte[] recivedDataBuffer = new byte[512];
        public int debugCountMessages = 0;
        public int debugCountErrorMessages = 0;
        public bool isStarted = false;
        public bool isSentWriteFolder = false;

        private MainForm m_mainForm;




        private MessagesProto()
        {
            comPort = new ComPortProcess(this);
            Start();
        }

        public static MessagesProto Instance
        {
            get
            {
                if (m_instance == null)  //patern double lock
                    lock (m_lock)
                    {
                        return m_instance ?? (m_instance = new MessagesProto());
                    }
                return m_instance;
            }
        }

        public void SetForm(MainForm f)
        {
            comPort.SetForm(f);
            m_mainForm = f;
        }

        private void Start()
        {
            if (m_currentThread != null) return;
            lock (m_lock)
            {
                if (m_currentThread != null) return;
                var token = m_source.Token;
                var factory = new TaskFactory(token);
                m_currentThread = factory.StartNew(() => Processing(token), token);
            }
        }

        public bool Send(byte[] data)
        {
            comPort.Send(data);
            return true;
        }

        public void ReceiveData(byte b)
        {
           messagesQueueRx.Enqueue(b);
        }

        private void RxControlTimerElapsedClbck(object sender, ElapsedEventArgs e)
        {
            ClearMessageQueue();
        }

        private void Timeouts()
        {
            if (messagesQueueRx.IsEmpty)
            {
                TimeoutsCheck.ResetTimer();
            }
        }

        private void ClearMessageQueue()
        {
            while (messagesQueueRx.Count != 0)
            {
                byte data;
                messagesQueueRx.TryDequeue(out data);
            }
        }

        private void Processing(CancellationToken token)
        {
            TimeoutsCheck.StartTimer(RxControlTimerElapsedClbck);
            while (!token.IsCancellationRequested)
            {
                try
                {
                    MessageDispatcherProcess();
                    Thread.Sleep(2);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        private void MessageDispatcherProcess()
        {
            Timeouts();
            int idx = 0;
            if(messagesQueueRx.Count > 4)
            {
                recivedDataBuffer = messagesQueueRx.ToArray();
                int lenMsg = BitConverter.ToInt32(recivedDataBuffer, 0);
                if(messagesQueueRx.Count >= lenMsg + 4)
                {
                    byte[] msgB = new byte[lenMsg + 4];
                    for(; idx < lenMsg + 4; idx++)
                    {
                        while (!messagesQueueRx.TryDequeue(out msgB[idx])) ;
                    }
                    MessageParse(msgB);
                    TimeoutsCheck.ResetTimer();
                }
               
            }
        }

        private void MessageParse(byte[] b)
        {
            object obj = new object();
            int size = b.Length - 8;
            if (size < 0) return;
            IntPtr ptr = Marshal.AllocHGlobal(size);
            try
            {
                Marshal.Copy(b, 8, ptr, size);
                uint id = BitConverter.ToUInt32(b, 4);
                switch ((MessagesId)id)
                {
                    case MessagesId.MSG_ID_ERROR:
                        obj = (MessageError)Marshal.PtrToStructure(ptr, typeof(MessageError));
                        OnMessageErrorReceived((MessageError)obj);
                        break;
                    case MessagesId.MSG_ID_GET_FLASH_TYPE:
                        obj = (MessageFlashType)Marshal.PtrToStructure(ptr, typeof(MessageFlashType));
                        OnMessageFlashTypeReceived((MessageFlashType)obj);
                        break;
                    default:
                        debugCountErrorMessages++;
                        break;
                }
            }
            finally
            {
                Marshal.FreeHGlobal(ptr);
            }
        }

        /// <summary>
        /// len - размер данных без значения размера всего пакета и msgId
        /// </summary>
        /// <param name="msgId"></param>
        /// <param name="len">размер данных без значения размера всего пакета и msgId</param>
        /// <param name="data"></param>
        /// <returns></returns>
        public byte[] PutInOrder(MessagesId msgId, int len, byte[] data)
        {
            byte[] arr = new byte[len + 8]; // длинна сообщения + sizeof(msgId) + sizeof(len)
            uint lenU = Convert.ToUInt32(len + 4); // lenU - размер сообщения плюс sizeof(msgId)
            uint msgIdU = Convert.ToUInt32(msgId);
            byte[] lenB = BitConverter.GetBytes(lenU);
            byte[] idB = BitConverter.GetBytes(msgIdU);
            lenB.CopyTo(arr, 0);
            idB.CopyTo(arr, 4);
            data.CopyTo(arr, 8);
            return arr;
        }

        public bool SendMessageWriteFolder(MessageWriteFolder msg)
        {
            return ProtoSend(MessagesId.MSG_ID_WRITE_FOLDER, msg);
        }

        public bool SendMessageSettings(MessageSettings msg)
        {
            return ProtoSend(MessagesId.MSG_ID_SPIFFS_SETTINGS, msg);
        }

        private bool ProtoSend(MessagesId id, object obj)
        {
            var b = GetBytes(obj);
            var d = PutInOrder(id, b.Length, b);
            return Send(d);
        }

        private byte[] GetBytes(object obj)
        {
            if (obj is MessageWriteFolder)
            {
                int size = 0;
                var msg = (MessageWriteFolder)obj;
                size = msg.srcPath.Length + msg.dstPath.Length + 2;
                byte[] arr = new byte[size];
                byte[] srcB = Encoding.ASCII.GetBytes(msg.srcPath);
                byte[] dstB = Encoding.ASCII.GetBytes(msg.dstPath);
                byte[] nl = new byte[1];
                nl[0] = 0;
                srcB.CopyTo(arr, 0);
                nl.CopyTo(arr, msg.srcPath.Length);
                dstB.CopyTo(arr, msg.srcPath.Length + 1);
                return arr;
            }
            else
            {
                int size = Marshal.SizeOf(obj);
                byte[] arr = new byte[size];

                IntPtr ptr = Marshal.AllocHGlobal(size);
                try
                {
                    Marshal.StructureToPtr(obj, ptr, true);
                    Marshal.Copy(ptr, arr, 0, size);
                }
                finally
                {
                    Marshal.FreeHGlobal(ptr);
                }

                return arr;
            }
        }

        private void UpdateUiProgress(int value)
        {
            SetControlPropertyThreadSafe(m_mainForm.progressBar1, "Value", value);
        }

        private void UpdateUIMessageText(ErrorList code)
        {
            string text;
            switch (code)
            {
                case ErrorList.GLOB_ERR_ALLOCATE_MEMORY:
                    text = "Ошибка выделения памяти. \r\n Переподключите устройство.";
                    break;
                case ErrorList.GLOB_ERR_FILE_WRITE:
                    text = "Ошибка записи в файловую систему Spiffs. \r\n Убедитесь что достаточно памяти для копирования всех файлов";
                    break;
                case ErrorList.GLOB_ERR_FLASH_PARAMETERS:
                    text = "Ошибка параметров для Spiffs.";
                    break;
                case ErrorList.GLOB_ERR_HASH:
                    text = "Ошибка контрольной суммы. \r\n Попробуйте еще раз.";
                    break;
                case ErrorList.GLOB_ERR_LONG_FILE_NAME:
                    text = "Имя файла слишком большое.";
                    break;
                case ErrorList.GLOB_ERR_SPIFFS_FORMATING:
                    text = "Ошибка форматирования.\r\n Попробуйте еще раз. ";
                    break;
                case ErrorList.GLOB_ERR_SPIFFS_MOUNTING:
                    text = "Ошибка монтирования файловой системы.\r\n Попробуйте еще раз. ";
                    break;
                case ErrorList.GLOB_ERR_NONE:
                    text = "Success!";
                    break;
                default:
                    text = "Ошибка";
                    break;
            }
            SetControlPropertyThreadSafe(m_mainForm.label_Message, "Text", text);
        }

        private void OnMessageErrorReceived(MessageError msg)
        {
            if (isStarted)
            {
                if((ErrorList)msg.codeError == ErrorList.GLOB_ERR_NONE)
                {
                    if (isStarted)
                    {
                        if (!isSentWriteFolder)
                        {
                            MainSettings mainSet = Settings.Instance.MnSettings;
                            MessageWriteFolder msg2;
                            msg2.srcPath = mainSet.pathFatfs;
                            msg2.dstPath = mainSet.pathSpiffs;
                            //msg2.srcPath ="test/snd/en";
                            //msg2.dstPath = "snd/en";
                            SendMessageWriteFolder(msg2);
                            isSentWriteFolder = true;
                            UpdateUiProgress(20);
                        }
                        else
                        {
                            isSentWriteFolder = false;
                            isStarted = false;
                            UpdateUIMessageText((ErrorList)msg.codeError);
                        }
                    }
                }
                else
                {
                    UpdateUIMessageText((ErrorList)msg.codeError);
                }
            }
        }

        private void OnMessageFlashTypeReceived(MessageFlashType msg)
        {

        }

        private delegate void SetControlPropertyThreadSafeDelegate(Control control, string propertyName, object propertyValue);

        public void SetControlPropertyThreadSafe(Control control, string propertyName, object propertyValue)
        {
            if (control.InvokeRequired)
            {
                control.Invoke(new SetControlPropertyThreadSafeDelegate
                (SetControlPropertyThreadSafe),
                new object[] { control, propertyName, propertyValue });
            }
            else
            {
                control.GetType().InvokeMember(
                    propertyName,
                    BindingFlags.SetProperty,
                    null,
                    control,
                    new object[] { propertyValue });
            }
        }

    }

    public static class TimeoutsCheck
    {
        private static System.Timers.Timer aTimer;


        /// <summary>
        /// контроль таймаута приема данных
        /// </summary>
        public static void StartTimer(ElapsedEventHandler handler)
        {
            aTimer = new System.Timers.Timer(2000);
            aTimer.AutoReset = true;
            aTimer.Elapsed += handler;
            aTimer.Start();
        }

        public static void ResetTimer()
        {
            aTimer.Stop();
            aTimer.Start();
        }        
    }
}
