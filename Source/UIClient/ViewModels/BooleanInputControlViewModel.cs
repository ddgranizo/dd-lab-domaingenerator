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
using UIClient.UserControls.Inputs;
using UIClient.ViewModels.Base;

namespace UIClient.ViewModels
{
    public class BooleanInputControlViewModel : BaseViewModel
    {

		public bool DefaultValue { get { return GetValue<bool>(); } set { SetValue(value, SetDefaultValue); } }
        public bool Value { get { return GetValue<bool>(); } set { SetValue(value, UpdatedValue); } }

        private BooleanInputControlView _view;

		public BooleanInputControlViewModel()
        {
			
        }

        public void Initialize(BooleanInputControlView v)
        {
			_view = v;
            Value = DefaultValue;
        }


        private void UpdatedValue(bool value)
        {
            _view.RaiseValueChangedEvent(value);
        }



        private void SetDefaultValue(bool value)
        {
            Value = value;
        }
		

    }
}
