using System.Collections.Generic;

namespace AnomalyDetection.Model
{
    public interface IFGModel
    {
        public JoystickProperties Joystick { get; set; }
        public SpeedProperties SpeedProperties { get; set; }
        public FilesDataProperties FilesData { get; set; }
        public GraphsLogic GraphsLogic { get; set; }
        public Dictionary<string, int> CsvNames { get; }
        public CurrentPosition CurrentPosition { get; set; }

        public event NotifyEventHandler LoadXmlCompleted;
        public FlightProperties FlightProperties { get; set; }
        void StartStimulate();
        void ReadCsvFile();
        void ReadXmlFile();
        void ChangeStimulate();
        void PauseStimulate();
        void FastStimulate();
        void SlowStimulate();
        void DllLoad();
        string GetCorrelatedField(string fieldName);
        List<double> GetValuesByField(string fieldName);
        AlgorithmProperties GetAlgorithmProperties(string field1, string field2);
    }
}