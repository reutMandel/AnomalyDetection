using System.Collections.Generic;
using System.IO;

namespace AnomalyDetection.Model
{
    public class CsvReader
    {
        public static List<string> ReadCsvFile(string path)
        {
            List<string> lines = new List<string>();

            using (StreamReader sr = new StreamReader(path))
            {
                string currentLine;
                while ((currentLine = sr.ReadLine()) != null)
                {
                    lines.Add(currentLine);
                }
            }

            return lines;
        }
    }
}
