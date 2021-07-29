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
        private static readonly string processing = "Подождите!";
        private static readonly string errorString = "Ошибка";
        private static readonly string warningNoConnection = "Сначала выберите порт и дождитесь сообщения \n\r" + connected;
        private static readonly string imagesFolderName = "images/";

        private static MainForm m_mainForm;

        public static bool isStartedWrite = false;
        public static bool isStartedCreateImage = false;
        public static bool isSentWriteFolderMessage = false;
        public static bool isConnectedDevice = false;

        private static System.Timers.Timer timerTimeoutError = new System.Timers.Timer();
        private static System.Timers.Timer timerProgressBar = new System.Timers.Timer();
        private static System.Timers.Timer timerWaitConnect = new System.Timers.Timer();


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
            if (isStartedWrite)
            {
                StopFlash(ErrorList.GLOB_ERR_TIMEOUT);
                isStartedWrite = false;
            }
            else if (isStartedCreateImage)
            {
                StopCreateImage(ErrorList.GLOB_ERR_TIMEOUT);
            }


        }

        private static void TimerWaitConnectElapsedClbck(object sender, ElapsedEventArgs e)
        {
            MessagesProto.Instance.SendMessagePing(new MessagePing() { senderId = serverId });
        }

        delegate void IncrementProgressDelegate();

        private static void IncrementProgress()
        {
            if (isStartedWrite)
                m_mainForm.progressBar1.Increment(1);
            else if (isStartedCreateImage)
                m_mainForm.progressBar2.Increment(1);
        }

        private static void ProgressBarTimerElapsedClbck(object sender, ElapsedEventArgs e)
        {
            if (isStartedWrite)
            {
                if (m_mainForm.progressBar1.Value < 95)
                    UpdateUiProgress();
            }
            else if (isStartedCreateImage)
            {
                if (m_mainForm.progressBar2.Value < 95)
                    UpdateUiProgress();
            }
           
        }

        private static void SetMessageTextFlashTab(string text, Color c)
        {
            SetControlPropertyThreadSafe(m_mainForm.label_flashTabMessage, "ForeColor", c);
            SetControlPropertyThreadSafe(m_mainForm.label_flashTabMessage, "Text", text);
        }
        
        private static void SetMessageTextImageTab(string text, Color c)
        {
            SetControlPropertyThreadSafe(m_mainForm.label_ImageTabMessageText, "ForeColor", c);
            SetControlPropertyThreadSafe(m_mainForm.label_ImageTabMessageText, "Text", text);
        }

        public static void StartFlash()
        {
            if (isConnectedDevice)
            {
                Timer.StartTimer(TimeoutTimerElapsedClbck, out timerTimeoutError, 120000);

                var messageText = m_mainForm.checkBox_FormatingSpiffs.Checked && m_mainForm.checkBox_useSpiffs.Checked ? formatingString : processing;

                SetMessageTextFlashTab(messageText, Color.DarkBlue);
                SetControlPropertyThreadSafe(m_mainForm.button_StartFlash, "BackColor", Color.LightGray);
                SetControlPropertyThreadSafe(m_mainForm.progressBar1, "Value", 2);
                SetControlPropertyThreadSafe(m_mainForm.button_StartFlash, "Enabled", false);
                SetControlPropertyThreadSafe(m_mainForm.comboBox_ComPorts, "Enabled", false);
                SetControlPropertyThreadSafe(m_mainForm.tabPage3, "Enabled", false);
                SetControlPropertyThreadSafe(m_mainForm.tabPage2, "Enabled", false);

                MainSettings mainSet = Settings.MnSettings;
                MessageSettings msg;
                msg.flashSize = mainSet.flashSize;
                msg.spiffsAddr = mainSet.spiffsAddress;
                msg.eraseSize = mainSet.eraseSize;
                msg.pageSize = mainSet.logPageSize;
                msg.logBlockSize = mainSet.blockSize;
                msg.allowFormating = Convert.ToUInt32(mainSet.allowFormating);

                isStartedWrite = true;
                if (mainSet.useSpiffs)
                {
                    MessagesProto.Instance.SendMessageSettings(msg);
                }
                else
                {
                    Timer.StartTimer(ProgressBarTimerElapsedClbck, out timerProgressBar, 200);
                    MessageWriteFolder msg2;
                    msg2.dstPath = " ";
                    msg2.srcPath = mainSet.pathFatfs;
                    MessagesProto.Instance.SendMessageWriteFolder(msg2);
                    isSentWriteFolderMessage = true;
                }
            }
            else
            {
                SetMessageTextFlashTab(warningNoConnection, Color.OrangeRed);
            }
        }

        public static void StartCreateImage()
        {
            if (isConnectedDevice)
            {
                isStartedCreateImage = true;
                Timer.StartTimer(TimeoutTimerElapsedClbck, out timerTimeoutError, 120000);
                Timer.StartTimer(ProgressBarTimerElapsedClbck, out timerProgressBar, 200);

                SetControlPropertyThreadSafe(m_mainForm.progressBar2, "Value", 2);
                SetControlPropertyThreadSafe(m_mainForm.tabPage1, "Enabled", false);
                SetControlPropertyThreadSafe(m_mainForm.tabPage2, "Enabled", false);
                SetControlPropertyThreadSafe(m_mainForm.button_imageTabStart, "Enabled", false);
                SetControlPropertyThreadSafe(m_mainForm.button_imageTabStart, "BackColor", Color.LightGray);
                SetControlPropertyThreadSafe(m_mainForm.textBox_imageTabPath, "Enabled", false);


                SetControlPropertyThreadSafe(m_mainForm.label_ImageTabMessageText, "ForeColor", Color.DarkBlue);
                SetControlPropertyThreadSafe(m_mainForm.label_ImageTabMessageText, "Text", processing);
                MessagesProto.Instance.SendMessageCreateImage(new MessageCreateImage() { 
                    fileName = imagesFolderName + m_mainForm.textBox_imageTabPath.Text,
                    fileSize = MemoryChip.GetSize((FlashTypes)m_mainForm.comboBox_FlashType.SelectedIndex)
                });
            }
            else
            {
                SetControlPropertyThreadSafe(m_mainForm.label_ImageTabMessageText, "ForeColor", Color.OrangeRed);
                SetControlPropertyThreadSafe(m_mainForm.label_ImageTabMessageText, "Text", warningNoConnection);
            }
        }

        public static void StopCreateImage(ErrorList result)
        {
            timerTimeoutError.Stop();
            timerProgressBar.Stop();
            UpdateUIMessageText(result);

            var progBarVal = result == ErrorList.GLOB_ERR_NONE ? 100 : 0;

            SetControlPropertyThreadSafe(m_mainForm.progressBar2, "Value", progBarVal);
            SetControlPropertyThreadSafe(m_mainForm.textBox_imageTabPath, "Enabled", true); ;
            SetControlPropertyThreadSafe(m_mainForm.button_imageTabStart, "BackColor", Color.SpringGreen);
            SetControlPropertyThreadSafe(m_mainForm.button_imageTabStart, "Enabled", true);
            SetControlPropertyThreadSafe(m_mainForm.tabPage1, "Enabled", true);
            SetControlPropertyThreadSafe(m_mainForm.tabPage2, "Enabled", true);

            isStartedCreateImage = false;
        }

        public static void StopFlash(ErrorList result)
        {
            timerTimeoutError.Stop();
            timerProgressBar.Stop();
            UpdateUIMessageText(result);

            var progBarVal = result == ErrorList.GLOB_ERR_NONE ? 100 : 0;
            SetControlPropertyThreadSafe(m_mainForm.button_StartFlash, "BackColor", Color.SpringGreen);
            SetControlPropertyThreadSafe(m_mainForm.progressBar1, "Value", progBarVal);
            SetControlPropertyThreadSafe(m_mainForm.button_StartFlash, "Enabled", true);
            SetControlPropertyThreadSafe(m_mainForm.comboBox_ComPorts, "Enabled", true);
            SetControlPropertyThreadSafe(m_mainForm.tabPage3, "Enabled", true);
            SetControlPropertyThreadSafe(m_mainForm.tabPage2, "Enabled", true);

            isStartedWrite = false;
            isSentWriteFolderMessage = false;
        }       

        public static void Connect()
        {
            MessagesProto.Instance.SendMessagePing(new MessagePing() { senderId = serverId });
            //Timer.StartTimer(TimerWaitConnectElapsedClbck, out timerWaitConnect, 1000);
            SetControlPropertyThreadSafe(m_mainForm.label_flashTabMessage, "Text", " ");
            SetControlPropertyThreadSafe(m_mainForm.label_ImageTabMessageText, "Text", " ");
        }

        public static void Disconnect()
        {
            isConnectedDevice = false;
            UpdateConnectionText(disconnected);
            StopCreateImage(ErrorList.GLOB_ERR_DISCONNECT);
            StopFlash(ErrorList.GLOB_ERR_DISCONNECT);
        }

        private static void UpdateUiProgress()
        {
            IncrementProgressDelegate inc = new IncrementProgressDelegate(IncrementProgress);
            if (isStartedWrite)
                m_mainForm.progressBar1.Invoke(inc);
            else if (isStartedCreateImage)
                m_mainForm.progressBar2.Invoke(inc);
        }

        private static void UpdateUIMessageText(ErrorList code)
        {
            string text;
            Color color = Color.Red;
            switch (code)
            {
                case ErrorList.GLOB_ERR_ALLOCATE_MEMORY:
                    text = "Ошибка выделения памяти. \r\n Переподключите кабель USB и попробуйте снова";
                    break;
                case ErrorList.GLOB_ERR_FILE_WRITE:
                    text = "Ошибка записи в файловую систему Spiffs. \r\n Убедитесь что достаточно памяти для копирования всех файлов";
                    break;
                case ErrorList.GLOB_ERR_FLASH_PARAMETERS:
                    text = "Ошибка параметров для Spiffs. \r\n Размер флеш-памяти не соответсвует настройкам в текущем профиле";
                    break;
                case ErrorList.GLOB_ERR_HASH:
                    text = "Ошибка контрольной суммы. Попробуйте еще раз.\r\n Убедитесь в хорошем электрическом контакте. Используйте шлейф минимальной длинны";
                    break;
                case ErrorList.GLOB_ERR_LONG_FILE_NAME:
                    text = "Имена файлов больше 32 символов не поддерживаются";
                    break;
                case ErrorList.GLOB_ERR_SPIFFS_FORMATING:
                    text = "Ошибка форматирования.\r\n Попробуйте еще раз";
                    break;
                case ErrorList.GLOB_ERR_SPIFFS_MOUNTING:
                    text = "Ошибка монтирования файловой системы.\r\n Попробуйте еще раз";
                    break;
                case ErrorList.GLOB_ERR_NONE:
                    color = Color.Green;
                    text = successString;
                    break;
                case ErrorList.GLOB_ERR_TIMEOUT:
                    text = "Время ожидания истекло. \r\n Переподключите кабель USB и попробуйте снова";
                    break;
                case ErrorList.GLOB_ERR_FLASH_NOT_ANSWER:
                    text = "Микросхема флеш-памяти не отвечает. \r\n  Убедитесь в хорошем электрическом контакте";
                    break;
                case ErrorList.GLOB_ERR_OPEN_FILE:
                    text = "Файл на SD карте не найден";
                    break;
                case ErrorList.GLOB_ERR_FILE_READ:
                    text = "Ошибка чтения файла. Попробуйте ещё раз";
                    break;
                case ErrorList.GLOB_ERR_DISCONNECT:
                    text = "Соединение было потеряно";
                    break;
                case ErrorList.GLOB_ERR_IMAGE_NOT_CORRECT:
                    text = "Файл образа некоректен";
                    break;
                case ErrorList.GLOB_ERR_FILES_NOT_FOUNDED:
                    text = "Ни один файл не найден. Проверьте наличие SD карты или наличие файлов";
                    break;
                default:
                    text = errorString;
                    break;
            }
            if (isStartedWrite)
                SetMessageTextFlashTab(text, color);
            else if (isStartedCreateImage)
                SetMessageTextImageTab(text, color);
        }

        public static void OnMessageErrorReceived(MessageError msg)
        {
            if (isStartedWrite)
            {
                if ((ErrorList)msg.codeError == ErrorList.GLOB_ERR_NONE)
                {
                    if (!isSentWriteFolderMessage)
                    {
                        MainSettings mainSet = Settings.MnSettings;
                        MessageWriteFolder msg2;
                        msg2.dstPath = mainSet.pathSpiffs;
                        msg2.srcPath = mainSet.pathFatfs;
                        MessagesProto.Instance.SendMessageWriteFolder(msg2);
                        isSentWriteFolderMessage = true;
                        SetMessageTextFlashTab(processing, Color.DarkBlue);
                        Timer.StartTimer(ProgressBarTimerElapsedClbck, out timerProgressBar, 200);
                    }
                    else
                    {
                        StopFlash((ErrorList)msg.codeError);
                    }
                }
                else
                {
                    StopFlash((ErrorList)msg.codeError);
                }
            }
            else if (isStartedCreateImage)
            {
                StopCreateImage((ErrorList)msg.codeError);
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
                timerWaitConnect.Stop();
                isConnectedDevice = true;
                UpdateConnectionText(connected);
            }
        }

        private delegate void SetControlPropertyThreadSafeDelegate(Control control, string propertyName, object propertyValue);

        public static void SetControlPropertyThreadSafe(Control control, string propertyName, object propertyValue)
        {
            if (control.InvokeRequired)
            {
                control.Invoke(new SetControlPropertyThreadSafeDelegate(SetControlPropertyThreadSafe),
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
