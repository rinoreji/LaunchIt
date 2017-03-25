using System.Collections.Generic;
using System.Xml.Serialization;

namespace LaunchIt.Data
{
    public class Data
    {
        [XmlArray("Files")]
        [XmlArrayItem("Path")]
        public List<FileDetail> Files { get; set; }

        public Data()
        {
            Files = new List<FileDetail>();
        }
    }
}
