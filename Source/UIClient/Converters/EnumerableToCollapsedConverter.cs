using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Data;

namespace UIClient.Converters
{
    public class EnumerableToCollapsedConverter : IValueConverter
    {
        public EnumerableToCollapsedConverter()
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
            ICollection list = value as ICollection;
            bool positive = list != null && list.Count > 0;
            bool show =  applyOpposite
                    ? !positive
                    : positive;

            return show 
                ? Visibility.Visible
                : Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
