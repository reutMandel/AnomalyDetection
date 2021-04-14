namespace AnomalyDetection.Model
{
    public class Circle
    {
        public Point Center { get; set; }
        public double Radius { get; set; }
        public Circle(Point c, double r)
        {
            this.Center = c;
            this.Radius = r;
        }
    }

}
