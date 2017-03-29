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
                                    fileRanks.Add(new FileDetail(path, 0));
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

        public IEnumerable<FileDetail> GetFileDetailList()
        {
            return _context.FileDetailList;
        }
    }
}
