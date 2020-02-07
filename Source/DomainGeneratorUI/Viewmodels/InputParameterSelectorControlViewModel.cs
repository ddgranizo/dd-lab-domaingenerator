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
        public MethodParameterReferenceValueViewModel NewReferenceValue { get { return GetValue<MethodParameterReferenceValueViewModel>(); } set { SetValue(value, UpdatedNewReferenceValue); } }

        public MethodParameterReferenceViewModel SelectedMethodParameterReference { get { return GetValue<MethodParameterReferenceViewModel>(); } set { SetValue(value, UpdatedSelectedMethodParameterReference); } }

        public KeyValuePair<int, string> SelectedSourceType { get { return GetValue<KeyValuePair<int, string>>(); } set { SetValue(value, (value) => { SelectedSourceInt = value.Key; }); } }
        public int SelectedSourceInt { get { return GetValue<int>(); } set { SetValue(value, UpdatedSelectedSourceInt); } }

        public Dictionary<int, string> SourceTypes { get { return GetValue<Dictionary<int, string>>(); } set { SetValue(value); } }

        private InputParameterSelectorControlView _view;

        public InputParameterSelectorControlViewModel()
        {
            SourceTypes = new Dictionary<int, string>()
            {
                { 1, "Constant"  },
                { 2, "Dynamic"  },
            };

            NewReferenceValue = new MethodParameterReferenceValueViewModel();
        }

        public void Initialize(InputParameterSelectorControlView v)
        {
            _view = v;
        }


        private void UpdatedParameter(MethodParameterViewModel data)
        {
            NewReferenceValue.RegardingMethodParameter = data;
            
        }

        private void UpdatedCurrentReferenceValue(MethodParameterReferenceValueViewModel data)
        {
            var newValue = new MethodParameterReferenceValueViewModel();
            if (data == null)
            {
                newValue.RegardingMethodParameter = Parameter;
            }
            else
            {
                newValue.Type = data.Type;
                if (newValue.Type == MethodParameterReferenceValue.ValueType.Constant)
                {
                    SelectedSourceType = SourceTypes.ElementAt(0);
                }
                else
                {
                    SelectedSourceType = SourceTypes.ElementAt(1);
                }
                newValue.RegardingMethodParameter = data.RegardingMethodParameter;
                newValue.RegardingReferenceMethodParameter = data.RegardingReferenceMethodParameter;
                if (newValue.Type == MethodParameterReferenceValue.ValueType.Constant)
                {
                    var type = MethodTypeToGenericFormInputType(data.RegardingMethodParameter.Type);
                    _view.UpdateGenericConstantInput(data.RegardingMethodParameter.Name, type, data.ConstantValue);
                }
                newValue.ConstantValue = data.ConstantValue;
            }
            NewReferenceValue = newValue;
            UpdatedCurrentValue();
        }

        private void UpdatedSelectedSourceInt(int type)
        {
            NewReferenceValue.ConstantValue = null;
            NewReferenceValue.RegardingReferenceMethodParameter = null;
            if (type == 1)
            {
                NewReferenceValue.Type = MethodParameterReferenceValue.ValueType.Constant;
                var genericMainType = MethodTypeToGenericFormInputType(NewReferenceValue.RegardingMethodParameter.Type);
                var defaultValue = GetDefaultValueOfType(genericMainType);
                NewReferenceValue.ConstantValue = defaultValue;
                _view.UpdateGenericConstantInput(NewReferenceValue.RegardingMethodParameter.Name, genericMainType, defaultValue);
            }
            UpdatedCurrentValue();
        }

        private void UpdatedSelectedMethodParameterReference(MethodParameterReferenceViewModel data)
        {
            NewReferenceValue.RegardingReferenceMethodParameter = data;
            if (data.Sentence == null)
            {
                NewReferenceValue.Type = MethodParameterReferenceValue.ValueType.UseCaseInput;
            }
            else
            {
                NewReferenceValue.Type = MethodParameterReferenceValue.ValueType.SentenceOutput;
            }
            
            UpdatedCurrentValue();
        }

        public void UpdatedConstantValue(object value)
        {
            NewReferenceValue.ConstantValue = value;
            UpdatedCurrentValue();
        }


        private void UpdatedNewReferenceValue(MethodParameterReferenceValueViewModel data)
        {
            
        }

        
        public string GetDynamicParameterDisplayName(MethodParameterReferenceViewModel parameter)
        {
            return parameter.GetDisplayName();
        }


        private void UpdatedCurrentValue()
        {
            _view.RaiseUpdatedValueEvent(CurrentReferenceValue, NewReferenceValue);
        }

        private object GetDefaultValueOfType(TypeValue type)
        {
            if (type == TypeValue.Bool)
            {
                return false;
            }
            else if (type == TypeValue.DateTime)
            {
                return DateTime.Now;
            }
            else if (type == TypeValue.Decimal)
            {
                return (decimal)0;
            }
            else if (type == TypeValue.Double)
            {
                return (double)0;
            }
            else if (type == TypeValue.Int)
            {
                return (int)0;
            }
            else if (type == TypeValue.MultilineString)
            {
                return string.Empty;
            }
            else if (type == TypeValue.String)
            {
                return string.Empty;
            }
            throw new NotImplementedException();
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
    }
}
