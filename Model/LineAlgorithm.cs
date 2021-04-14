using OxyPlot.Annotations;
using System.Linq;

namespace AnomalyDetection.Model
{
    class LineAlgorithm : IAlgorithm
    {
        public AlgorithmProperties GetAlgorithmProperties(Point[] points)
        {
            Line line = AnomalyDetectionUtil.LinearReg(points, points.Length);
            LineAnnotation annotation = new LineAnnotation();
            double minX = points.Select(p => p.X).Min();
            double minY = line.F(minX);
            double maxX = points.Select(p => p.X).Max();
            double maxY = line.F(maxX);
            annotation.MaximumX = maxX;
            annotation.MaximumY = maxY;
            annotation.MinimumX = minX;
            annotation.MinimumY = minY;
            annotation.Slope = line.A;
            annotation.Intercept = line.B;
            return new AlgorithmProperties
            {
                Points = points.ToList(),
                AnnotationShape = annotation
            };
        }
    }
}
