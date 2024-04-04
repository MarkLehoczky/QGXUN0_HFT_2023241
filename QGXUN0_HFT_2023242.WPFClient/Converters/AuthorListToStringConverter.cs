using QGXUN0_HFT_2023241.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Data;
using System.Globalization;

namespace QGXUN0_HFT_2023242.WPFClient.Converters
{
    [ValueConversion(typeof(IEnumerable<Author>), typeof(string))]
    class AuthorListToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is IEnumerable<Author> items) return string.Join("\n", items.Select(t => t.AuthorName));

            else throw new ArgumentException($"Parameter is not correct '{nameof(IEnumerable<Author>)}' type", nameof(value));
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
