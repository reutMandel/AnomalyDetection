using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnomalyDetection.Model
{
    public class SpeedProperties : INotifyPropertyChanged
    {
        private double speed;
        private int sleep;

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

        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged(string propName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
    }
}
