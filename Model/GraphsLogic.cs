using System.Collections.Generic;

namespace AnomalyDetection.Model
{
    public class GraphsLogic : Notify
    {
        public Dictionary<string, List<double>> Columns { get; set; }

        public List<double> GetValuesByFieldName(string fieldName)
        {
            return Columns[fieldName];
        }

        public string GetCorrelatedField(string fieldName)
        {
            return AnomalyDetectionLogic.FindCorrelated(Columns, fieldName);
        }

        public LinearReg GetLinearReg(string field1, string field2)
        {
            return AnomalyDetectionLogic.FindLinearReg(Columns, field1, field2);
        }
    }
}
