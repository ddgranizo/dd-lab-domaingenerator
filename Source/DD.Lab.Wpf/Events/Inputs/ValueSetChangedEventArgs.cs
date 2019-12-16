using DD.Lab.Wpf.Models.Inputs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace DD.Lab.Wpf.Inputs.Events
{

    public class ValueSetChangedEventArgs : RoutedEventArgs
    {
        public Dictionary<string, object> Data { get; set; }
        public bool IsDataCompleted { get; set; }
    }
}
