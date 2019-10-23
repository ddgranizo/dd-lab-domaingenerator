using DD.DomainGenerator.Models;
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
using UIClient.UserControls;
using UIClient.ViewModels.Base;

namespace UIClient.ViewModels
{
    public class GenericInputControlViewModel : BaseViewModel
    {
        public ActionParameterDefinition ParameterDefinition { get { return GetValue<ActionParameterDefinition>(); } set { SetValue(value); } }
        public Dictionary<string, object> DefaultValues { get { return GetValue<Dictionary<string, object>>(); } set { SetValue(value, DefaultValuesUpdate); } }

        public Dictionary<string, List<string>> SugestionsDictionary { get { return GetValue<Dictionary<string, List<string>>>(); } set { SetValue(value, SugestionsDictionaryUpdated); } }

        public string DefaultStringValue { get { return GetValue<string>(); } set { SetValue(value); } }
        public string DefaultPasswordValue { get { return GetValue<string>(); } set { SetValue(value); } }
        public bool DefaultBoolValue { get { return GetValue<bool>(); } set { SetValue(value); } }
        public int DefaultIntValue { get { return GetValue<int>(); } set { SetValue(value); } }
        public decimal DefaultDecimalValue { get { return GetValue<decimal>(); } set { SetValue(value); } }
        public Guid DefaultGuidValue { get { return GetValue<Guid>(); } set { SetValue(value); } }

        public List<string> Sugestions { get { return GetValue<List<string>>(); } set { SetValue(value); UpdateListToCollection(value, SugestionsCollection); } }
        public ObservableCollection<string> SugestionsCollection { get; set; } = new ObservableCollection<string>();



        private GenericInputControlView _view;

        public GenericInputControlViewModel()
        {

        }

        private void SugestionsDictionaryUpdated(Dictionary<string, List<string>> values)
        {
            if (values.ContainsKey(ParameterDefinition.Name))
            {
                Sugestions = values[ParameterDefinition.Name];
            }
        }

        private void DefaultValuesUpdate(Dictionary<string, object> values)
        {
            if (values.ContainsKey(ParameterDefinition.Name))
            {
                object value = values[ParameterDefinition.Name];
                if (ParameterDefinition.Type == ActionParameterDefinition.TypeValue.Boolean)
                {
                    DefaultBoolValue = (bool)value;
                }
                else if (ParameterDefinition.Type == ActionParameterDefinition.TypeValue.Integer)
                {
                    DefaultIntValue = (int)value;
                }
                else if (ParameterDefinition.Type == ActionParameterDefinition.TypeValue.Decimal)
                {
                    DefaultDecimalValue = (int)value;
                }
                else if (ParameterDefinition.Type == ActionParameterDefinition.TypeValue.String)
                {
                    DefaultStringValue = (string)value;
                }
                else if (ParameterDefinition.Type == ActionParameterDefinition.TypeValue.Password)
                {
                    DefaultPasswordValue = (string)value;
                }
                else if (ParameterDefinition.Type == ActionParameterDefinition.TypeValue.Guid)
                {
                    DefaultGuidValue = (Guid)value;
                }
            }
        }

        public void Initialize(GenericInputControlView v)
        {
            _view = v;
        }

    }
}
