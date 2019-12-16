using DD.Lab.GenericUI.Core.Models.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace UIClientV2.Events
{
    public class SelectedDataRowEventArgs : RoutedEventArgs
    {
        public DataRowModel Data { get; set; }
    }

}
