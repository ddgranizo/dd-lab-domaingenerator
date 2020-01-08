using DomainGeneratorUI.Models.UseCases;
using DomainGeneratorUI.Viewmodels.UseCases;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace DomainGeneratorUI.Inputs
{
    public class UseCaseSentenceCollectionManagerInputData : RoutedEventArgs
    {
        public UseCaseSentenceCollectionViewmodel SentenceCollection { get; set; }
    }
}
