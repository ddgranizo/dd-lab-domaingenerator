using DD.Lab.Wpf.ViewModels.Base;
using DomainGeneratorUI.Viewmodels.UseCases.Sentences.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace DomainGeneratorUI.Viewmodels.UseCases
{
    public class UseCaseSentenceCollectionViewmodel : BaseViewModel
    {
        public List<UseCaseSentenceViewModel> Sentences { get { return GetValue<List<UseCaseSentenceViewModel>>(); } set { SetValue(value); UpdateListToCollection(value, SentencesCollection); } }
        public ObservableCollection<UseCaseSentenceViewModel> SentencesCollection { get; set; } = new ObservableCollection<UseCaseSentenceViewModel>();
    }
}
