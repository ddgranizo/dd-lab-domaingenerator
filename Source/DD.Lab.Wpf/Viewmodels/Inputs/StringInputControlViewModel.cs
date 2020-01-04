using DD.Lab.Wpf.Controls.Inputs;
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
    public class StringInputControlViewModel : BaseViewModel
    {
        public WpfEventManager WpfEventManager { get { return GetValue<WpfEventManager>(); } set { SetValue(value); } }

        public bool IsReadOnly { get { return GetValue<bool>(); } set { SetValue(value); } }
        public bool IsMultiline { get { return GetValue<bool>(); } set { SetValue(value); } }

        public string DefaultValue { get { return GetValue<string>(); } set { SetValue(value, SetDefaultValue); } }

        public List<string> Sugestions { get { return GetValue<List<string>>(); } set { SetValue(value, UpdatedSugestions); UpdateListToCollection(value, SugestionsCollection); } }
        public ObservableCollection<string> SugestionsCollection { get; set; } = new ObservableCollection<string>();

        public string Value { get { return GetValue<string>(); } set { SetValue(value, UpdatedValue); } }
        public string SelectedSugestion { get { return GetValue<string>(); } set { SetValue(value, UpdatedSugestion); } }

        private StringInputControlView _view;

        public StringInputControlViewModel()
        {

        }

        public void Initialize(StringInputControlView v)
        {
            _view = v;
            Value = DefaultValue;
        }

        private void UpdatedSugestions(List<string> value)
        {
            CheckIfValueIsInSugestions(Value);
        }

        private void UpdatedSugestion(string value)
        {
            Value = value;
        }


        private void UpdatedValue(string value)
        {
            _view.RaiseValueChangedEvent(value);
        }

        private void SetDefaultValue(string value)
        {
            CheckIfValueIsInSugestions(value);
            Value = value;
        }

        private void CheckIfValueIsInSugestions(string value)
        {
            if (!string.IsNullOrEmpty(value) && Sugestions!= null && Sugestions.Count > 0)
            {
                var itemInSugestion = Sugestions.FirstOrDefault(k => k.ToLower() == value.ToLower());
                if (itemInSugestion != null)
                {
                    SelectedSugestion = itemInSugestion;
                }
            }
        }
    }
}
