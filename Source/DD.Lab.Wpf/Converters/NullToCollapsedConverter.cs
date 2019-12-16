using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Data;

namespace DD.Lab.Wpf.Converters
{
    public class NullToCollapsedConverter : IValueConverter
    {
        public NullToCollapsedConverter()
        {
        }

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            bool applyOpposite = parameter != null && parameter.ToString().ToLower() == "false";

            if (value == null)
            {
                return applyOpposite 
                    ? Visibility.Visible
                    : Visibility.Collapsed;
            }
            return applyOpposite
                    ? Visibility.Collapsed
                    : Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
