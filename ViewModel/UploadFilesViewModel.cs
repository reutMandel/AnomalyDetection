using AnomalyDetection.Model;
using Microsoft.Win32;
using System.Windows;
using System.Windows.Input;

namespace AnomalyDetection.ViewModel
{
    public class UploadFilesViewModel : ViewModel
    {
        private IFGModel fgModel;
        private bool xmlIsClick, csvIsClick, instructionIsClick, startIsEnable;
        public ICommand XmlButtonCommand { get; set; }
        public ICommand CsvButtonCommand { get; set; }
        public ICommand InstructionButtonCommand { get; set; }
        public ICommand StartButtonCommand { get; set; }

        public UploadFilesViewModel(IFGModel fGModel)
        {
            this.fgModel = fGModel;
            XmlButtonCommand = new DelegateCommand(o => XmlButtonClick());
            CsvButtonCommand = new DelegateCommand(o => CsvButtonClick());
            InstructionButtonCommand = new DelegateCommand(o => InstructionButtonClick());
            StartButtonCommand = new DelegateCommand(o => StartButtonClick());
            xmlIsClick = false;
            csvIsClick = false;
            instructionIsClick = false;
            startIsEnable = false;
        }

        public string XmlFile
        {
            get { return fgModel.FilesData.XmlPath; }
            set
            {
                fgModel.FilesData.XmlPath = value;
                NotifyPropertyChanged("XmlFile");
            }
        }

        public string CsvFile
        {
            get { return fgModel.FilesData.CsvPath; }
            set
            {
                fgModel.FilesData.CsvPath = value;
                NotifyPropertyChanged("CsvFile");
            }
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

        private void InstructionButtonClick()
        {
            instructionIsClick = true;
            StartIsClick = instructionIsClick && xmlIsClick && csvIsClick;
            MessageBox.Show("Please add playback_small.xml to $FG_ROOT/data/Protocol/ directory.\n Make sure to run FlightGear with the following settings:\n "
                 + " --generic = socket, in, 10, 127.0.0.1, 5400, tcp, playback_small --fdm = null \n"
                 + "Start FlightGear simulator with the above settings. \n "
                 + "Choose csv and xml files and start the simulator.", "Instruction", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void CsvButtonClick()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                CsvFile = openFileDialog.FileName;
                csvIsClick = true;
                StartIsClick = instructionIsClick && xmlIsClick && csvIsClick;
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
                StartIsClick = instructionIsClick && xmlIsClick && csvIsClick;
                fgModel.ReadXmlFile();
            }
        }
    }
}
