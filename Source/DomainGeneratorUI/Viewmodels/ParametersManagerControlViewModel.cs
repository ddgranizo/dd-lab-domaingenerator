using DD.Basic.Extensions;
using DD.Lab.Wpf.Commands;
using DD.Lab.Wpf.Commands.Base;
using DD.Lab.Wpf.Models.Inputs;
using DD.Lab.Wpf.ViewModels.Base;
using DD.Lab.Wpf.Windows;
using DomainGeneratorUI.Controls;
using DomainGeneratorUI.Extensions;
using DomainGeneratorUI.Models.Methods;
using DomainGeneratorUI.Viewmodels.Methods;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Xml.Serialization;


namespace DomainGeneratorUI.Viewmodels
{
    public class ParametersManagerControlViewModel : BaseViewModel
    {
        public List<MethodParameterViewModel> Parameters { get { return GetValue<List<MethodParameterViewModel>>(); } set { SetValue(value); UpdateListToCollection(value, ParametersCollection); } }
        public ObservableCollection<MethodParameterViewModel> ParametersCollection { get; set; } = new ObservableCollection<MethodParameterViewModel>();

        public MethodParameterViewModel SelectedParameter { get { return GetValue<MethodParameterViewModel>(); } set { SetValue(value); } }
        public bool IsGenericFormOpen { get { return GetValue<bool>(); } set { SetValue(value); } }
        public GenericFormModel FormModel { get { return GetValue<GenericFormModel>(); } set { SetValue(value); } }


        private ParametersManagerControlView _view;

        private readonly List<OptionSetValue> _typesAttribute;

        public ParametersManagerControlViewModel()
        {
            RegisterCommands();
            _typesAttribute = new List<OptionSetValue>();
            foreach (MethodParameter.ParameterInputType type in (MethodParameter.ParameterInputType[])Enum.GetValues(typeof(MethodParameter.ParameterInputType)))
            {
                _typesAttribute.Add(new OptionSetValue(type.ToString(), (int)type));
            }
        }

        public void Initialize(ParametersManagerControlView v)
        {
            _view = v;
        }




        public ICommand AddNewParameterCommand { get; set; }
        public ICommand ModifyNewParameterCommand { get; set; }
        public ICommand RemoveParameterCommand { get; set; }
        public ICommand MoveUpParameterCommand { get; set; }
        public ICommand MoveDownParameterCommand { get; set; }

        private void RegisterCommands()
        {
            AddNewParameterCommand = new RelayCommandHandled((input) =>
            {
                var instance = new MethodParameterViewModel();
                var model = instance.ToGenericInputModel("Add new parmeter", _typesAttribute.ToArray());
                var window = new GenericInputFormWindow(model);
                window.ShowDialog();
                if (window.Response == WindowResponse.OK)
                {
                    instance.UpdateDataFromDictionary(window.Values);
                    Parameters.Add(instance);
                    UpdateListToCollection(Parameters, ParametersCollection);
                    _view.RaiseOnModifiedListEvent(Parameters);
                }
            });

            ModifyNewParameterCommand = new RelayCommandHandled((input) =>
            {
                var instance = SelectedParameter;
                var model = instance.ToGenericInputModel("Modify parmeter", _typesAttribute.ToArray());
                var window = new GenericInputFormWindow(model);
                window.ShowDialog();
                if (window.Response == WindowResponse.OK)
                {
                    instance.UpdateDataFromDictionary(window.Values);
                    UpdateListToCollection(Parameters, ParametersCollection);
                    _view.RaiseOnModifiedListEvent(Parameters);
                }

            }, (input) => { return SelectedParameter != null; });

            RemoveParameterCommand = new RelayCommandHandled((input) =>
            {
                RaiseOkCancelDialog("Confirm the remove?", "Remove", () =>
                {
                    Parameters.Remove(SelectedParameter);
                    UpdateListToCollection(Parameters, ParametersCollection);
                });
                _view.RaiseOnModifiedListEvent(Parameters);

            }, (input) => { return SelectedParameter != null; });

            MoveUpParameterCommand = new RelayCommandHandled((input) =>
            {
                var oldSelectedParameter = SelectedParameter;
                var items = Parameters;
                Parameters = Parameters.MoveItemUp(SelectedParameter).ToList();
                UpdateListToCollection(Parameters, ParametersCollection);
                SelectedParameter = ParametersCollection.First(k => k.Name == oldSelectedParameter.Name);
                _view.RaiseOnModifiedListEvent(Parameters);

            }, (input) => { return SelectedParameter != null && SelectedParameter != ParametersCollection.First(); });

            MoveDownParameterCommand = new RelayCommandHandled((input) =>
            {
                var oldSelectedParameter = SelectedParameter;
                var items = Parameters;
                Parameters = Parameters.MoveItemDown(SelectedParameter).ToList();
                UpdateListToCollection(Parameters, ParametersCollection);
                SelectedParameter = ParametersCollection.First(k => k.Name == oldSelectedParameter.Name);
                _view.RaiseOnModifiedListEvent(Parameters);

            }, (input) => { return SelectedParameter != null && SelectedParameter != ParametersCollection.Last(); });


            RegisterCommand(AddNewParameterCommand);
            RegisterCommand(ModifyNewParameterCommand);
            RegisterCommand(RemoveParameterCommand);
            RegisterCommand(MoveUpParameterCommand);
            RegisterCommand(MoveDownParameterCommand);
        }

    }
}
