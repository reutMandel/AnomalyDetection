using System.Collections.Generic;
using System.Globalization;

namespace AnomalyDetection.Model
{
    public class FlightProperties : Notify
    {
        private double altimeter;
        private double airspeed;
        private double direction;
        private double pitch;
        private double roll;
        private double yaw;

        public FlightProperties()
        {
            Altimeter = 0;
            Airspeed = 0;
            Direction = 0;
            Pitch = 0;
            Roll = 0;
            Yaw = 0;
        }

        public double Altimeter
        {
            get => this.altimeter;
            set
            {
                this.altimeter = value;
                NotifyPropertyChanged("Altimeter");
            }
        }

        public double Airspeed
        {
            get => this.airspeed;
            set
            {
                this.airspeed = value;
                NotifyPropertyChanged("Airspeed");
            }
        }

        public double Direction
        {
            get => this.direction;
            set
            {
                this.direction = value;
                NotifyPropertyChanged("Direction");
            }
        }

        public double Pitch
        {
            get => this.pitch;
            set
            {
                this.pitch = value;
                NotifyPropertyChanged("Pitch");
            }
        }

        public double Roll
        {
            get => this.roll;
            set
            {
                this.roll = value;
                NotifyPropertyChanged("Roll");
            }
        }

        public double Yaw
        {
            get => this.yaw;
            set
            {
                this.yaw = value;
                NotifyPropertyChanged("Yaw");
            }
        }

        public int AltimeterPosition { get; set; }
        public int AirspeedPosition { get; set; }
        public int DirectionPosition { get; set; }
        public int PitchPosition { get; set; }
        public int RollPosition { get; set; }
        public int YawPosition { get; set; }

        public void SetPositions(Dictionary<string, int> names)
        {
            AltimeterPosition = names["altitude-ft"];
            AirspeedPosition = names["airspeed-kt"];
            DirectionPosition = names["heading-deg"];
            PitchPosition = names["pitch-deg"];
            RollPosition = names["roll-deg"];
            YawPosition = names["side-slip-deg"];
        }

        public void SetValues(string[] values)
        {
            Altimeter = double.Parse(values[AltimeterPosition], CultureInfo.InvariantCulture);
            Airspeed = double.Parse(values[AirspeedPosition], CultureInfo.InvariantCulture);
            Direction = double.Parse(values[DirectionPosition], CultureInfo.InvariantCulture);
            Pitch = double.Parse(values[PitchPosition], CultureInfo.InvariantCulture);
            Roll = double.Parse(values[RollPosition], CultureInfo.InvariantCulture);
            Yaw = double.Parse(values[YawPosition], CultureInfo.InvariantCulture);
        }
    }
}