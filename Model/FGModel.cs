using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

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
                IPAddress ipAddress = IPAddress.Parse("127.0.0.1");
                IPEndPoint ipEndPoint = new IPEndPoint(ipAddress, 5400);
                Socket socket = new Socket(ipEndPoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
                socket.Connect(ipEndPoint);

            }
            catch(Exception e)
            {

            }
        }
    }
}
