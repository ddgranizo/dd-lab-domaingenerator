using DD.Lab.Wpf.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace DomainGeneratorUI.Viewmodels.UseCases
{
    public class UseCaseSentenceCollectionViewmodel : BaseViewModel
    {
        public List<UseCaseSentenceViewmodel> Sentences { get { return GetValue<List<UseCaseSentenceViewmodel>>(); } set { SetValue(value); UpdateListToCollection(value, SentencesCollection); } }
        public ObservableCollection<UseCaseSentenceViewmodel> SentencesCollection { get; set; } = new ObservableCollection<UseCaseSentenceViewmodel>();
    }
}
