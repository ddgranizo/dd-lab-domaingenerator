using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Data;
using static UIClient.ViewModels.MainViewModel;

namespace UIClient.Converters
{

    public class DetailViewSelectorToCollapsedConverter : IValueConverter
    {
        public DetailViewSelectorToCollapsedConverter()
        {
        }

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var valueTyped = (DetailViewSelector)value;
            var valueString = valueTyped.ToString().ToLowerInvariant();

            var stringParameter = parameter.ToString().ToLower();
            var show = stringParameter == valueString
                || (int.TryParse(stringParameter, out int number)
                && int.Parse(stringParameter) == (int)valueTyped);
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
