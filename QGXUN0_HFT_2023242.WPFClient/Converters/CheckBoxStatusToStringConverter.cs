using System;
using System.Globalization;
using System.Windows.Data;

namespace QGXUN0_HFT_2023242.WPFClient.Converters
{
    [ValueConversion(typeof(bool?), typeof(string))]
    class CheckBoxStatusToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return "null";
            else if (value is bool decision)
                if (decision) return "true";
                else return "false";

            else throw new ArgumentException($"Parameter is not correct '{nameof(Nullable<Boolean>)}' type", nameof(value));
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string str)
                if (str == "null") return null;
                else if (str == "true") return true;
                else return false;

            else throw new ArgumentException($"Parameter is not correct '{nameof(String)}' type", nameof(value));
        }
    }
}
