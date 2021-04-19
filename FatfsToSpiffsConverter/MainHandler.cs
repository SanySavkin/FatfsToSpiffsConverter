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

        private static MainForm m_mainForm;

        public static bool isStarted = false;
        public static bool isSentWriteFolder = false;

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
            UpdateUiProgress();
        }


        public static void StartFlash()
        {
            Timer.StartTimer(TimeoutTimerElapsedClbck, out timerTimeoutError, 80000);
            Timer.StartTimer(ProgressBarTimerElapsedClbck, out timerProgressBar, 200);

            SetControlPropertyThreadSafe(m_mainForm.progressBar1, "Value", 0);
            SetControlPropertyThreadSafe(m_mainForm.label_Message, "Text", "Processing");
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

        public static void StopFlash(ErrorList result)
        {
            timerTimeoutError.Stop();
            timerProgressBar.Stop();
            UpdateUIMessageText(result);

            SetControlPropertyThreadSafe(m_mainForm.progressBar1, "Value", 100);
            SetControlPropertyThreadSafe(m_mainForm.button_StartFlash, "Enabled", true);
            SetControlPropertyThreadSafe(m_mainForm.comboBox_ComPorts, "Enabled", true);
            SetControlPropertyThreadSafe(m_mainForm.tabPage2, "Enabled", true);
        }

        private static void UpdateUiProgress()
        {
            IncrementProgressDelegate inc = new IncrementProgressDelegate(IncrementProgress);
            m_mainForm.progressBar1.Invoke(inc);
        }

        private static void UpdateUIMessageText(ErrorList code)
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
                case ErrorList.GLOB_ERR_TIMEOUT:
                    text = "Устройство не отвечает";
                    break;
                default:
                    text = "Ошибка";
                    break;
            }
            SetControlPropertyThreadSafe(m_mainForm.label_Message, "Text", text);
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
            Color cl;
            if (text == "Connected")
            {
                cl = Color.SpringGreen;
            }
            else
            {
                cl = Color.Red;
            }
            SetControlPropertyThreadSafe(FormInstance.connectionLabel, "Text", text);
            SetControlPropertyThreadSafe(FormInstance.connectionLabel, "ForeColor", cl);
        }

        public static void OnMessageFlashTypeReceived(MessageFlashType msg)
        {

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
