using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace UIClient.Events
{
    public class DecimalValueChangedEventArgs : RoutedEventArgs
    {
        public decimal Value { get; set; }
    }
}
