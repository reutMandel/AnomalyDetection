using System.ComponentModel;

namespace AnomalyDetection.Model
{
    public interface IFGModel : INotifyPropertyChanged
    {
        string XmlPath { get; set;}
        string CsvPath { get; set;}
        string FgPath { get; set; }
        public JoystickProperties JoystickProperties { get; set; }
        void StartStimulate();
        void ReadCsvFile();
        void ReadXmlFile();
    }
}