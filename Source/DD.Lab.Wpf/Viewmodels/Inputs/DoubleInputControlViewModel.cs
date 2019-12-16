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
    public class DoubleInputControlViewModel : BaseViewModel
    {

		public double DefaultValue { get { return GetValue<double>(); } set { SetValue(value, SetDefaultValue); } }
        public string Value { get { return GetValue<string>(); } set { SetValue(value, UpdatedValue); } }


        private DoubleInputControlView _view;

		public DoubleInputControlViewModel()
        {
			
        }

        public void Initialize(DoubleInputControlView v)
        {
			_view = v;
        }

        private void UpdatedValue(string value)
        {
            if (!double.TryParse(value, out double myValue))
            {
                Value = 0.ToString();
            }
            _view.RaiseValueChangedEvent(myValue);
        }

        private void SetDefaultValue(double value)
        {
            Value = value.ToString();
        }

    }
}
