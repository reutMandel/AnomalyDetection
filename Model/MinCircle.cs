using System;
using System.Collections.Generic;

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

    public class MinCircle
    {
        public bool CheckIfPointInCircle(Circle c, Point p)
        {
            double distance = Math.Sqrt(Math.Pow(c.Center.X - p.X, 2) +
                Math.Pow(c.Center.Y - p.Y, 2));
            if (distance <= c.Radius)
            {
                return true;
            }
            return false;
        }

        public Circle CreateCircleFromTwoPoints(Point p1, Point p2)
        {
            double distance = Math.Sqrt(Math.Pow(p1.X - p2.X, 2) +
                Math.Pow(p1.Y - p2.Y, 2));
            double x = (p2.X + p1.X) / 2;
            double y = (p2.Y + p1.Y) / 2;
            return new Circle(new Point(x, y), distance / 2);
        }

        public Circle CreateCircleFromThreePoints(Point p1, Point p2, Point p3)
        {
            double mP1p2 = (p1.Y - p2.Y) / (p1.X - p2.X);
            double mP2p3 = (p2.Y - p3.Y) / (p2.X - p3.X);

            double verMp1p2 = -1 / mP1p2;
            double verMp2p3 = -1 / mP2p3;

            Point middleP1p2 = new Point((p1.X + p2.X) / 2, (p1.Y + p2.Y) / 2);
            Point middleP2p3 = new Point((p2.X + p3.X) / 2, (p2.Y + p3.Y) / 2);

            double bVerP1p2 = (-verMp1p2 * middleP1p2.X) + middleP1p2.Y;
            double bVerP2p3 = (-verMp2p3 * middleP2p3.X) + middleP2p3.Y;

            double centerX = (bVerP1p2 - bVerP2p3) / (verMp2p3 - verMp1p2);
            double centerY = (verMp1p2 * centerX) + bVerP1p2;

            double radius = Math.Sqrt(Math.Pow(p1.X - centerX, 2) +
                Math.Pow(p1.Y - centerY, 2));

            return new Circle(new Point(centerX, centerY), radius);
        }

        public Circle HandleBaseCases(List<Point> bounderyPoints)
        {
            if (bounderyPoints.Count == 0)
            {
                return new Circle(new Point(0, 0), 0);
            }
            if (bounderyPoints.Count == 1)
            {
                return new Circle(bounderyPoints[0], 0);
            }
            if (bounderyPoints.Count == 2)
            {
                return CreateCircleFromTwoPoints(bounderyPoints[0], bounderyPoints[1]);
            }
            if (bounderyPoints.Count == 3)
            {
                return CreateCircleFromThreePoints(bounderyPoints[0], bounderyPoints[1], bounderyPoints[2]);
            }
            return new Circle(new Point(0, 0), 0);
        }

        public Circle FindMinCircleRecursion(Point[] points, int size, List<Point> bounderyPoints)
        {
            // base cases
            if (bounderyPoints.Count == 3 || size == 0)
            {
                return HandleBaseCases(bounderyPoints);
            }
            Point point = points[size - 1];
            Circle c = FindMinCircleRecursion(points, size - 1, bounderyPoints);
            if (CheckIfPointInCircle(c, point))
            {
                return c;
            }
            bounderyPoints.Add(point);
            return FindMinCircleRecursion(points, size - 1, bounderyPoints);
        }

        public Circle FindMinCircle(Point[] points, int size)
        {
            List<Point> bounderyPoints = new List<Point>();
            return FindMinCircleRecursion(points, size, bounderyPoints);
        }
    }
}

