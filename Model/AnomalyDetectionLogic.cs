using System.Collections.Generic;
using System.Linq;

namespace AnomalyDetection.Model
{
    public class AnomalyDetectionLogic
    {
        public static string FindCorrelated(Dictionary<string, List<double>> values, string fieldName)
        {
            double[] currentFieldValues = values[fieldName].ToArray();
            int size = currentFieldValues.Length;
            double maxValue = 0;
            string maxField = fieldName;
            foreach (var item in values)
            {
                if (item.Key.Equals(fieldName))
                    continue;

                double currentValue = AnomalyDetectionUtil.Pearson(currentFieldValues, item.Value.ToArray(), size);

                if (currentValue > maxValue)
                {
                    maxValue = currentValue;
                    maxField = item.Key;
                }
            }

            return maxField;
        }

        public static LinearReg FindLinearReg(Dictionary<string, List<double>> values, string fieldName1, string fieldName2)
        {
            double[] currentFieldValues = values[fieldName1].ToArray();
            double[] corrlatedFieldValues = values[fieldName2].ToArray();
            int size = currentFieldValues.Length;
            Point[] points = new Point[size];
            for (int i = 0; i < size; i++)
            {
                points[i] = new Point(currentFieldValues[i], corrlatedFieldValues[i]);
            }

            return new LinearReg
            {
                Line = AnomalyDetectionUtil.LinearReg(points, size),
                Points = points.ToList()
            };
        }
    }
}
