using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace DD.Lab.Wpf.Inputs.Events
{
    public class DoubleValueChangedEventArgs : RoutedEventArgs
    {
        public double Value { get; set; }
    }
}
