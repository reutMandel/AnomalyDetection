using System.Collections.Generic;
using System.Globalization;

namespace AnomalyDetection.Model
{
    public class JoystickProperties : Notify
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

        public int RudderPosition { get; set; }
        public int AileronPosition { get; set; }
        public int ElevatorPosition { get; set; }
        public int ThrottlePosition { get; set; }

        public void SetPositions(Dictionary<string, int> names)
        {
            RudderPosition = names["rudder"];
            AileronPosition = names["aileron"];
            ElevatorPosition = names["elevator"];
            ThrottlePosition = names["throttle"];
        }

        public void SetValues(string rudder, string aileron, string elevator, string throttle)
        {
            Rudder = float.Parse(rudder, CultureInfo.InvariantCulture);
            Aileron = float.Parse(aileron, CultureInfo.InvariantCulture);
            Elevator = float.Parse(elevator, CultureInfo.InvariantCulture);
            Throttle = float.Parse(throttle, CultureInfo.InvariantCulture);
        }
    }
}