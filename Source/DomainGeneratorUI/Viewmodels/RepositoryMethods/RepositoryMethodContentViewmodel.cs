using DD.Lab.Wpf.ViewModels.Base;
using DomainGeneratorUI.Viewmodels.Methods;
using DomainGeneratorUI.Viewmodels.UseCases;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace DomainGeneratorUI.Viewmodels.RepositoryMethods
{
    public class RepositoryMethodContentViewmodel : BaseViewModel
    {
        public List<MethodParameterViewModel> Parameteters { get { return GetValue<List<MethodParameterViewModel>>(); } set { SetValue(value, UpdatedParameteters);  } }
        public ObservableCollection<MethodParameterViewModel> InputParametetersCollection { get; set; } = new ObservableCollection<MethodParameterViewModel>();
        public ObservableCollection<MethodParameterViewModel> OutputParametetersCollection { get; set; } = new ObservableCollection<MethodParameterViewModel>();

        public UseCaseSentenceCollectionViewmodel SentenceCollection { get { return GetValue<UseCaseSentenceCollectionViewmodel>(); } set { SetValue(value); } }

        private void UpdatedParameteters(List<MethodParameterViewModel> parameters)
        {
            var inputParameters = parameters
                .Where(k => k.Direction == Models.Methods.MethodParameter.ParameterDirection.Input)
                .ToList();
            var outputParameters = parameters
                .Where(k => k.Direction == Models.Methods.MethodParameter.ParameterDirection.Output)
                .ToList();

            UpdateListToCollection(inputParameters, InputParametetersCollection);
            UpdateListToCollection(outputParameters, OutputParametetersCollection);
        }
    }
}
