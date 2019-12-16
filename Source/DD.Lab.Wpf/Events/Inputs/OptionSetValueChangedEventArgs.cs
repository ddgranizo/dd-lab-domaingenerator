using DD.Lab.Wpf.Models.Inputs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace DD.Lab.Wpf.Inputs.Events
{
    public class OptionSetValueChangedEventArgs : RoutedEventArgs
    {
        public OptionSetValue Value { get; set; }
    }
}
