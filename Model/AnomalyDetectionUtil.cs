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

        public static void FindCorrelated()
        {

        }
        // performs a linear regression and returns the line equation
        //Line linear_reg(Point** points, int size)
        //{
        //    float* x = new float[size];
        //    float* y = new float[size];
        //    for (int i = 0; i < size; i++)
        //    {
        //        x[i] = points[i]->x;
        //        y[i] = points[i]->y;
        //    }
        //    float a = cov(x, y, size) / var(x, size);
        //    float b = avg(y, size) - a * avg(x, size);
        //    delete(x);
        //    delete(y);
        //    return Line(a, b);
        //}

        // returns the deviation between point p and the line equation of the points
        //float dev(Point p, Point** points, int size)
        //{
        //    Line line = linear_reg(points, size);
        //    return dev(p, line);
        //}

        //// returns the deviation between point p and the line
        //float dev(Point p, Line l)
        //{
        //    return std::abs(l.f(p.x) - p.y);
        //}
    }
}
