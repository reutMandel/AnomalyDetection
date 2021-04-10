using System;

namespace AnomalyDetection.Model
{
    public class AnomalyDetectionUtil
    {
        private static double Avg(double[] x, int size)
        {
            double sum = 0;
            for (int i = 0; i < size; i++)
            {
                sum += x[i];
            }
            return (sum / size);
        }

        private static double Var(double[] x, int size)
        {
            double average = Avg(x, size);
            double sum = 0;
            for (int i = 0; i < size; i++)
            {
                sum += Math.Pow(x[i] - average, 2);
            }
            return sum / size;
        }

        // returns the covariance of X and Y
        private static double Cov(double[] x, double[] y, int size)
        {
            double sum = 0;
            for (int i = 0; i < size; i++)
            {
                sum += (x[i] - Avg(x, size)) * (y[i] - Avg(y, size));
            }
            return sum / size;
        }

        // returns the Pearson correlation coefficient of X and Y
        public static double Pearson(double[] x, double[] y, int size)
        {
            double denominator = Math.Sqrt(Var(x, size)) * Math.Sqrt(Var(y, size));
            if (denominator == 0)
                return 0;
            return Cov(x, y, size) / denominator;
        }

        // performs a linear regression and returns the line equation
        public static Line LinearReg(Point[] points, int size)
        {
            double[] x = new double[size];
            double[] y = new double[size];
            for (int i = 0; i < size; i++)
            {
                x[i] = points[i].X;
                y[i] = points[i].Y;
            }
            double a = Cov(x, y, size) / Var(x, size);
            double b = Avg(y, size) - a * Avg(x, size);
            return new Line(a, b);
        }

       // returns the deviation between point p and the line equation of the points
        public static double Dev(Point p, Point[] points, int size)
        {
            Line line = LinearReg(points, size);
            return Dev(p, line);
        }

        // returns the deviation between point p and the line
        public static double Dev(Point p, Line l)
        {
            return Math.Abs(l.F(p.X) - p.Y);
        }
    }

    public class Point 
    {
        public double X { get; set; }
        public double Y { get; set; }

        public Point(double x, double y)
        {
            this.X = x;
            this.Y = y;  
        }
    }
}
