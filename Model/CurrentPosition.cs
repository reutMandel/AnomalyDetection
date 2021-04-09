
namespace AnomalyDetection.Model
{
    public class CurrentPosition : Notify
    {
        private int position;
        public event NotifyEventHandler PositionChanged;
        public int Position
        {
            get { return this.position; }
            set {
                this.position = value;
                NotifyPropertyChanged("Position");
                PositionChanged?.Invoke();
            }
        }

        public CurrentPosition()
        {
            Position = 0;
        }
    }
}
