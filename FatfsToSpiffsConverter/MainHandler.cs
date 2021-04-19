using FatfsToSpiffsConverter.Communication;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FatfsToSpiffsConverter
{
    public static class MainHandler
    {

        private static MainForm m_mainForm;

        public static bool isStarted = false;
        public static bool isSentWriteFolder = false;


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
            

        private static void UpdateUiProgress(int value)
        {
            SetControlPropertyThreadSafe(m_mainForm.progressBar1, "Value", value);
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
                            //msg2.srcPath ="test/snd/en";
                            //msg2.dstPath = "snd/en";
                            MessagesProto.Instance.SendMessageWriteFolder(msg2);
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
