using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace AnomalyDetection.Model
{
    public class AnomalyDetectionLogic
    {
        private const string filePath = @"ANOMALYALGORITHM.dll";

        public static object Direction { get; private set; }

        [DllImport(filePath, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr createTimeSeries(string CSVfileName);

        [DllImport(filePath, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr createTimeSeriesAnomalyDetector();

        [DllImport(filePath, CallingConvention = CallingConvention.Cdecl)]
        public static extern void learnNormal(IntPtr a, IntPtr ts);

        [DllImport(filePath, CallingConvention = CallingConvention.Cdecl)]
        public static extern int vectorSize(IntPtr v);

        [DllImport(filePath, CallingConvention = CallingConvention.Cdecl)]
        public static extern int getLineByIndex(IntPtr v, int index);


        [DllImport(filePath, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr getDescriptionByIndex(IntPtr v, int index);


        [DllImport(filePath, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr detect(IntPtr a, IntPtr ts);


        [DllImport(filePath, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr getShapeName();

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

                if (Math.Abs(currentValue) > maxValue)
                {
                    maxValue = currentValue;
                    maxField = item.Key;
                }
            }

            return maxField;
        }

        public static List<AnomalyReport> FindAnomalies(string csvPath, string csvLearnPath)
        {
            IntPtr learnTimeseries = createTimeSeries(csvLearnPath);
            IntPtr simpleAnomaly = createTimeSeriesAnomalyDetector();
            IntPtr timeseries = createTimeSeries(csvPath);
            learnNormal(simpleAnomaly, learnTimeseries);
            IntPtr anomalyVectorWrapper = detect(simpleAnomaly, timeseries);
            int size = vectorSize(anomalyVectorWrapper);
            List<AnomalyReport> anomalies = new List<AnomalyReport>();
            for (int i = 0; i < size; i++)
            {
                AnomalyReport anomaly = new AnomalyReport();
                anomaly.TimeStep = getLineByIndex(anomalyVectorWrapper, i);
                IntPtr si = getDescriptionByIndex(anomalyVectorWrapper, i);
                anomaly.Description = Marshal.PtrToStringAnsi(si);
                anomalies.Add(anomaly);
            }
            return anomalies;
        }

        public static string GetAnnotationShape()
        {
            return Marshal.PtrToStringAnsi(getShapeName());
        }
    }
}
