using System.ComponentModel;

namespace AnomalyDetection.Model
{
    public class JoystickProperties : INotifyPropertyChanged
    {
        private float rudder;
        private float aileron;
        private float elevator;
        private float throttle;

        public JoystickProperties()
        {
            this.Rudder = 0;
            this.Aileron = 0;
            this.Elevator = 0;
            this.Throttle = 0;
        }

        public float Rudder
        {
            get { return this.rudder; }
            set
            {
                rudder = value;
                NotifyPropertyChanged("Rudder");
            }
        }

        public float Aileron
        {
            get { return this.aileron; }
            set
            {
                aileron = value;
                NotifyPropertyChanged("Aileron");
            }
        }

        public float Elevator
        {
            get { return this.elevator; }
            set
            {
                elevator = value;
                NotifyPropertyChanged("Elevator");
            }
        }

        public float Throttle
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
