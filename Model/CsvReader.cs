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
                int i = 0;
                string currentLine;
                while ((currentLine = sr.ReadLine()) != null)
                {
                    if (i == 0)
                    {
                        i++;
                        continue;
                    }

                    lines.Add(currentLine + "\n");
                }
            }

            return lines;
        }
    }
}
