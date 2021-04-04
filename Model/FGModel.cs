using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.IO;
using System.Threading;

namespace AnomalyDetection.Model
{
    public class FGModel : IFGModel
    {
        private string xmlPath;
        private string csvPath;
        private string fgPath;
        public event PropertyChangedEventHandler PropertyChanged;

        public string XmlPath
        {
            get { return xmlPath; }
            set
            {
                xmlPath = value;
                NotifyPropertyChanged("xmlPath");
            }
        }

        public string CsvPath
        {
            get { return csvPath; }
            set
            {
                csvPath = value;
                NotifyPropertyChanged("csvPath");
            }
        }

        public string FgPath
        {
            get { return fgPath; }
            set
            {
                fgPath = value;
                NotifyPropertyChanged("fgPath");
            }
        }

        public void NotifyPropertyChanged(string propName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }

        public void StartStimulate()
        {
            if (this.csvPath == null || this.xmlPath == null)
            {
                throw new Exception("csv path or xml path are not valid"); // create new exception
            }
            try
            {
                // IPAddress ipAddress = IPAddress.Parse("127.0.0.1");
                IPEndPoint ipEndPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 5400);
                Socket socket = new Socket(ipEndPoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
                socket.Connect(ipEndPoint);
                //TcpClient client = new TcpClient();
               // while (!client.Connected) // keep trynig to connect
              //  {
                 //   try { client.Connect(ipEndPoint); }
                 //   catch (Exception e)
                  //  {
                  //  }
              //  }





               // BinaryWriter writer = new BinaryWriter(client.GetStream());
                //   string[] lines = File.ReadAllLines(csvPath);
                using (StreamReader sr = new StreamReader(csvPath))
                {
                    string currentLine;
                    // currentLine will be null when the StreamReader reaches the end of file

                    while ((currentLine = sr.ReadLine()) != null)
                    {
                        //currentLine += "\r\n";
                       // writer.Write(Encoding.ASCII.GetBytes(currentLine));
                        var byteArr = Encoding.ASCII.GetBytes(currentLine);
                   
                        socket.Send(byteArr);
                        Thread.Sleep(100);
                    }
                }
            }
            catch (Exception e)
            {

            }
        }
    }
}
