using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace UIClient.Events
{
    public class BooleanValueChangedEventArgs: RoutedEventArgs
    {
        public bool Value { get; set; }
    }
}
