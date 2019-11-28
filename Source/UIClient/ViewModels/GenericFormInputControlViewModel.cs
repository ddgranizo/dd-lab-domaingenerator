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
using UIClient.Models.Inputs;
using UIClient.UserControls.Inputs;
using UIClient.ViewModels.Base;

namespace UIClient.ViewModels
{
    public class GenericFormInputControlViewModel : BaseViewModel
    {
		public GenericFormInputModel InputModel { get { return GetValue<GenericFormInputModel>(); } set { SetValue(value, UpdatedInputModel); } }

        public string DefaultStringValue { get { return GetValue<string>(); } set { SetValue(value); } }
        public string DefaultPasswordValue { get { return GetValue<string>(); } set { SetValue(value); } }
        public bool DefaultBoolValue { get { return GetValue<bool>(); } set { SetValue(value); } }
        public int DefaultIntValue { get { return GetValue<int>(); } set { SetValue(value); } }
        public decimal DefaultDecimalValue { get { return GetValue<decimal>(); } set { SetValue(value); } }
        public Guid DefaultGuidValue { get { return GetValue<Guid>(); } set { SetValue(value); } }

        public List<string> Suggestions { get { return GetValue<List<string>>(); } set { SetValue(value); UpdateListToCollection(value, SuggestionsCollection); } }
        public ObservableCollection<string> SuggestionsCollection { get; set; } = new ObservableCollection<string>();

        private GenericFormInputControlView _view;

		public GenericFormInputControlViewModel()
        {
			
        }

        public void Initialize(GenericFormInputControlView v)
        {
			_view = v;
        }

        private void UpdatedInputModel(GenericFormInputModel model)
        {
            var suggestions = model.Options;
            if (suggestions.Length == 0 && model.SuggestionHandler != null)
            {
                suggestions = model.SuggestionHandler.GetValues();
            }
            Suggestions = suggestions.ToList();
            if (model.DefaultValue != null)
            {
                if (model.Type == ActionParameterDefinition.TypeValue.Boolean)
                {
                    DefaultBoolValue = (bool)model.DefaultValue;
                }
                else if (model.Type == ActionParameterDefinition.TypeValue.Decimal)
                {
                    DefaultDecimalValue = (decimal)model.DefaultValue;
                }
                else if (model.Type == ActionParameterDefinition.TypeValue.Integer)
                {
                    DefaultIntValue = (int)model.DefaultValue;
                }
                else if (model.Type == ActionParameterDefinition.TypeValue.Password)
                {
                    DefaultPasswordValue = (string)model.DefaultValue;
                }
                else if (model.Type == ActionParameterDefinition.TypeValue.String)
                {
                    DefaultStringValue = (string)model.DefaultValue;
                }
            }
        }
    }
}
