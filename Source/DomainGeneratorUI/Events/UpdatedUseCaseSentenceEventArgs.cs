using DomainGeneratorUI.Models.Methods;
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
        public enum UpdateType
        {
            Sentence = 1,
            InputParameters = 2,
        }
        public UseCaseSentenceViewModel UseCaseViewModel { get; set; }
        public List<MethodParameterReferenceValueViewModel> Parameters { get; set; }
        public UseCaseSentence UseCase { get; set; }
        public UpdateType Type { get; set; }
    }
}
