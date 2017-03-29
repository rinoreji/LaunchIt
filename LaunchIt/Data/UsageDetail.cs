using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace LaunchIt.Data
{
    public class UsageDetail
    {
        [XmlText]
        public string FullPath { get; set; }

        [XmlAttribute("InvokeCount")]
        public int UsageCount { get; set; }

        public UsageDetail()
        {

        }

        public UsageDetail(string path, int usage)
        {
            FullPath = path; UsageCount = usage;
        }

        public override bool Equals(object obj)
        {
            var other = obj as UsageDetail;
            if (other == null)
                return false;
            if (!string.Equals(this.FullPath, other.FullPath, StringComparison.OrdinalIgnoreCase))
                return false;

            return this.UsageCount == other.UsageCount;
        }
    }
}
