using DomainGeneratorUI.Viewmodels.Methods;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace DomainGeneratorUI.Events
{

    public class OnModifiedMethodParameterListEventArgs : RoutedEventArgs
    {
        public List<MethodParameterViewmodel> Data { get; set; }
    }
}
