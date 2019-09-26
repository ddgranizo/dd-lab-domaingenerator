using DD.DomainGenerator.Actions.Base;
using DD.DomainGenerator.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using UIClient.Models.Base;
using UIClient.ViewModels;

namespace UIClient.Models
{
    public class WorkspaceModel : BaseModel
    {

        public MainViewModel Vm { get; }
        public WorkspaceModel(MainViewModel viewModel)
        {
            Vm = viewModel ?? throw new ArgumentNullException(nameof(viewModel));
        }

        public List<ActionBase> NewActions { get { return GetValue<List<ActionBase>>(); } set { NewActionsCollection = SetCollection(value); } }
        public ObservableCollection<ActionBase> NewActionsCollection { get; set; }
        public ActionBase SelectedNewAction { get { return GetValue<ActionBase>(); } set { SetValue(value, Vm.OnNewActionChanged); } }

        public List<ActionParameterDefinition> NewActionParametersDefinitions { get { return GetValue<List<ActionParameterDefinition>>(); } set { NewActionParametersDefinitionsCollection = SetCollection(value); } }
        public ObservableCollection<ActionParameterDefinition> NewActionParametersDefinitionsCollection { get; set; }

    }
}
