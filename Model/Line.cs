namespace AnomalyDetection.Model
{
    public class Line
    {
        public double A { get; set; }
        public double B { get; set; }

        public Line()
        {
            this.A = 0;
            this.B = 0;
        }

        public Line(double a, double b)
        {
            this.A = a;
            this.B = b;
        }

        public double F(double x)
        {
            return A * x + B;
        }
    }
}
