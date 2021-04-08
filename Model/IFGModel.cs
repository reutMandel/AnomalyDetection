namespace AnomalyDetection.Model
{
    public interface IFGModel
    {

        public JoystickProperties Joystick { get; set; }
        public ToolBarProperties ToolBarProperties { get; set; }
        public FilesDataProperties FilesData { get; set; }
        void StartStimulate();
        void ReadCsvFile();
        void ReadXmlFile();

        void ChangeStimulate();

        void PauseStimulate();

        void FastStimulate();
        void SlowStimulate();
    }
}