using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace UIClient.Events
{
    public class CollapsedChangedEventArgs : RoutedEventArgs
    {
        public bool Data { get; set; }
    }
}
