using System.Linq;
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

        public override bool Equals(object obj)
        {
            var other = obj as Data;
            if (other == null)
                return false;

            if (this.Files.Count != other.Files.Count)
                return false;

            return (!Enumerable.Except(this.Files, other.Files).Any() || !Enumerable.Except(other.Files, this.Files).Any());
        }
    }
}
