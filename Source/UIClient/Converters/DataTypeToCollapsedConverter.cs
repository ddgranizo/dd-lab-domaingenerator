using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Data;
using static DD.DomainGenerator.Models.ActionParameterDefinition;

namespace UIClient.Converters
{
    public class DataTypeToCollapsedConverter : IValueConverter
    {
        public DataTypeToCollapsedConverter()
        {
        }

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var valueTyped = (TypeValue)value;
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
