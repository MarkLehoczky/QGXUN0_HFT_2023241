using QGXUN0_HFT_2023241.Models.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows.Data;

namespace QGXUN0_HFT_2023242.WPFClient.Converters
{
    [ValueConversion(typeof(IEnumerable<Collection>), typeof(string))]
    class CollectionListToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is IEnumerable<Collection> items) return string.Join("\n", items.Select(t => t.CollectionName));

            else throw new ArgumentException($"Parameter is not correct '{nameof(IEnumerable<Collection>)}' type", nameof(value));
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
