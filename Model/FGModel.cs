using System;
using System.ComponentModel;
using System.Text;
using System.IO;
using System.Threading;
using System.Collections.Generic;
using System.Globalization;

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
        private bool stopThread;
        private List<string> csvFile;
        private Dictionary<string, int> csvNames;
        public event PropertyChangedEventHandler PropertyChanged;
        public JoystickProperties Joystick { get; set; } 

        private static readonly FGModel instance = new FGModel();

        private FGModel()
        {
            this.client = new Client("127.0.0.1", 5400);
            this.currentPosition = 0;
            Joystick = new JoystickProperties();
            this.stopThread = false;
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
            csvNames = XmlParserUtil.Parse(FGXmlReader.Reader(this.xmlPath));
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
            //this.stopThread = this.thread != null ? true : false;
            this.stopThread = false;
            thread = new Thread(() => {Logic(client, this.currentPosition); });
            thread.Start();
        }

        public void PauseStimulate()
        {
            this.stopThread = true;
            this.thread.Join();
        }

        private void Logic(IClient client, int line)
        {
            for (int i = line; i < numOfLines; i++)
            {
                if (this.stopThread)
                    break;

                CurrentPosition = i;
                string currentLine = csvFile[i];
                string[] values = currentLine.Split(',');
                Joystick.SetValues(values[Joystick.RudderPosition], values[Joystick.AileronPosition],
                    values[Joystick.ElevatorPosition], values[Joystick.ThrottlePosition]);
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
