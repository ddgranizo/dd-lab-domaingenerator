using DomainGeneratorUI.Models.Methods;
using DomainGeneratorUI.Models.UseCases.Sentences.Base;
using DomainGeneratorUI.Viewmodels.UseCases.Sentences.Base;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace DomainGeneratorUI.Events
{
    public class UpdatedMethodParameterReferenceValueEventArgs : RoutedEventArgs
    {
        public MethodParameterReferenceValueViewModel OldValue { get; set; }
        public MethodParameterReferenceValueViewModel NewValue { get; set; }
    }
}
