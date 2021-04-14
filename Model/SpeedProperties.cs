using System;

namespace AnomalyDetection.Model
{
    public class SpeedProperties : Notify
    {
        private int numOfLines;
        private double speed;
        private int sleep;
        public event NotifyEventHandler SpeedChanged;

        public SpeedProperties()
        {
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

        public int Sleep
        {
            get => this.sleep;
            set
            {
                this.sleep = value;
                SpeedChanged?.Invoke();
            }
        }

        public int NumOfLines
        {
            get { return numOfLines; }
            set
            {
                numOfLines = value;
                NotifyPropertyChanged("NumOfLines");
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
