using System.Linq;
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

        public override bool Equals(object obj)
        {
            var other = obj as Settings;

            return !Enumerable.Except(SourcePaths, other.SourcePaths).Any() || !Enumerable.Except(other.SourcePaths, this.SourcePaths).Any();
        }
    }
}
