using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace LaunchIt.Data
{
    public class UsageStatistics
    {
        [XmlArray("Statistics")]
        [XmlArrayItem("Path")]
        public List<UsageDetail> UsageDetails { get; set; }
    }
}
