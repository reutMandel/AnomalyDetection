using System.ComponentModel;
using System.Windows.Input;
using AnomalyDetection.Model;

namespace AnomalyDetection.ViewModel
{
    public class ToolBarViewModel : ViewModel
    {
        private IFGModel fgModel;
        public ICommand SliderCommand { get; set; }
        public ICommand PauseCommand { get; set; }
        public ICommand ContinueCommand { get; set; }

        public ToolBarViewModel(IFGModel fgModel)
        {
            this.fgModel = fgModel;
            fgModel.PropertyChanged += delegate (object sender, PropertyChangedEventArgs e) { NotifyPropertyChanged(e.PropertyName); };
            PauseCommand = new DelegateCommand(o => PauseSimulator());
            ContinueCommand = new DelegateCommand(o => ContinueSimulator());
        }

        public int CurrentPosition
        {
            get { return fgModel.CurrentPosition; }
            set
            {
                fgModel.CurrentPosition = value;
                NotifyPropertyChanged("CurrentPosition");
                SliderHandler();
            }
        }

        public void SliderHandler()
        {
            fgModel.PauseStimulate();
            fgModel.ChangeStimulate();
        }

        public int NumOfLines
        {
            get { return fgModel.NumOfLines; }
        }

        private void PauseSimulator()
        {
            fgModel.PauseStimulate();

        }

        private void ContinueSimulator()
        {
            fgModel.ChangeStimulate();
        }

    }
}
