using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace AnomalyDetection.Model
{
    public class FGXmlReader
    {
        public static PropertyList Reader(string path)
        {
            PropertyList data;
            using (FileStream fs = new FileStream(path, FileMode.Open))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(PropertyList));
                data = (PropertyList)serializer.Deserialize(fs);
            }

            return data;
        }
    }

    
    [XmlRoot(ElementName = "PropertyList")]
    public class PropertyList
    {
        [XmlElement(ElementName = "commant")]
        public string Comment { get; set; }

        [XmlElement(ElementName = "generic")]
        public Generic Generic { get; set; }
    }

    public class Generic
    {
        [XmlElement(ElementName = "output")]
        public InputOutput Output { get; set; }

        [XmlElement(ElementName = "input")]
        public InputOutput Input { get; set; }
    }

    public class InputOutput
    {
        [XmlElement(ElementName = "line_separator")]
        public string LineSeparator { get; set; }

        [XmlElement(ElementName = "var_separator")]
        public string VarSeparator { get; set; }

        [XmlElement(ElementName = "chunk")]
        public List<Chunk> Chunks { get; set; }
    }

    public class Chunk
    {
        [XmlElement(ElementName = "name")]
        public string Name { get; set; }

        [XmlElement(ElementName = "type")]
        public string Type { get; set; }

        [XmlElement(ElementName = "format")]
        public string Format { get; set; }

        [XmlElement(ElementName = "node")]
        public string Node { get; set; }
    }
}