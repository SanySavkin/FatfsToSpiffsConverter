using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace FatfsToSpiffsConverter.Communication
{
    public enum ErrorList{
        ERROR_WRITE_FILE = 1,
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
        private static MessagesProto m_instance;
        private static readonly object m_lock = new object();


        private MessagesProto()
        {
            comPort = new ComPortProcess(this);
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
            comPort.Send(data);
            return true;
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
            //if (BitConverter.IsLittleEndian)
            //    Array.Reverse(bytes);
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

        //private object FromBytes(byte[] arr)
        //{
        //    CIFSPacket str = new CIFSPacket();

        //    int size = Marshal.SizeOf(str);
        //    IntPtr ptr = Marshal.AllocHGlobal(size);

        //    Marshal.Copy(arr, 0, ptr, size);

        //    str = (CIFSPacket)Marshal.PtrToStructure(ptr, str.GetType());
        //    Marshal.FreeHGlobal(ptr);

        //    return str;
        //}
    }
}
