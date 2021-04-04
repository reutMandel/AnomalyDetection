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
        private IClient client;
        private List<string> csvFile;
        public event PropertyChangedEventHandler PropertyChanged;

        public FGModel()
        {
            this.client = new Client("127.0.0.1", 5400);
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

        public void NotifyPropertyChanged(string propName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }

        public void ReadCsvFile()
        {
            csvFile = CsvReader.ReadCsvFile(this.csvPath);
        }

        public void StartStimulate()
        {
            if (this.csvPath == null || this.xmlPath == null)
            {
                throw new Exception("csv path or xml path are not valid"); // create new exception
            }

            Thread thr = new Thread(delegate ()
            {
                client.Connect();

                using (StreamReader sr = new StreamReader(csvPath))
                {
                    string currentLine;
                    while ((currentLine = sr.ReadLine()) != null)
                    {
                        currentLine += "\n";
                        client.Send(Encoding.ASCII.GetBytes(currentLine));
                        Thread.Sleep(100);
                    }
                }
                client.Disconnect();
            });
            thr.Start();
        }
    }
}
