using DD.Lab.Wpf.Controls.Inputs;
using DD.Lab.Wpf.Models.Inputs;
using DD.Lab.Wpf.ViewModels.Base;
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

namespace DD.Lab.Wpf.Viewmodels.Inputs
{
    public class GenericFormInputControlViewModel : BaseViewModel
    {
        public GenericFormInputModel InputModel { get { return GetValue<GenericFormInputModel>(); } set { SetValue(value, UpdatedInputModel); } }

        public OptionSetValue DefaultOptionSetValue { get { return GetValue<OptionSetValue>(); } set { SetValue(value); } }

        public EntityReferenceValue DefaultEntityReferenceValue { get { return GetValue<EntityReferenceValue>(); } set { SetValue(value); } }
        public string DefaultStringValue { get { return GetValue<string>(); } set { SetValue(value); } }
        public DateTime DefaultDateTimeValue { get { return GetValue<DateTime>(); } set { SetValue(value); } }

        public string DefaultPasswordValue { get { return GetValue<string>(); } set { SetValue(value); } }
        public bool DefaultBoolValue { get { return GetValue<bool>(); } set { SetValue(value); } }
        public int DefaultIntValue { get { return GetValue<int>(); } set { SetValue(value); } }
        public decimal DefaultDecimalValue { get { return GetValue<decimal>(); } set { SetValue(value); } }
        public double DefaultDoubleValue { get { return GetValue<double>(); } set { SetValue(value); } }

        public List<string> Suggestions { get { return GetValue<List<string>>(); } set { SetValue(value); UpdateListToCollection(value, SuggestionsCollection); } }
        public ObservableCollection<string> SuggestionsCollection { get; set; } = new ObservableCollection<string>();


        public List<OptionSetValue> OptionSetOptions { get { return GetValue<List<OptionSetValue>>(); } set { SetValue(value); UpdateListToCollection(value, OptionSetOptionsCollection); } }
        public ObservableCollection<OptionSetValue> OptionSetOptionsCollection { get; set; } = new ObservableCollection<OptionSetValue>();


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
            if (model.DefaultValue != null)
            {
                if (model.Type == GenericFormInputModel.TypeValue.Bool)
                {
                    DefaultBoolValue = (bool)model.DefaultValue;
                }
                else if (model.Type == GenericFormInputModel.TypeValue.Decimal)
                {
                    DefaultDecimalValue = (decimal)model.DefaultValue;
                }
                else if (model.Type == GenericFormInputModel.TypeValue.Double)
                {
                    DefaultDoubleValue = (double)model.DefaultValue;
                }
                else if (model.Type == GenericFormInputModel.TypeValue.Int)
                {
                    int valueInt =  model.DefaultValue is Int64
                        ? Convert.ToInt32(model.DefaultValue)
                        : (int)model.DefaultValue;
                    DefaultIntValue = valueInt;
                }
                else if (model.Type == GenericFormInputModel.TypeValue.Password)
                {
                    DefaultPasswordValue = (string)model.DefaultValue;
                }
                else if (model.Type == GenericFormInputModel.TypeValue.String)
                {
                    DefaultStringValue = (string)model.DefaultValue;
                }
                else if (model.Type == GenericFormInputModel.TypeValue.MultilineString)
                {
                    DefaultStringValue = (string)model.DefaultValue;
                }
                else if (model.Type == GenericFormInputModel.TypeValue.Guid)
                {
                    DefaultStringValue = model.DefaultValue.ToString();
                }
                else if (model.Type == GenericFormInputModel.TypeValue.DateTime)
                {
                    DefaultDateTimeValue = (DateTime)model.DefaultValue;
                }
                else if (model.Type == GenericFormInputModel.TypeValue.EntityReference)
                {
                    DefaultEntityReferenceValue = (EntityReferenceValue)model.DefaultValue;
                }
                else if (model.Type == GenericFormInputModel.TypeValue.OptionSet)
                {
                    DefaultOptionSetValue = (OptionSetValue)model.DefaultValue;

                }
                else if (model.Type == GenericFormInputModel.TypeValue.State)
                {
                    DefaultOptionSetValue = (OptionSetValue)model.DefaultValue;
                }
                else
                {
                    throw new NotImplementedException();
                }
            }
            if (model.Type == GenericFormInputModel.TypeValue.OptionSet
                || model.Type == GenericFormInputModel.TypeValue.State)
            {
                SetOptionSetSuggestions(model);
            }


            _view.ClearControl();
            if (model.Type == GenericFormInputModel.TypeValue.String)
            {
                _view.AddStringControl(DefaultStringValue, Suggestions);
            }
            else if (model.Type == GenericFormInputModel.TypeValue.Bool)
            {
                _view.AddBooleanControl(DefaultBoolValue);
            }
            else if (model.Type == GenericFormInputModel.TypeValue.Decimal)
            {
                _view.AddDecimalControl(DefaultDecimalValue);
            }
            else if (model.Type == GenericFormInputModel.TypeValue.Double)
            {
                _view.AddDoubleControl(DefaultDoubleValue);
            }
            else if (model.Type == GenericFormInputModel.TypeValue.Int)
            {
                _view.AddIntegerControl(DefaultIntValue);
            }
            else if (model.Type == GenericFormInputModel.TypeValue.Password)
            {
                _view.AddPasswordControl();
            }
            else if (model.Type == GenericFormInputModel.TypeValue.MultilineString)
            {
                _view.AddMultilineStringControl(DefaultStringValue);
            }
            else if (model.Type == GenericFormInputModel.TypeValue.Guid)
            {
                _view.AddGuidControl(DefaultStringValue);
            }
            else if (model.Type == GenericFormInputModel.TypeValue.DateTime)
            {
                _view.AddDateTimeControl(DefaultDateTimeValue);
            }
            else if (model.Type == GenericFormInputModel.TypeValue.EntityReference)
            {
                _view.AddEntityReferenceControl(DefaultEntityReferenceValue);
            }
            else if (model.Type == GenericFormInputModel.TypeValue.OptionSet)
            {
                _view.AddOptionSetControl(DefaultOptionSetValue, OptionSetOptions);
            }
            else if (model.Type == GenericFormInputModel.TypeValue.State)
            {
                _view.AddOptionSetControl(DefaultOptionSetValue, OptionSetOptions);
            }
            else
            {
                throw new NotImplementedException();
            }

        }

        private void SetOptionSetSuggestions(GenericFormInputModel model)
        {
            OptionSetOptions = model.OptionSetValueOptions.ToList();
        }

        private void SetStringSuggestions(GenericFormInputModel model)
        {
            var suggestions = new string[] { };
            if (model.StringSuggestionOptions != null)
            {
                suggestions = model.StringSuggestionOptions;
            }
            if (suggestions.Length == 0 && model.SuggestionHandler != null)
            {
                suggestions = model.SuggestionHandler.GetValues();
            }
            Suggestions = suggestions.ToList();
        }
    }
}
