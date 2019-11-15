using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Data;

namespace UIClient.Converters
{
    public class StringNullOrEmptyToCollapsedConverter : IValueConverter
    {
        public StringNullOrEmptyToCollapsedConverter()
        {
        }

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            bool applyOpposite = parameter != null && parameter.ToString().ToLower() == "false";

            if (value == null || string.IsNullOrEmpty((string)value))
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
