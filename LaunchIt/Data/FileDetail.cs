using System.Drawing;
using System.IO;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
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

        [XmlIgnore]
        public int UsedCount { get; set; }

        public FileDetail(string path)
        {
            FilePath = path;
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

        public override bool Equals(object obj)
        {
            var other = obj as FileDetail;
            if (other == null)
                return false;
            
            return (!string.Equals(this.FilePath, other.FilePath, System.StringComparison.OrdinalIgnoreCase));
        }
    }
}
