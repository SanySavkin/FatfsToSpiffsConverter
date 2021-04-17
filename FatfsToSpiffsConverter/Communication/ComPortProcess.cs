using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FatfsToSpiffsConverter.Communication
{

    class ComPortProcess
    {
        private CancellationTokenSource m_source = new CancellationTokenSource();
        private static object m_lock = new object();
        private Task m_currentThread;
        public ConcurrentQueue<byte[]> messagesQueueTx = new ConcurrentQueue<byte[]>();
        private MessagesProto m_msgProto;
        private SerialPort port = null;
        private byte[] rxBuffer = new byte[2048];

        public ComPortProcess(MessagesProto proto)
        {
            m_msgProto = proto;
            Start();
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

        private void PortInit()
        {
            port = new SerialPort
            {
                PortName = "COM3",
                BaudRate = 115200
            };
            try
            {
                Console.WriteLine("Try opening port: " + port.PortName);
                port.Open();
                Console.WriteLine("port opened: " + port.PortName);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void Processing(CancellationToken token)
        {
            while (!token.IsCancellationRequested)
            {
                try
                {
                    PortInit();
                    ClearMessageQueue();
                    if (port != null)
                    {
                        using (port)
                        {
                            try
                            {
                                //MessageWriteFolder msg = new MessageWriteFolder();
                                //msg.srcPath = "Indoor/snd/en";
                                //msg.dstPath = "snd/en";
                                while (port.IsOpen)
                                {
                                    ReceiveDatFromPort();
                                    SendDataToPort();
                                    Thread.Sleep(2);
                                }
                            }
                            catch (IOException ex)
                            {
                                Console.WriteLine(ex);
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex);
                            }
                        }
                    }
                }
                catch (IOException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                Thread.Sleep(2000);
            }
        }

        private void SendDataToPort()
        {
            if (messagesQueueTx.Count != 0)
            {
                byte[] data;
                if(messagesQueueTx.TryDequeue(out data))
                {
                    port.Write(data, 0, data.Length);
                }
            }
        }

        private void ReceiveDatFromPort()
        {
            while(port.BytesToRead != 0)
            {
                var d = port.ReadByte();
                m_msgProto.ReceiveData(Convert.ToByte(d));
            }
        }

        public bool Send(byte[] data)
        {
            messagesQueueTx.Enqueue(data);
            return true;
        }

        //public byte[] GetPacket()
        //{
        //    byte[] data;
        //    if (messagesQueueRx.TryPeek(out data))
        //    {
        //        uint len = BitConverter.ToUInt32(data, 0);
        //        if (messagesQueueRx.Count * portReadBytesCount >= len + 4)
        //        {
        //            if (messagesQueueRx.TryDequeue(out data)}
        //        }
        //    }
        //    return data;
        //}

        private void ClearMessageQueue()
        {
            while(messagesQueueTx.Count != 0)
            {
                byte[] data;
                messagesQueueTx.TryDequeue(out data);
            }
        }
    }
}
