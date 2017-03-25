using System.Collections.Generic;
using System.Xml.Serialization;

namespace LaunchIt.Data
{
    public class Settings
    {
        [XmlArray("ImportSettings")]
        [XmlArrayItem("ImportSource")]
        public List<SourcePath> SourcePaths { get; set; }


        public Settings()
        {
            SourcePaths = new List<SourcePath>();
        }
    }
}
