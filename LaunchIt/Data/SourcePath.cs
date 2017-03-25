using System.Xml.Serialization;

namespace LaunchIt.Data
{
    public class SourcePath
    {
        [XmlText]
        public string Path { get; set; }

        [XmlAttribute("Types")]
        public string Types { get; set; }

        [XmlAttribute("Recursive")]
        public bool RecursiveSearch { get; set; }
    }
}
