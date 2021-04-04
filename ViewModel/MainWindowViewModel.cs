using AnomalyDetection.Model;

namespace AnomalyDetection.ViewModel
{
    public class MainWindowViewModel : ViewModel
    {
        private IFGModel fgModel;

        public MainWindowViewModel(IFGModel fgModel)
        {
            this.fgModel = fgModel;
        }
        public string XmlFile
        {
            get { return fgModel.XmlPath; }
            set
            {
                fgModel.XmlPath = value;
                NotifyPropertyChanged("XmlFile");
            }
        }

        public string CsvFile
        {
            get { return fgModel.CsvPath; }
            set
            {
                fgModel.CsvPath = value;
                NotifyPropertyChanged("CsvPath");
            }
        }

        public string FgPath
        {
            get { return fgModel.FgPath; }
            set
            {
                fgModel.FgPath = value;
                NotifyPropertyChanged("fgPath");
            }
        }

        public void MVStartSimulator()
        {
            fgModel.StartStimulate();
        }
    }
}
