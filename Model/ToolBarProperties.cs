using System;

namespace AnomalyDetection.Model
{
    public class ToolBarProperties : Notify
    {
        private int numOfLines;
        private int currentPosition;
        private double speed;

        public ToolBarProperties()
        {
            CurrentPosition = 0;
            Speed = 1;
            Sleep = 100;
        }

        public double Speed
        {
            get => this.speed;
            set
            {
                this.speed = value;
                NotifyPropertyChanged("Speed");
            }
        }

        public int Sleep { get; set; }

        public int NumOfLines
        {
            get { return numOfLines; }
            set
            {
                numOfLines = value;
                NotifyPropertyChanged("NumOfLines");
            }
        }

        public int CurrentPosition
        {
            get { return currentPosition; }
            set
            {
                currentPosition = value;
                NotifyPropertyChanged("CurrentPosition");
            }
        }

        public void CalculateSleepThread(bool isFaster)
        {
            if (isFaster && Speed < 2)
            {
                Speed += 0.25;
            }
            else if (!isFaster && Speed > 0.5)
            {
                Speed -= 0.25;
            }
            Sleep = Convert.ToInt32(1000 / (10 * Speed));
        }
    }
}
