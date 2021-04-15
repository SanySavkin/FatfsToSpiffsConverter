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
        private ConcurrentQueue<string> messagesQueue = new ConcurrentQueue<string>();
        public ComPortProcess()
        {
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

        private SerialPort PortInit()
        {
            SerialPort port = new SerialPort();
            port.PortName = "COM3";
            port.BaudRate = 115200;
            try
            {
                Console.WriteLine("Try opening port: " + port.PortName);
                port.Open();
                Console.WriteLine("port opened: " + port.PortName);
                return port;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }

        private void Processing(CancellationToken token)
        {
            while (!token.IsCancellationRequested)
            {
                var port = PortInit();
                if (port != null)
                {
                    try
                    {
                        while (true)
                        {
                            if (port.BytesToRead > 0)
                            {
                                Console.WriteLine(port.ReadExisting());
                            }
                        }
                    }
                    catch (IOException ex)
                    {
                    }

                    catch (Exception ex)
                    {
                        Console.WriteLine(ex);
                    }
                }
                else
                {
                    Thread.Sleep(4000);
                }
               
            }
        }
    }
}
