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
            fgModel.Joystick.PropertyChanged += delegate (object sender, PropertyChangedEventArgs e) { NotifyPropertyChanged(e.PropertyName); };
            Aileron = 0;
            Elevator = 0;
        }

        public float Rudder
        {
            get { return fgModel.Joystick.Rudder; }
            set
            {
                fgModel.Joystick.Rudder = value;
                NotifyPropertyChanged("Rudder");
            }
        }
        public float Aileron
        {
            get { return 60 * fgModel.Joystick.Aileron + 125; }
            set
            {
                fgModel.Joystick.Aileron = value;
                NotifyPropertyChanged("Aileron");
            }
        }
        public float Elevator
        {
            get { return fgModel.Joystick.Elevator * 60 + 125; }
            set
            {
                fgModel.Joystick.Elevator = value;
                NotifyPropertyChanged("Elevator");
            }
        }
        public float Throttle
        {
            get { return fgModel.Joystick.Throttle; }
            set
            {
                fgModel.Joystick.Throttle = value;
                NotifyPropertyChanged("Throttle");
            }
        }
    }
}
