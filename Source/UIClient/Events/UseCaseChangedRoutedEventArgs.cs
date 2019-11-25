using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using UIClient.Models;

namespace UIClient.Events
{
    public class UseCaseChangedRoutedEventArgs : RoutedEventArgs
    {
        public UseCaseModel Value { get; set; }
    }
    
}
