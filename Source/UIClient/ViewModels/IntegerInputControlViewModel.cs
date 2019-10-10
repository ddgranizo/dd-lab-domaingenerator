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
    public class IntegerInputControlViewModel : BaseViewModel
    {
		public int DefaultValue { get { return GetValue<int>(); } set { SetValue(value, SetDefaultValue); } }
        public string Value {  get { return GetValue<string>(); }  set { SetValue(value, UpdatedValue); } }

        private IntegerInputControlView _view;

		public IntegerInputControlViewModel()
        {
			
        }

        public void Initialize(IntegerInputControlView v)
        {
			_view = v;
            Value = DefaultValue.ToString();
        }

        private void UpdatedValue(string value)
        {
            if (!int.TryParse(value, out int myValue))
            {
                Value = 0.ToString();
            }
            _view.RaiseValueChangedEvent(myValue);
        }

        private void SetDefaultValue(int value)
        {
            Value = value.ToString();
        }
    }
}
