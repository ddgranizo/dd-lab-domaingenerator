using DD.Lab.Wpf.ViewModels.Base;
using DomainGeneratorUI.Models.Methods;
using DomainGeneratorUI.Viewmodels.Methods;
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


        public List<MethodParameterViewModel> InputParameters { get { return GetValue<List<MethodParameterViewModel>>(); } set { SetValue(value);  UpdateListToCollection(value, InputParametetersCollection); } }
        public ObservableCollection<MethodParameterViewModel> InputParametetersCollection { get; set; } = new ObservableCollection<MethodParameterViewModel>();

        public List<MethodParameterViewModel> OutputParameters { get { return GetValue<List<MethodParameterViewModel>>(); } set { SetValue(value); UpdateListToCollection(value, OutputParametersCollection); } }
        public ObservableCollection<MethodParameterViewModel> OutputParametersCollection { get; set; } = new ObservableCollection<MethodParameterViewModel>();

        //public List<SentenceInputReferencedParameterViewModel> InputReferencedParameters { get { return GetValue<List<SentenceInputReferencedParameterViewModel>>(); } set { SetValue(value); UpdateListToCollection(value, InputReferencedParametetersCollection); } }
        //public ObservableCollection<SentenceInputReferencedParameterViewModel> InputReferencedParametetersCollection { get; set; } = new ObservableCollection<SentenceInputReferencedParameterViewModel>();

        //public List<SentenceOutputReferencedParameterViewModel> OutputReferencedParameters { get { return GetValue<List<SentenceOutputReferencedParameterViewModel>>(); } set { SetValue(value); UpdateListToCollection(value, OutputReferencedParametersCollection); } }
        //public ObservableCollection<SentenceOutputReferencedParameterViewModel> OutputReferencedParametersCollection { get; set; } = new ObservableCollection<SentenceOutputReferencedParameterViewModel>();

        public List<MethodParameterReferenceValueViewModel> ReferencedInputParametersValues { get { return GetValue<List<MethodParameterReferenceValueViewModel>>(); } set { SetValue(value); UpdateListToCollection(value, ReferencedInputParametersValuesCollection); } }
        public ObservableCollection<MethodParameterReferenceValueViewModel> ReferencedInputParametersValuesCollection { get; set; } = new ObservableCollection<MethodParameterReferenceValueViewModel>();


        public Dictionary<string, object> Values { get { return GetValue<Dictionary<string, object>>(); } set { SetValue(value); } }

    }
}
