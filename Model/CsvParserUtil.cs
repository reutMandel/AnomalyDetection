
using System.Collections.Generic;
using System.Linq;

namespace AnomalyDetection.Model
{
    public class CsvParserUtil
    {

        public static Dictionary<string,List<double>> convertLinesToColumns (List<string> lines, Dictionary<string,int> csvNames) 
        {
            Dictionary<string, List<double>> columns = new Dictionary<string, List<double>>();
            foreach(var i in csvNames.Keys)
            {
                columns.Add(i, new List<double>());
            }

            for (int i =0; i< lines.Count; i++)
            {
                string[] values = lines[i].Split(',');
                for(int j = 0; j< values.Length; j++)
                {
                    string index = csvNames.FirstOrDefault(x => x.Value == j).Key;
                    columns[index].Add(double.Parse(values[j]));
                }
            }
            return columns;
        }

    }
}
