using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace DD.Lab.Wpf.Drm.Events
{
    public class OnConfirmedSelectionEventArgs : RoutedEventArgs
    {
        public Guid Data { get; set; }
    }
}
