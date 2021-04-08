using System.Collections.Generic;

namespace AnomalyDetection.Model
{
    public interface IFGModel
    {
        public JoystickProperties Joystick { get; set; }
        public ToolBarProperties ToolBarProperties { get; set; }
        public FilesDataProperties FilesData { get; set; }
        public Dictionary<string, int> CsvNames { get; }

        public event NotifyEventHandler LoadXmlCompleted;
        void StartStimulate();
        void ReadCsvFile();
        void ReadXmlFile();
        void ChangeStimulate();
        void PauseStimulate();
        void FastStimulate();
        void SlowStimulate();
    }
}