using System;
using System.Text;
using System.Threading;
using System.Collections.Generic;

namespace AnomalyDetection.Model
{
    public delegate void NotifyEventHandler();

    public class FGModel : IFGModel
    {
        private IClient client;
        private Thread thread;
        private bool stopThread;
        private List<string> csvFile;
        private Dictionary<string, int> csvNames;
        public JoystickProperties Joystick { get; set; } 
        public SpeedProperties SpeedProperties { get; set; }
        public FilesDataProperties FilesData { get; set; }
        public FlightProperties FlightProperties { get; set; }
        public GraphsLogic GraphsLogic { get; set; }

        public CurrentPosition CurrentPosition { get; set; }

        public event NotifyEventHandler LoadXmlCompleted;


        private static readonly FGModel instance = new FGModel();

        private FGModel()
        {
            this.client = new Client("127.0.0.1", 5400);
            FilesData = new FilesDataProperties();
            Joystick = new JoystickProperties();
            SpeedProperties = new SpeedProperties();
            FlightProperties = new FlightProperties();
            GraphsLogic = new GraphsLogic();
            CurrentPosition = new CurrentPosition();
            this.stopThread = false;
        }

        public Dictionary<string, int> CsvNames { get => csvNames; }

        public static FGModel Instance
        {
            get
            {
                return instance;
            }
        }


        public void ReadCsvFile()
        {
            csvFile = CsvReader.ReadCsvFile(this.FilesData.CsvPath);
            SpeedProperties.NumOfLines = csvFile.Count;
        }

        public void ReadXmlFile()
        {
            csvNames = XmlParserUtil.Parse(FGXmlReader.Reader(this.FilesData.XmlPath));
            Joystick.SetPositions(csvNames);
            FlightProperties.SetPositions(csvNames);
            LoadXmlCompleted();
        }

        public void StartStimulate()
        {
            GraphsLogic.Columns = CsvParserUtil.convertLinesToColumns(csvFile, csvNames);
            if (this.FilesData.CsvPath == null || this.FilesData.XmlPath == null)
            {
                throw new Exception("csv path or xml path are not valid"); // create new exception
            }
            client.Connect();
            ChangeStimulate();
        }

        public void ChangeStimulate()
        {
            this.stopThread = false;
            thread = new Thread(() => {Logic(client, CurrentPosition.Position); });
            thread.Start();
        }

        public void PauseStimulate()
        {
            if (thread != null)
            {
                this.stopThread = true;
                this.thread.Join();
            }
        }

        public void FastStimulate()
        {
            SpeedProperties.CalculateSleepThread(true);
        }

        public void SlowStimulate()
        {
            SpeedProperties.CalculateSleepThread(false);
        }

        public string GetCorrelatedField(string fieldName)
        {
            return GraphsLogic.GetCorrelatedField(fieldName);
        }

        public List<double> GetValuesByField(string fieldName)
        {
            return GraphsLogic.GetValuesByFieldName(fieldName);
        }

        private void Logic(IClient client, int line)
        {
            for (int i = line; i < SpeedProperties.NumOfLines; i++)
            {
                if (this.stopThread)
                    break;

                CurrentPosition.Position = i;
                string currentLine = csvFile[i];
                string[] values = currentLine.Split(',');
                Joystick.SetValues(values[Joystick.RudderPosition], values[Joystick.AileronPosition],
                    values[Joystick.ElevatorPosition], values[Joystick.ThrottlePosition]);
                FlightProperties.SetValues(values);
                SendData(client, currentLine);
                Thread.Sleep(SpeedProperties.Sleep);
            }
        }

        private void SendData(IClient client, string line)
        {
            client.Send(Encoding.ASCII.GetBytes(line));
        }
    }
}