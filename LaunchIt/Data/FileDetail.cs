using System.IO;
using System.Xml.Serialization;

namespace LaunchIt.Data
{
    public class FileDetail
    {
        public string Name
        {
            get
            {
                return GetFileName(FilePath);
            }
        }
        
        [XmlText]
        public string FilePath { get; set; }
        
        [XmlAttribute("InvokeCount")]
        public int UsageCount { get; set; }

        public FileDetail(string path, int rank)
        {
            FilePath = path; UsageCount = rank;
        }
        public FileDetail()
        {

        }
                
        static string GetFileName(string filePath)
        {
            return new FileInfo(filePath).Name; ;
        }

        public override string ToString()
        {
            return string.Format("{0} - {1}", Name, FilePath);
        }
    }
}
