using DD.DomainGenerator.DeployActions.Base;
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
            if (state == DeployActionUnit.DeployState.QueuedForExecution)
            {
                color = "#FFFF84";
            }
            else if (state == DeployActionUnit.DeployState.Executing)
            {
                color = "#FFFFFF";
            }
            else if (state == DeployActionUnit.DeployState.Error)
            {
                color = "#FF0000";
            }
            else if (state == DeployActionUnit.DeployState.Completed)
            {
                color = "#aaff80";
            }
            var brush = (SolidColorBrush)(new BrushConverter().ConvertFrom(color));
            return brush;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }

    }
}
