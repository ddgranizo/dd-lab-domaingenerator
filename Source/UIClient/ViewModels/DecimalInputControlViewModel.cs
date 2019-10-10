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
    public class DecimalInputControlViewModel : BaseViewModel
    {


        public decimal DefaultValue { get { return GetValue<decimal>(); } set { SetValue(value, SetDefaultValue); } }
        public string Value { get { return GetValue<string>(); } set { SetValue(value, UpdatedValue); } }



        private DecimalInputControlView _view;

		public DecimalInputControlViewModel()
        {
			
        }

        public void Initialize(DecimalInputControlView v)
        {
			_view = v;
            Value = DefaultValue.ToString();
        }

        private void UpdatedValue(string value)
        {
            if (!decimal.TryParse(value, out decimal myValue))
            {
                Value = 0.ToString();
            }
            _view.RaiseValueChangedEvent(myValue);
        }

        private void SetDefaultValue(decimal value)
        {
            Value = value.ToString();
        }

    }
}
