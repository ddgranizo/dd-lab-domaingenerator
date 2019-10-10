using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace UIClient.Events
{
    public class IntValueChangedEventArgs : RoutedEventArgs
    {
        public int Value { get; set; }
    }
}
