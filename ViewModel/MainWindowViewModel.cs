﻿using AnomalyDetection.Model;
using Microsoft.Win32;
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
 

        public MainWindowViewModel(IFGModel fgModel)
        {
            this.fgModel = fgModel;
            XmlButtonCommand = new DelegateCommand(o => XmlButtonClick());
            CsvButtonCommand = new DelegateCommand(o => CsvButtonClick());
            FgButtonCommand = new DelegateCommand(o => FgButtonClick());
            StartButtonCommand = new DelegateCommand(o => StartButtonClick());
            xmlIsClick = false;
            csvIsClick = false;
            fgIsClick = false;
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

        public string FgPath
        {
            get { return fgModel.FilesData.FgPath; }
            set
            {
                fgModel.FilesData.FgPath = value;
                NotifyPropertyChanged("FgPath");
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
                fgModel.ReadXmlFile();
            }
        }
    }
}
