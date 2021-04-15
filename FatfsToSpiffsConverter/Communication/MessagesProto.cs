using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FatfsToSpiffsConverter.Communication
{
    enum ErrorList{
        ERROR_WRITE_FILE = 1,
    }

    enum MessagesId
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
    public struct MessageError{
        public UInt32 codeError;
    }

    public class MessagesProto: IProto
    {
        private ComPortProcess comPort;
        private static MessagesProto m_instance;
        private static readonly object m_lock = new object();


        private MessagesProto()
        {
            comPort = new ComPortProcess();
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

        public bool Send(byte[] data)
        {
            throw new NotImplementedException();
        }

        public bool SendMessageError(MessageError msg)
        {

            return true;
        }
    }
}
