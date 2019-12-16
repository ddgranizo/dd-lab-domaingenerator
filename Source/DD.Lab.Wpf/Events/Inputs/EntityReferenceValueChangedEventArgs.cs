using DD.Lab.Wpf.Models.Inputs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace DD.Lab.Wpf.Inputs.Events
{
    public class EntityReferenceValueChangedEventArgs : RoutedEventArgs
    {
        public EntityReferenceValue Value { get; set; }
    }
}
