using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace DD.Lab.Wpf.Inputs.Events
{
    public class DateTimeValueChangedEventArgs : RoutedEventArgs
    {
        public DateTime Value { get; set; }
    }
}
