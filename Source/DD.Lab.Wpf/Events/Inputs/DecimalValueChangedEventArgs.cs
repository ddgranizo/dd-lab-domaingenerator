using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace DD.Lab.Wpf.Inputs.Events
{
    public class DecimalValueChangedEventArgs : RoutedEventArgs
    {
        public decimal Value { get; set; }
    }
}
