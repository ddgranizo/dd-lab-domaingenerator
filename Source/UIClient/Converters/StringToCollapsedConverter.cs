﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Data;

namespace UIClient.Converters
{
    public class StringToCollapsedConverter : IValueConverter
    {
        public StringToCollapsedConverter()
        {
        }

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            bool applyOpposite = parameter != null && parameter.ToString().ToLower() == "false";
            bool show = applyOpposite ? string.IsNullOrEmpty((string)value) : !string.IsNullOrEmpty((string)value);
            if (show)
            {
                return Visibility.Collapsed;
            }
            return Visibility.Hidden;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
