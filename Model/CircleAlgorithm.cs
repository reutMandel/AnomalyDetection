using OxyPlot;
using OxyPlot.Annotations;
using System.Linq;

namespace AnomalyDetection.Model
{
    class CircleAlgorithm : IAlgorithm
    {
        public AlgorithmProperties GetAlgorithmProperties(Point[] points)
        {
            Circle circle = AnomalyDetectionLogic.FindMinCircle(points);
            EllipseAnnotation annotation = new EllipseAnnotation();
            annotation.Width = 2 * circle.Radius;
            annotation.Height = 2 * circle.Radius;
            annotation.X = circle.Center.X;
            annotation.Y = circle.Center.Y;
            annotation.Fill = OxyColors.Transparent;
            annotation.Stroke = OxyColors.Black; 
            annotation.StrokeThickness = 2;

            return new AlgorithmProperties
            {
                Points = points.ToList(),
                AnnotationShape = annotation,
                minX = (int)( circle.Center.X - (2 * circle.Radius)),
                maxX = (int)(circle.Center.X + 2 * circle.Radius),
                minY = (int)(circle.Center.Y - (2 * circle.Radius)),
                maxY = (int)(circle.Center.Y + (2 * circle.Radius)),
            };
        }
    }
}
