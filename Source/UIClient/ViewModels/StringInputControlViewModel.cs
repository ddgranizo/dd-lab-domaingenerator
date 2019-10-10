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
    public class StringInputControlViewModel : BaseViewModel
    {
		public string DefaultValue { get { return GetValue<string>(); } set { SetValue(value, SetDefaultValue);  } }
		public List<string> Sugestions { get { return GetValue<List<string>>(); } set { SetValue(value); } }
        public string Value { get { return GetValue<string>(); } set { SetValue(value, UpdatedValue); } }

        private StringInputControlView _view;

		public StringInputControlViewModel()
        {
			
        }

        public void Initialize(StringInputControlView v)
        {
			_view = v;
            Value = DefaultValue;
        }

        private void UpdatedValue(string value)
        {
            _view.RaiseValueChangedEvent(value);
        }

		private void SetDefaultValue(string value)
        {
            Value = value;
        }
    }
}
