using OxyPlot;
using OxyPlot.Annotations;
using System.Linq;

namespace AnomalyDetection.Model
{
    class CircleAlgorithm : IAlgorithm
    {
        public AlgorithmProperties GetAlgorithmProperties(Point[] points)
        {
            Circle circle = AnomalyDetectionUtil.MinCircle(points, points.Length);
            EllipseAnnotation annotation = new EllipseAnnotation();
            annotation.Width = circle.Radius * 2;
            annotation.Height = circle.Radius * 2;
            annotation.X = circle.Center.X;
            annotation.Y = circle.Center.Y;
            annotation.Fill = OxyColors.Transparent;
            annotation.Stroke = OxyColors.Black; annotation.StrokeThickness = 2;
            return new AlgorithmProperties
            {
                Points = points.ToList(),
                AnnotationShape = annotation
            };
        }
    }
}
