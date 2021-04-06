using System;
using System.ComponentModel;
using System.Text;
using System.IO;
using System.Threading;
using System.Collections.Generic;

namespace AnomalyDetection.Model
{
    public class FGModel : IFGModel
    {
        private string xmlPath;
        private string csvPath;
        private string fgPath;
        private int numOfLines;
        private int currentPosition;
        private IClient client;
        private Thread thread;
        private List<string> csvFile;
        public event PropertyChangedEventHandler PropertyChanged;
        public JoystickProperties JoystickProperties { get; set; } 

        private static readonly FGModel instance = new FGModel();

        private FGModel()
        {
            this.client = new Client("127.0.0.1", 5400);
            this.currentPosition = 0;
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
            }
        }

        public string CsvPath
        {
            get { return csvPath; }
            set
            {
                csvPath = value;
            }
        }

        public string FgPath
        {
            get { return fgPath; }
            set
            {
                fgPath = value;
            }
        }

        public int NumOfLines
        {
            get { return numOfLines; }
            set
            {
                numOfLines = value;
                NotifyPropertyChanged("NumOfLines");
            }
        }

        public int CurrentPosition
        {
            get { return currentPosition; }
            set
            {
                currentPosition = value;
                NotifyPropertyChanged("CurrentPosition");
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
            FGXmlReader.Reader(this.xmlPath);
            NumOfLines = csvFile.Count;
        }

        public void StartStimulate()
        {
            if (this.csvPath == null || this.xmlPath == null)
            {
                throw new Exception("csv path or xml path are not valid"); // create new exception
            }
            client.Connect();
            ChangeStimulate();
        }

        public void ChangeStimulate()
        {
            if (thread != null && thread.IsAlive)
                thread.Abort();

            thread = new Thread(() => {SendData(client, this.currentPosition); });
            thread.Start();
        }

        public void PauseStimulate()
        {
            if(thread != null && thread.IsAlive)
            {
                thread.Abort();
            }
        }

        private void SendData(IClient client, int line)
        {
            for (int i = line; i < numOfLines; i++)
            {
                CurrentPosition = i;
                client.Send(Encoding.ASCII.GetBytes(csvFile[i]));
                Thread.Sleep(100);
            }
        }
    }
}
