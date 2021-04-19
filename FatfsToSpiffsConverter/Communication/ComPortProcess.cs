using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FatfsToSpiffsConverter.Communication
{

    class ComPortProcess
    {
        private CancellationTokenSource m_source = new CancellationTokenSource();
        private static object m_lock = new object();
        private Task m_currentThread;
        public ConcurrentQueue<byte[]> messagesQueueTx = new ConcurrentQueue<byte[]>();
        private MessagesProto m_msgProto;
        private SerialPort port;
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

        public static List<string> GetPorts()
        {
            string[] r = SerialPort.GetPortNames();
            List<string> listStr = new List<string>();
            foreach(var s in r)
            {
                if (!listStr.Contains(s))
                {
                    listStr.Add(s);
                }
            }
            return listStr;
        }

        private SerialPort PortInit()
        {
            SerialPort p = new SerialPort();
            p.PortName = "COM3";
            p.BaudRate = 115200;

            Console.WriteLine("Try opening port: " + p.PortName);
            p.Open();
            Console.WriteLine("port opened: " + p.PortName);
            return p;
        }

        private void Processing(CancellationToken token)
        {
            while (!token.IsCancellationRequested)
            {
                try
                {
                    Thread.Sleep(2000);
                    port = PortInit();
                    UpdateConnectionText();
                    ClearMessageQueue();
                    try
                    {   
                        while (port.IsOpen)
                        {
                            ReceiveDatFromPort();
                            SendDataToPort();
                            Thread.Sleep(2);
                        }
                    }
                    finally
                    {
                        port.Close();
                    }
                }
                catch (IOException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                UpdateConnectionText();
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

        private void ClearMessageQueue()
        {
            while(messagesQueueTx.Count != 0)
            {
                byte[] data;
                messagesQueueTx.TryDequeue(out data);
            }
        }

        private void UpdateConnectionText()
        {
            string text;
            Color cl;
            bool ok = (port != null && port.IsOpen);
            if (ok)
            {
                text = "Connected";
                cl = Color.SpringGreen;
            }
            else
            {
                text = "No connected";
                cl = Color.Red;
            }
            MainHandler.SetControlPropertyThreadSafe(MainHandler.FormInstance.connectionLabel, "Text", text);
            MainHandler.SetControlPropertyThreadSafe(MainHandler.FormInstance.connectionLabel, "ForeColor", cl);
        }

       
    }
}
