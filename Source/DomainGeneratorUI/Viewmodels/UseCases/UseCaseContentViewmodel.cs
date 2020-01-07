using DD.Lab.Wpf.ViewModels.Base;
using DomainGeneratorUI.Viewmodels.Methods;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace DomainGeneratorUI.Viewmodels.UseCases
{
    public class UseCaseContentViewmodel: BaseViewModel
    {
        public List<MethodParameterViewmodel> Parameters { get { return GetValue<List<MethodParameterViewmodel>>(); } set { SetValue(value, UpdatedParameteters); } }
        public ObservableCollection<MethodParameterViewmodel> InputParametetersCollection { get; set; } = new ObservableCollection<MethodParameterViewmodel>();
        public ObservableCollection<MethodParameterViewmodel> OutputParametetersCollection { get; set; } = new ObservableCollection<MethodParameterViewmodel>();


        private void UpdatedParameteters(List<MethodParameterViewmodel> parameters)
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
