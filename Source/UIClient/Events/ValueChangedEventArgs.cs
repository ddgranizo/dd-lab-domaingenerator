using DD.DomainGenerator.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using static DD.DomainGenerator.Models.ActionParameterDefinition;

namespace UIClient.Events
{

    public class ValueChangedEventArgs : RoutedEventArgs
    {
        public object Data { get; set; }
        public ActionParameterDefinition ParameterDefinition { get; set; }
        
    }
}
