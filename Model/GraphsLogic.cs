using System.Collections.Generic;

namespace AnomalyDetection.Model
{
    public class GraphsLogic : Notify
    {
       public Dictionary<string, List<string>> Columns { get; set; }

       public List<string> GetValuesByFieldName (string fieldName)
       {
            return Columns[fieldName];
       }    
    }
}
