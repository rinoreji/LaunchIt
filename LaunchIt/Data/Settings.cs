using System.Linq;
using System.Collections.Generic;
using System.Xml.Serialization;
using System;

namespace LaunchIt.Data
{
    public struct HotKey
    {
        public int ModifierKey { get; set; }
        public int Key { get; set; }
    }

    public class Settings
    {
        [XmlArray("ImportSettings")]
        [XmlArrayItem("ImportSource")]
        public List<SourcePath> SourcePaths { get; set; }

        public HotKey HotKey { get; set; }

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
