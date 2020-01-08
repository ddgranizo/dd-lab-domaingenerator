using DD.Lab.Wpf.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using static DomainGeneratorUI.Models.UseCases.Sentences.Base.UseCaseSentence;

namespace DomainGeneratorUI.Viewmodels.UseCases.Sentences.Base
{
    public class UseCaseSentenceViewModel : BaseViewModel
    {

        public SentenceType Type { get { return GetValue<SentenceType>(); } set { SetValue(value); } }

        public string Name { get { return GetValue<string>(); } set { SetValue(value); } }
        public string DisplayName { get { return GetValue<string>(); } set { SetValue(value); } }
        public string Description { get { return GetValue<string>(); } set { SetValue(value); } }


        public List<SentenceInputParameterViewModel> InputParameters { get { return GetValue<List<SentenceInputParameterViewModel>>(); } set { SetValue(value);  UpdateListToCollection(value, InputParametetersCollection); } }
        public ObservableCollection<SentenceInputParameterViewModel> InputParametetersCollection { get; set; } = new ObservableCollection<SentenceInputParameterViewModel>();

        public List<SentenceInputParameterViewModel> OutputParameters { get { return GetValue<List<SentenceInputParameterViewModel>>(); } set { SetValue(value); UpdateListToCollection(value, OutputParametersCollection); } }
        public ObservableCollection<SentenceInputParameterViewModel> OutputParametersCollection { get; set; } = new ObservableCollection<SentenceInputParameterViewModel>();

    }
}
