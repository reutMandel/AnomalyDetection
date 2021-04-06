using AnomalyDetection.Model;
using System.ComponentModel;

namespace AnomalyDetection.ViewModel
{
    public class JoystickViewModel : ViewModel
    {
        private IFGModel fgModel;
        public JoystickViewModel(IFGModel fgModel)
        {
            this.fgModel = fgModel;
            fgModel.JoystickProperties.PropertyChanged += delegate (object sender, PropertyChangedEventArgs e) { NotifyPropertyChanged(e.PropertyName); };
        }

        public float Rudder
        {
            get { return fgModel.JoystickProperties.Rudder; }
            set
            {
                fgModel.JoystickProperties.Rudder = value;
                NotifyPropertyChanged("Rudder");
            }
        }
        public float Aileron
        {
            get { return fgModel.JoystickProperties.Aileron; }
            set
            {
                fgModel.JoystickProperties.Aileron = value;
                NotifyPropertyChanged("Aileron");
            }
        }
        public float Elevator
        {
            get { return fgModel.JoystickProperties.Elevator; }
            set
            {
                fgModel.JoystickProperties.Elevator = value;
                NotifyPropertyChanged("Elevator");
            }
        }
        public float Throttle
        {
            get { return fgModel.JoystickProperties.Throttle; }
            set
            {
                fgModel.JoystickProperties.Throttle = value;
                NotifyPropertyChanged("Throttle");
            }
        }
    }
}
