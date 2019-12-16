using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace DD.Lab.Wpf.Inputs.Events
{
    public class IntValueChangedEventArgs : RoutedEventArgs
    {
        public int Value { get; set; }
    }
}
