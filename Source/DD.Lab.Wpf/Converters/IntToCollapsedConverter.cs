using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Data;

namespace DD.Lab.Wpf.Converters
{
    public class IntToCollapsedConverter : IValueConverter
    {
        public IntToCollapsedConverter()
        {
        }

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var val = (int)value;
            if (parameter == null)
            {
                if (val == 0)
                {
                    return Visibility.Collapsed;
                }
                return Visibility.Visible;
            }
            var intValue = 0;
            if (parameter is Int16 || parameter is Int32)
            {
                intValue = (int)parameter; ;
            }
            else
            {
                if (!int.TryParse((string)parameter, out int resultParseInt))
                {
                    return Visibility.Collapsed;
                }
                if (resultParseInt == val)
                {
                    return Visibility.Visible;
                }
            }
            return Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
