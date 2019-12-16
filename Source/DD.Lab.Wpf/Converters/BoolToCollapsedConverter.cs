using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Data;

namespace DD.Lab.Wpf.Converters
{
    public class BoolToCollapsedConverter : IValueConverter
    {
        public BoolToCollapsedConverter()
        {
        }

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            bool applyOpposite = parameter != null && parameter.ToString().ToLower() == "false";
            var val = (bool)value;
            bool show = applyOpposite ? !val : val;
            if (show)
            {
                return Visibility.Visible;
            }
            return Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
