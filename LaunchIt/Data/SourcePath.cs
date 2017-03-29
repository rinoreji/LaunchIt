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

        public override bool Equals(object obj)
        {
            var other = obj as SourcePath;
            if (other == null)
                return false;

            if (!string.Equals(Path, other.Path, System.StringComparison.OrdinalIgnoreCase))
                return false;

            if (!string.Equals(Types, other.Types, System.StringComparison.OrdinalIgnoreCase))
                return false;

            return this.RecursiveSearch == other.RecursiveSearch;
        }
    }
}
