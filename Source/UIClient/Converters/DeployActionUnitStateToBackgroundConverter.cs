using DD.DomainGenerator.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Data;
using System.Windows.Media;

namespace UIClient.Converters
{
    public class DeployActionUnitStateToBackgroundConverter : IValueConverter
    {
        public DeployActionUnitStateToBackgroundConverter()
        {
        }

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var state = (DeployActionUnit.DeployState)value;
            string color = "#FFFFFF";
            if (state == DeployActionUnit.DeployState.Queued)
            {
                color = "#FFFF84";
            }
            else if (state == DeployActionUnit.DeployState.Executing)
            {
                color = "#FFFFFF";
            }
            else if (state == DeployActionUnit.DeployState.Completed)
            {
                color = "#aaff80";
            }
            var brush = (SolidColorBrush)(new BrushConverter().ConvertFrom(color));
            return color;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }

    }
}
