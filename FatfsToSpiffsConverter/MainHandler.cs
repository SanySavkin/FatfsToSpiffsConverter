using FatfsToSpiffsConverter.Communication;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;

namespace FatfsToSpiffsConverter
{
    public static class MainHandler
    {
        private static readonly uint deviceId = 1;
        private static readonly uint serverId = 0xffff;

        private static readonly string connected = "подключено";
        private static readonly string disconnected = "нет подключения";
        private static readonly string successString = "Готово!";
        private static readonly string formatingString = "Форматирование";
        private static readonly string processing = "Копирование";
        private static readonly string errorString = "Ошибка";

        private static MainForm m_mainForm;

        public static bool isStarted = false;
        public static bool isSentWriteFolder = false;
        public static bool isConnectedDevice = false;

        private static System.Timers.Timer timerTimeoutError;
        private static System.Timers.Timer timerProgressBar;


        public static MainForm FormInstance {
            set
            {
                m_mainForm = value;
            }
            get
            {
                return m_mainForm;
            }
        }

        private static void TimeoutTimerElapsedClbck(object sender, ElapsedEventArgs e)
        {
            StopFlash(ErrorList.GLOB_ERR_TIMEOUT);
        }

        delegate void IncrementProgressDelegate();

        private static void IncrementProgress()
        {
            m_mainForm.progressBar1.Increment(1);
        }

        private static void ProgressBarTimerElapsedClbck(object sender, ElapsedEventArgs e)
        {
            if(m_mainForm.progressBar1.Value < 95)
                UpdateUiProgress();
        }

        private static void SetMessageText(string text, Color c)
        {
            SetControlPropertyThreadSafe(m_mainForm.label_Message, "ForeColor", c);
            SetControlPropertyThreadSafe(m_mainForm.label_Message, "Text", text);
        }


        public static void StartFlash()
        {
            if (isConnectedDevice)
            {
                Timer.StartTimer(TimeoutTimerElapsedClbck, out timerTimeoutError, 120000);

                var messageText = m_mainForm.checkBox_FormatingSpiffs.Checked ? formatingString : processing;

                SetMessageText(messageText, Color.DarkBlue);
                SetControlPropertyThreadSafe(m_mainForm.button_StartFlash, "BackColor", Color.LightGray);
                SetControlPropertyThreadSafe(m_mainForm.progressBar1, "Value", 2);
                SetControlPropertyThreadSafe(m_mainForm.button_StartFlash, "Enabled", false);
                SetControlPropertyThreadSafe(m_mainForm.comboBox_ComPorts, "Enabled", false);
                SetControlPropertyThreadSafe(m_mainForm.tabPage2, "Enabled", false);

                MainSettings mainSet = Settings.Instance.MnSettings;
                MessageSettings msg;
                msg.flashSize = mainSet.flashSize;
                msg.spiffsAddr = mainSet.spiffsAddress;
                msg.eraseSize = mainSet.eraseSize;
                msg.pageSize = mainSet.logPageSize;
                msg.logBlockSize = mainSet.blockSize;
                msg.allowFormating = Convert.ToUInt32(mainSet.allowFormating);

                isStarted = true;
                MessagesProto.Instance.SendMessageSettings(msg);
            }
            else
            {
                SetMessageText("Сначала выберите порт и дождитесь сообщения \n\r" + connected, Color.OrangeRed);
            }
        }

        public static void StopFlash(ErrorList result)
        {
            timerTimeoutError.Stop();
            timerProgressBar.Stop();
            UpdateUIMessageText(result);

            SetControlPropertyThreadSafe(m_mainForm.button_StartFlash, "BackColor", Color.SpringGreen);
            SetControlPropertyThreadSafe(m_mainForm.progressBar1, "Value", 100);
            SetControlPropertyThreadSafe(m_mainForm.button_StartFlash, "Enabled", true);
            SetControlPropertyThreadSafe(m_mainForm.comboBox_ComPorts, "Enabled", true);
            SetControlPropertyThreadSafe(m_mainForm.tabPage2, "Enabled", true);
        }

        public static void Connect()
        {
            MessagesProto.Instance.SendMessagePing(new MessagePing() { senderId = serverId });
        }

        public static void Disconnect()
        {
            isConnectedDevice = false;
            UpdateConnectionText(disconnected);
        }

        private static void UpdateUiProgress()
        {
            IncrementProgressDelegate inc = new IncrementProgressDelegate(IncrementProgress);
            m_mainForm.progressBar1.Invoke(inc);
        }

        private static void UpdateUIMessageText(ErrorList code)
        {
            string text;
            Color color = Color.Red;
            switch (code)
            {
                case ErrorList.GLOB_ERR_ALLOCATE_MEMORY:
                    text = "Ошибка выделения памяти. \r\n Переподключите кабель USB и попробуйте снова.";
                    break;
                case ErrorList.GLOB_ERR_FILE_WRITE:
                    text = "Ошибка записи в файловую систему Spiffs. \r\n Убедитесь что достаточно памяти для копирования всех файлов";
                    break;
                case ErrorList.GLOB_ERR_FLASH_PARAMETERS:
                    text = "Ошибка параметров для Spiffs. \r\n Размер флеш-памяти не соответсвует настройкам в текущем профиле";
                    break;
                case ErrorList.GLOB_ERR_HASH:
                    text = "Ошибка контрольной суммы. Попробуйте еще раз.\r\n Убедитесь в отсутсвии электромагнитных помех. Используйте шлейф минимальной длинны";
                    break;
                case ErrorList.GLOB_ERR_LONG_FILE_NAME:
                    text = "Имена файлов больше 32 символов не поддерживаются.";
                    break;
                case ErrorList.GLOB_ERR_SPIFFS_FORMATING:
                    text = "Ошибка форматирования.\r\n Попробуйте еще раз.";
                    break;
                case ErrorList.GLOB_ERR_SPIFFS_MOUNTING:
                    text = "Ошибка монтирования файловой системы.\r\n Попробуйте еще раз. ";
                    break;
                case ErrorList.GLOB_ERR_NONE:
                    color = Color.Green;
                    text = successString;
                    break;
                case ErrorList.GLOB_ERR_TIMEOUT:
                    text = "Время ожидания истекло. \r\n Переподключите кабель USB и попробуйте снова.";
                    break;
                default:
                    text = errorString;
                    break;
            }
            SetMessageText(text, color);
        }

        public static void OnMessageErrorReceived(MessageError msg)
        {
            if (isStarted)
            {
                if ((ErrorList)msg.codeError == ErrorList.GLOB_ERR_NONE)
                {
                    if (isStarted)
                    {
                        if (!isSentWriteFolder)
                        {
                            MainSettings mainSet = Settings.Instance.MnSettings;
                            MessageWriteFolder msg2;
                            msg2.srcPath = mainSet.pathFatfs;
                            msg2.dstPath = mainSet.pathSpiffs;
                            MessagesProto.Instance.SendMessageWriteFolder(msg2);
                            isSentWriteFolder = true;
                            SetMessageText(processing, Color.DarkBlue);
                            Timer.StartTimer(ProgressBarTimerElapsedClbck, out timerProgressBar, 200);
                        }
                        else
                        {
                            isSentWriteFolder = false;
                            isStarted = false;
                            StopFlash((ErrorList)msg.codeError);
                        }
                    }
                }
                else
                {
                    StopFlash((ErrorList)msg.codeError);
                }
            }
        }

        public static void UpdateConnectionText(string text)
        {
            Color cl = text == connected ? Color.Green : Color.Red;
            SetControlPropertyThreadSafe(FormInstance.connectionLabel, "Text", text);
            SetControlPropertyThreadSafe(FormInstance.connectionLabel, "ForeColor", cl);
        }

        public static void OnMessageFlashTypeReceived(MessageFlashType msg)
        {

        }

        public static void OnMessagePingReceived(MessagePing msg)
        {
            if (msg.senderId == deviceId)
            {
                isConnectedDevice = true;
                UpdateConnectionText(connected);
            }
        }

        private delegate void SetControlPropertyThreadSafeDelegate(Control control, string propertyName, object propertyValue);

        public static void SetControlPropertyThreadSafe(Control control, string propertyName, object propertyValue)
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
}
