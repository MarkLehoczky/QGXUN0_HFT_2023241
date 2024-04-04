using QGXUN0_HFT_2023241.Models.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows.Data;

namespace QGXUN0_HFT_2023242.WPFClient.Converters
{
    [ValueConversion(typeof(IEnumerable<Book>), typeof(string))]
    class BookListToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is IEnumerable<Book> books) return string.Join("\n", books.Select(t => t.Title));

            else throw new ArgumentException($"Parameter is not correct '{nameof(IEnumerable<Book>)}' type", nameof(value));
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
