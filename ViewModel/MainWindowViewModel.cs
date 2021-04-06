using AnomalyDetection.Model;
using Microsoft.Win32;
using System;
using System.ComponentModel;
using System.Windows.Input;

namespace AnomalyDetection.ViewModel
{
    public class MainWindowViewModel : ViewModel
    {
        private IFGModel fgModel;
        private bool xmlIsClick, csvIsClick, fgIsClick, startIsEnable;

        public ICommand XmlButtonCommand { get; set; }
        public ICommand CsvButtonCommand { get; set; }
        public ICommand FgButtonCommand { get; set; }
        public ICommand StartButtonCommand { get; set; }
        public ICommand SliderCommand { get; set; }
        public ICommand PauseCommand { get; set; }
        public ICommand ContinueCommand { get; set; }

        public MainWindowViewModel(IFGModel fgModel)
        {
            this.fgModel = fgModel;
            fgModel.PropertyChanged += delegate (object sender, PropertyChangedEventArgs e) { NotifyPropertyChanged( e.PropertyName); };
            XmlButtonCommand = new DelegateCommand(o => XmlButtonClick());
            CsvButtonCommand = new DelegateCommand(o => CsvButtonClick());
            FgButtonCommand = new DelegateCommand(o => FgButtonClick());
            StartButtonCommand = new DelegateCommand(o => StartButtonClick());
            PauseCommand = new DelegateCommand(o => PauseSimulator());
            ContinueCommand = new DelegateCommand(o => ContinueSimulator());
            xmlIsClick = false;
            csvIsClick = false;
            fgIsClick = false;
            startIsEnable = false;
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
                NotifyPropertyChanged("CsvFile");
            }
        }

        public string FgPath
        {
            get { return fgModel.FgPath; }
            set
            {
                fgModel.FgPath = value;
                NotifyPropertyChanged("FgPath");
            }
        }

        public int NumOfLines
        {
            get { return fgModel.NumOfLines; }
        }

        public int CurrentPosition
        {
           get { return fgModel.CurrentPosition; }
           set
           {
                fgModel.CurrentPosition = value;
                NotifyPropertyChanged("CurrentPosition");
           }
        }

        public void SlideHandler()
        {
            fgModel.ChangeStimulate();
        }

        public bool StartIsClick
        {
            get { return startIsEnable; }
            set
            {
                startIsEnable = value;
                NotifyPropertyChanged("StartIsClick");
            }
        }

        private void StartButtonClick()
        {
            fgModel.StartStimulate();
        }

        private void PauseSimulator()
        {
            fgModel.PauseStimulate();

        }

        private void ContinueSimulator()
        {
            fgModel.ChangeStimulate();
        }

        private void FgButtonClick()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                FgPath = openFileDialog.FileName;
                fgIsClick = true;
                StartIsClick = fgIsClick && xmlIsClick && csvIsClick;
            }
        }

        private void CsvButtonClick()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                CsvFile = openFileDialog.FileName;
                csvIsClick = true;
                StartIsClick = fgIsClick && xmlIsClick && csvIsClick;
                fgModel.ReadCsvFile();
            }
        }

        private void XmlButtonClick()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                XmlFile = openFileDialog.FileName;
                xmlIsClick = true;
                StartIsClick = fgIsClick && xmlIsClick && csvIsClick;
            }
        }
    }
}
