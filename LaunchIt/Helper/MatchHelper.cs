using LaunchIt.Data;
using System.Collections.Generic;

namespace LaunchIt.Helper
{
    public class MatchHelper
    {
        public static int MatchScore(string source, string query)
        {
            if (source.Contains(query))
            {
                return 1;
            }
            return 0;
        }

        public static IList<FileDetail> GetMatchedData<T>(IList<FileDetail> source, string query)
        {
            var result = new List<FileDetail>();
            foreach (var item in source)
            {
                if (MatchScore(item.FilePath, query) > 0)
                    result.Add(item);
            }

            return result;
        }
    }
}
