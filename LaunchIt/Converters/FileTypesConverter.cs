using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Data;

namespace LaunchIt.Converters
{
    public class FileTypesConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var data = value as string;
            if (String.IsNullOrWhiteSpace(data))
            {
                return new List<string>();
            }
            return data.Split(new Char[] { ';' }, StringSplitOptions.RemoveEmptyEntries).ToList();
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
