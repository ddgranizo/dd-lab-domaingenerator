using DomainGeneratorUI.Models.UseCases.Sentences.Base;
using DomainGeneratorUI.Viewmodels.UseCases.Sentences.Base;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace DomainGeneratorUI.Events
{
    public class UpdatedUseCaseSentenceEventArgs: RoutedEventArgs
    {
        public UseCaseSentenceViewModel UseCaseViewModel { get; set; }
        public Dictionary<string, object> Values { get; set; }
        public UseCaseSentence UseCase { get; set; }
    }
}
