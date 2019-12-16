using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Data;
using System.Windows.Media;

namespace DD.Lab.Wpf.Converters
{
    public class ActionStateToBackgroundConverter : IValueConverter
    {
        public ActionStateToBackgroundConverter()
        {
        }

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            //var state = (ActionExecution.ActionExecutionState)value;
            //string color = "#FFFFFF";
            //if (state == ActionExecution.ActionExecutionState.Queued)
            //{
            //    color = "#FFFF84";
            //}
            //else if (state == ActionExecution.ActionExecutionState.Executing)
            //{
            //    color = "#FFFFFF";
            //}
            //else if (state == ActionExecution.ActionExecutionState.Executed)
            //{
            //    color = "#aaff80";
            //}
            //var brush = (SolidColorBrush)(new BrushConverter().ConvertFrom(color));
            //return color;
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }

    }
}
