using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Data;

namespace DD.Lab.Wpf.Converters
{

    public class EnumSelectorToCollapsedConverter<T>  : DependencyObject,  IValueConverter where T : Enum
    {
        public T EnumValue
        {
            get => (T)GetValue(ExampleProperty);
            set => SetValue(ExampleProperty, value);
        }
        public static readonly DependencyProperty ExampleProperty =
            DependencyProperty.Register(nameof(EnumValue), typeof(T), typeof(EnumSelectorToCollapsedConverter<T>), new PropertyMetadata(null));

        public EnumSelectorToCollapsedConverter()
        {
        }

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var valueTyped = (T)value;
            var valueString = valueTyped.ToString().ToLowerInvariant();

            if (parameter == null)
            {
                if (valueString == "0")
                {
                    return Visibility.Collapsed;
                }
                return Visibility.Visible;
            }
            var stringParameter = parameter.ToString().ToLower();
            var show = stringParameter == valueString;
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
