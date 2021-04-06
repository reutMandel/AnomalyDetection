using System;
using System.ComponentModel;
using System.Text;
using System.IO;
using System.Threading;
using System.Collections.Generic;
using System.Linq;
using System.Globalization;

namespace AnomalyDetection.Model
{
    public class FGModel : IFGModel
    {
        private string xmlPath;
        private string csvPath;
        private string fgPath;
        private int numOfLines;
        private IClient client;
        private Thread thread;
        private Dictionary<string,int> csvNames;
        private List<string> csvFile;
        public event PropertyChangedEventHandler PropertyChanged;
        public JoystickProperties JoystickProperties { get; set; }

        private static readonly FGModel instance = new FGModel();

        private FGModel()
        {
            this.client = new Client("127.0.0.1", 5400);
            JoystickProperties = new JoystickProperties();
        }

        public static FGModel Instance
        {
            get
            {
                return instance;
            }
        }

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

        public int NumOfLines
        {
            get { return numOfLines; }
            set
            {
                numOfLines = value;
                NotifyPropertyChanged("numOfLines");
            }
        }

        public void NotifyPropertyChanged(string propName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }

        public void ReadCsvFile()
        {
            csvFile = CsvReader.ReadCsvFile(this.csvPath);
            NumOfLines = csvFile.Count;
        }

        public void ReadXmlFile()
        {
            csvNames = XmlParserUtil.Parse(FGXmlReader.Reader(this.xmlPath));
        }

        public void StartStimulate()
        {
            if (this.csvPath == null || this.xmlPath == null)
            {
                throw new Exception("csv path or xml path are not valid"); // create new exception
            }

            client.Connect();

            ChangeStimulate(0);
        }

        public void ChangeStimulate(int line)
        {
            if (thread != null && thread.IsAlive)
                thread.Abort();

            thread = new Thread(() => { Logic(client, line); });
     
            thread.Start();
        }


        private void Logic(IClient client, int line)
        {
            for (int i = line; i < numOfLines; i++)
            {
                string currentLine = csvFile[i];
                string[] values = currentLine.Split(',');
                JoystickProperties.Rudder = float.Parse(values[csvNames["rudder"]], CultureInfo.InvariantCulture);
                JoystickProperties.Throttle = float.Parse(values[csvNames["throttle"]], CultureInfo.InvariantCulture);
                SendData(client, currentLine);
                Thread.Sleep(100);
            }
        }


        private void SendData(IClient client, string line)
        {
            client.Send(Encoding.ASCII.GetBytes(line));
        }
    }
}
