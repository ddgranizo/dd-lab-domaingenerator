using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace DD.Lab.Wpf.Inputs.Events
{
    public class StringValueChangedEventArgs : RoutedEventArgs
    {
        public string Value { get; set; }
    }
}
