
using DD.Lab.Wpf.Drm.Models.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace DD.Lab.Wpf.Drm.Events
{
    public class SelectedDataRowEventArgs : RoutedEventArgs
    {
        public string LogicalName { get; set; }
        public DataRecord Data { get; set; }
    }

}
