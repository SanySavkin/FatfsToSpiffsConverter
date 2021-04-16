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
        private ConcurrentQueue<byte[]> messagesQueue = new ConcurrentQueue<byte[]>();
        private MessagesProto m_msgProto;
        private SerialPort port = null;

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
                    if (port != null)
                    {
                        using (port)
                        {
                            try
                            {
                                MessageWriteFolder msg = new MessageWriteFolder();
                                msg.srcPath = "Indoor/snd/en";
                                msg.dstPath = "snd/en";
                                while (port.IsOpen)
                                {
                                    m_msgProto.SendMessageWriteFolder(msg);
                                    if (port.BytesToRead > 0)
                                    {
                                        Console.WriteLine(port.ReadExisting());
                                    }
                                    if (messagesQueue.Count != 0)
                                    {
                                        byte[] data;
                                        messagesQueue.TryDequeue(out data);
                                        port.Write(data, 0, data.Length);
                                    }
                                    Thread.Sleep(4000);
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

        public bool Send(byte[] data)
        {
            messagesQueue.Enqueue(data);
            return true;
        }
    }
}
