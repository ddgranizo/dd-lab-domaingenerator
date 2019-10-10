using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace UIClient.Events
{
    public class GuidValueChangedEventArgs : RoutedEventArgs
    {
        public Guid Value { get; set; }
    }
}
