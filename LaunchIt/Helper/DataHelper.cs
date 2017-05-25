using LaunchIt.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace LaunchIt.Helper
{
    public class DataHelper
    {
        private static DataContext _context = new DataContext();

        const char SPLIT_CHAR = ';';

        public DataHelper()
        {
            if (_context.FileDetailList.Count <= 0)
                IndexFiles();
        }

        List<string> GetValues(string value)
        {
            var list = new List<string>();
            if (value.Contains(SPLIT_CHAR))
            {
                var values = value.Split(new char[] { SPLIT_CHAR }, StringSplitOptions.RemoveEmptyEntries);
                list.AddRange(values);
            }
            else
            {
                list.Add(value);
            }

            return list;
        }

        public List<FileDetail> IndexFiles()
        {
            var fileRanks = new List<FileDetail>();
            foreach (var fPaths in _context.Settings.SourcePaths)
            {
                var fullPath = Environment.ExpandEnvironmentVariables(fPaths.Path);

                var types = GetValues(fPaths.Types);
                var dInfo = new DirectoryInfo(fullPath);
                if (!dInfo.Exists)
                    continue;

                foreach (var fType in types)
                {
                    try
                    {
                        var searchOpt = fPaths.RecursiveSearch ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly;
                        var filePaths = dInfo.GetFiles(fType, searchOpt).Select(f => f.FullName);
                        foreach (var path in filePaths)
                        {
                            var duplicate = fileRanks.FirstOrDefault(fr => fr.FilePath.Equals(path, StringComparison.InvariantCultureIgnoreCase));
                            if (duplicate == null)
                            {
                                var existing = _context.FileDetailList.FirstOrDefault(fr => fr.FilePath.Equals(path, StringComparison.InvariantCultureIgnoreCase));
                                if (existing != null)
                                    fileRanks.Add(existing);
                                else
                                    fileRanks.Add(new FileDetail(path));
                            }
                        }
                    }
                    catch (Exception exp)
                    {
                    }
                }
            }

            _context.FileDetailList = fileRanks;
            _context.SaveData();
            return _context.FileDetailList;
        }

        public IList<FileDetail> FileDetailList
        {
            get
            {
                return _context.FileDetailList;
            }
        }

        public List<UsageDetail> UsageStatistics
        {
            get
            {
                return _context.UsageStatitics.UsageDetails;
            }
        }

        internal void SaveUsageStatitics()
        {
            _context.SaveUsageData();

            this.UsageStatistics.ForEach(u =>
            {
                var item = this.FileDetailList.FirstOrDefault(f => string.Equals(f.FilePath, u.FullPath, System.StringComparison.OrdinalIgnoreCase));
                if (item != null)
                    item.UsedCount = u.UsageCount;
            });
        }

        public HotKey HotKey
        {
            get
            {
                return _context.Settings.HotKey;
            }
        }

        public Settings GetSettings()
        {
            return _context.Settings;
        }

        public bool SaveSettings()
        {
            return _context.SaveSettings();
        }
    }
}
