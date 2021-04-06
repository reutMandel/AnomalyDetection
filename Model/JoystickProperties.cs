using System.ComponentModel;

namespace AnomalyDetection.Model
{
    public class JoystickProperties : INotifyPropertyChanged
    {
        private int rudder;
        private int aileron;
        private int elevator;
        private int throttle;

        public int Rudder
        {
            get { return this.rudder; }
            set
            {
                rudder = value;
                NotifyPropertyChanged("Rudder");
            }
        }

        public int Aileron
        {
            get { return this.aileron; }
            set
            {
                aileron = value;
                NotifyPropertyChanged("Aileron");
            }
        }

        public int Elevator
        {
            get { return this.elevator; }
            set
            {
                elevator = value;
                NotifyPropertyChanged("Elevator");
            }
        }

        public int Throttle
        {
            get { return this.throttle; }
            set
            {
                throttle = value;
                NotifyPropertyChanged("Throttle");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChanged(string propName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
    }
}
