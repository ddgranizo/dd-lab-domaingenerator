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
    public class GuidInputControlViewModel : BaseViewModel
    {

        public Guid DefaultValue { get { return GetValue<Guid>(); } set { SetValue(value, SetDefaultValue); } }
        public string Value { get { return GetValue<string>(); } set { SetValue(value, UpdatedValue); } }
        
		private GuidInputControlView _view;

		public GuidInputControlViewModel()
        {
			
        }

        public void Initialize(GuidInputControlView v)
        {
			_view = v;
            Value = DefaultValue.ToString();
        }

        private void UpdatedValue(string value)
        {
            if (!Guid.TryParse(value, out Guid myValue))
            {
                Value = Guid.Empty.ToString();
            }
            _view.RaiseValueChangedEvent(myValue);
        }

        private void SetDefaultValue(Guid value)
        {
            Value = value.ToString();
        }

    }
}
