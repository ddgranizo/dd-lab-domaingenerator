using DD.Lab.Wpf.Models.Inputs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace DD.Lab.Wpf.Inputs.Events
{

    public class ValueChangedEventArgs : RoutedEventArgs
    {
        public object Data { get; set; }
        public GenericFormInputModel Model { get; set; }
    }
}
