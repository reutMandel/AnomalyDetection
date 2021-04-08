﻿using System;
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

        public ICommand ForwardCommand { get; set; }
        public ICommand BackwardCommand { get; set; }

        public ToolBarViewModel(IFGModel fgModel)
        {
            this.fgModel = fgModel;
            fgModel.PropertyChanged += delegate (object sender, PropertyChangedEventArgs e) { NotifyPropertyChanged(e.PropertyName); };
            fgModel.SpeedProperties.PropertyChanged += delegate (object sender, PropertyChangedEventArgs e) { NotifyPropertyChanged(e.PropertyName); };
            PauseCommand = new DelegateCommand(o => PauseSimulator());
            ContinueCommand = new DelegateCommand(o => ContinueSimulator());
            ForwardCommand = new DelegateCommand(o => FasterSimulate());
            BackwardCommand = new DelegateCommand(o => SlowerSimulate());

        }

        private void FasterSimulate()
        {
            fgModel.FastStimulate();
        }

        private void SlowerSimulate()
        {
            fgModel.SlowStimulate();
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

        public double Speed
        {
            get => fgModel.SpeedProperties.Speed;
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
