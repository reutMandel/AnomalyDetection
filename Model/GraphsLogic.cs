using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AnomalyDetection.Model
{
    public class GraphsLogic : Notify
    {
        public Dictionary<string, List<double>> Columns { get; set; }
        public List<AnomalyReport> Anomalies { get; set; }
        public IAlgorithm AlgorithmProperties { get; set; }

        public event NotifyEventHandler LoadDllCompleted;

        public List<double> GetValuesByFieldName(string fieldName)
        {
            return Columns[fieldName];
        }

        public string GetCorrelatedField(string fieldName)
        {
            return AnomalyDetectionLogic.FindCorrelated(Columns, fieldName);
        }

        public AlgorithmProperties GetAlgorithmProperties(string field1, string field2)
        {
            double[] currentFieldValues = Columns[field1].ToArray();
            double[] corrlatedFieldValues = Columns[field2].ToArray();
            int size = currentFieldValues.Length;
            Point[] points = new Point[size];
            for (int i = 0; i < size; i++)
            {
                points[i] = new Point((float)currentFieldValues[i], (float)corrlatedFieldValues[i]);
            }
            return this.AlgorithmProperties.GetAlgorithmProperties(points);
        }

        public void DllLoad(FilesDataProperties filesData)
        {
            string fileToCopy = filesData.DllPath;
            string destinationDirectory = Directory.GetCurrentDirectory() + "\\";
            string fullPath = destinationDirectory + "ANOMALYALGORITHM.dll";
            if (File.Exists(fullPath))
            {
                File.Delete(fullPath);
            }
            File.Copy(fileToCopy, fullPath);
            Anomalies = AnomalyDetectionLogic.FindAnomalies(filesData.CsvPath, filesData.LearnCsvPath);
            LoadDllCompleted?.Invoke();
            SetAnnotation();
        }

        public List<string> GetAnomaliesAsString()
        {
            List<string> anomaliesStr = new List<string>();
            Anomalies.ForEach(x => anomaliesStr.Add(x.Description + " : " + x.TimeStep));
            return anomaliesStr;
        }

        public void SetAnnotation()
        {
            string shape = AnomalyDetectionLogic.GetAnnotationShape();
            if (shape.Equals("line"))
                this.AlgorithmProperties = new LineAlgorithm();
            else if (shape.Equals("circle"))
                this.AlgorithmProperties = new CircleAlgorithm();
        }

        public List<Point> GetAnnomaliesPoints(string field1, string field2, List<Point> points)
        {
            List<int> timesteps = Anomalies.Where(x => x.Description.Contains(field1) && x.Description.Contains(field2))
                .Select(x => x.TimeStep).ToList();
            List<Point> anomaliesP = new List<Point>();
            timesteps.ForEach(x => anomaliesP.Add(points[x]));
            return anomaliesP;
        }
    }
}