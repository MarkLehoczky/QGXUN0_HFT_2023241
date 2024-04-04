using QGXUN0_HFT_2023242.WPFClient.ViewModels;
using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace QGXUN0_HFT_2023242.WPFClient.Converters
{
    [ValueConversion(typeof(ActiveMenu), typeof(GridLength))]
    class ActiveMenuToSubMenuHeightConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is ActiveMenu menu)

                if (menu == ActiveMenu.MAINMENU) return new GridLength(0, GridUnitType.Star);
                else return new GridLength(1, GridUnitType.Star);

            else throw new ArgumentException($"Parameter is not correct '{nameof(ActiveMenu)}' type", nameof(value));
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is GridLength length)

                if (length.Value > 0) return ActiveMenu.SUBMENU;
                else return ActiveMenu.MAINMENU;

            else throw new ArgumentException($"Parameter is not correct '{nameof(GridLength)}' type", nameof(value));
        }
    }
}
