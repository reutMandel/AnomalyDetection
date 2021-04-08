using AnomalyDetection.Model;
using System.ComponentModel;

namespace AnomalyDetection.ViewModel
{
    public class FlightPropertiesViewModel : ViewModel
    {
        private IFGModel fgModel;

        public FlightPropertiesViewModel(IFGModel fgModel)
        {
            this.fgModel = fgModel;
            this.fgModel.FlightProperties.PropertyChanged += delegate (object sender, PropertyChangedEventArgs e) { NotifyPropertyChanged(e.PropertyName); };
            this.fgModel.FlightProperties.PropertyChanged += delegate (object sender, PropertyChangedEventArgs e) { NotifyPropertyChanged("Normalized"+e.PropertyName); };
            Airspeed = 0;
        }

        public double NormalizedAltimeter
        {
            get
            {
                if (this.fgModel.FlightProperties.Altimeter < 0)
                    return 0;

                return 360 * this.fgModel.FlightProperties.Altimeter / 1000;
            }
        }

        public double NormalizedAirspeed
        {
            get { return (270 / 100) * this.fgModel.FlightProperties.Airspeed - 135; }
        }

        public double Altimeter
        {
            get => fgModel.FlightProperties.Altimeter;
            set
            {
                this.fgModel.FlightProperties.Altimeter = value;
                NotifyPropertyChanged("Altimeter");
            }
        }

        public double Airspeed
        {
            get => this.fgModel.FlightProperties.Airspeed;
            set
            {
                this.fgModel.FlightProperties.Airspeed = value;
                NotifyPropertyChanged("Airspeed");
            }
        }

        public double Direction
        {
            get => this.fgModel.FlightProperties.Direction;
            set
            {
                this.fgModel.FlightProperties.Direction = value;
                NotifyPropertyChanged("Direction");
            }
        }

        public double Pitch
        {
            get => this.fgModel.FlightProperties.Pitch;
            set
            {
                this.fgModel.FlightProperties.Pitch = value;
                NotifyPropertyChanged("Pitch");
            }
        }

        public double Roll
        {
            get => this.fgModel.FlightProperties.Roll;
            set
            {
                this.fgModel.FlightProperties.Roll = value;
                NotifyPropertyChanged("Roll");
            }
        }

        public double Yaw
        {
            get => this.fgModel.FlightProperties.Yaw;
            set
            {
                this.fgModel.FlightProperties.Yaw = value;
                NotifyPropertyChanged("Yaw");
            }
        }
    }
}
