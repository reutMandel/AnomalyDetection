using System.Collections.Generic;

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
            foreach(var item in values)
            {
                if (item.Key.Equals(fieldName))
                    continue;

                double currentValue = AnomalyDetectionUtil.Pearson(currentFieldValues, item.Value.ToArray(), size);

                if(currentValue > maxValue)
                {
                    maxValue = currentValue;
                    maxField = item.Key;
                }
            }

            return maxField;
        }
    }
}
