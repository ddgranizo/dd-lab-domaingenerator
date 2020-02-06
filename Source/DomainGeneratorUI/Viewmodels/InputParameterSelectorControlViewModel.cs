using DD.Lab.Wpf.Models.Inputs;
using DD.Lab.Wpf.ViewModels.Base;
using DomainGeneratorUI.Controls;
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
using static DD.Lab.Wpf.Models.Inputs.GenericFormInputModel;
using static DomainGeneratorUI.Models.Methods.MethodParameter;

namespace DomainGeneratorUI.Viewmodels
{
    public class InputParameterSelectorControlViewModel : BaseViewModel
    {
        public MethodParameterViewModel Parameter { get { return GetValue<MethodParameterViewModel>(); } set { SetValue(value, UpdatedParameter); } }

        public List<MethodParameterReferenceViewModel> AvailableParameterReferences { get { return GetValue<List<MethodParameterReferenceViewModel>>(); } set { SetValue(value); UpdateListToCollection(value, AvailableParameterReferencesCollection); } }
        public ObservableCollection<MethodParameterReferenceViewModel> AvailableParameterReferencesCollection { get; set; } = new ObservableCollection<MethodParameterReferenceViewModel>();

        public MethodParameterReferenceValueViewModel CurrentReferenceValue { get { return GetValue<MethodParameterReferenceValueViewModel>(); } set { SetValue(value, UpdatedCurrentReferenceValue); } }

        public KeyValuePair<int, string> SelectedSourceType { get { return GetValue<KeyValuePair<int, string>>(); } set { SetValue(value, (value) => { SelectedSourceInt = value.Key; }); } }
        public int SelectedSourceInt { get { return GetValue<int>(); } set { SetValue(value); } }

        public Dictionary<int, string> SourceTypes { get { return GetValue<Dictionary<int, string>>(); } set { SetValue(value); } }

        private InputParameterSelectorControlView _view;

        public InputParameterSelectorControlViewModel()
        {
            SourceTypes = new Dictionary<int, string>()
            {
                { 1, "Constant"  },
                { 2, "Dynamic"  },
            };
        }

        public void Initialize(InputParameterSelectorControlView v)
        {
            _view = v;
        }

        private void UpdatedParameter(MethodParameterViewModel data)
        {
            var genericMainType = MethodTypeToGenericFormInputType(data.Type);
            _view.SetGenericConstantInput(data.Name, genericMainType);
        }

        private TypeValue MethodTypeToGenericFormInputType(ParameterInputType type)
        {
            if (type == ParameterInputType.Boolean)
            {
                return TypeValue.Bool;
            }
            else if (type == ParameterInputType.Datetime)
            {
                return TypeValue.DateTime;
            }
            else if (type == ParameterInputType.Decimal)
            {
                return TypeValue.Decimal;
            }
            else if (type == ParameterInputType.Dictionary)
            {
                return 0;
            }
            else if (type == ParameterInputType.Double)
            {
                return TypeValue.Double;
            }
            else if (type == ParameterInputType.Entity)
            {
                return 0;
            }
            else if (type == ParameterInputType.Enumerable)
            {
                return 0;
            }
            else if (type == ParameterInputType.Guid)
            {
                return TypeValue.Guid;
            }
            else if (type == ParameterInputType.Integer)
            {
                return TypeValue.Int;
            }
            else if (type == ParameterInputType.String)
            {
                return TypeValue.MultilineString;
            }
            throw new NotImplementedException();
        }

        private void UpdatedAvailableParameterReferences(List<MethodParameterReferenceViewModel> data)
        {

        }

        private void UpdatedCurrentReferenceValue(MethodParameterReferenceValueViewModel data)
        {

        }

        public string GetDynamicParameterDisplayName(MethodParameterReferenceViewModel parameter)
        {
            return parameter.GetDisplayName();
        }
    }
}
