
using System.Collections.Generic;
using System.Linq;

namespace AnomalyDetection.Model
{
    public class CsvParserUtil
    {

        public static Dictionary<string,List<string>> convertLinesToColumns (List<string> lines, Dictionary<string,int> csvNames) 
        {
            Dictionary<string, List<string>> columns = new Dictionary<string, List<string>>();
            foreach(var i in csvNames.Keys)
            {
                columns.Add(i, new List<string>());
            }

            for (int i =0; i< lines.Count; i++)
            {
                string[] values = lines[i].Split(',');
                for(int j = 0; j< values.Length; j++)
                {
                    string index = csvNames.FirstOrDefault(x => x.Value == j).Key;
                    columns[index].Add(values[j]);
                }
            }
            return columns;
        }

    }
}
