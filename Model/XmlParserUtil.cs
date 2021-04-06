using System.Collections.Generic;

namespace AnomalyDetection.Model
{
   public class XmlParserUtil
    {
        public static Dictionary<string, int> Parse(PropertyList propertyList)
        {
            Dictionary<string, int> names = new Dictionary<string, int>();
            List<Chunk> chunks = propertyList.Generic.Input.Chunks;
            for (int i = 0; i < chunks.Count; i++)
            {
                if (!names.ContainsKey(chunks[i].Name))
                    names.Add(chunks[i].Name, i);
            }
            return names;
        }
    }
}