using LaunchIt.Data;
using System.Diagnostics;
using System.IO;

namespace LaunchIt.Core
{
    class Launcher
    {
        public static void Launch(FileDetail fileDetail)
        {
            if (File.Exists(fileDetail.FilePath))
            {
                using (var process = new Process())
                {
                    process.StartInfo = new ProcessStartInfo(fileDetail.FilePath);
                    process.Start();
                }
            }
        }
    }
}
