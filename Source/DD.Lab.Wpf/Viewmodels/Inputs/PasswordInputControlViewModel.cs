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
    public class PasswordInputControlViewModel : BaseViewModel
    {
        public string Value { get { return GetValue<string>(); } set { SetValue(value, UpdatedValue); } }

        private PasswordInputControlView _view;

        public PasswordInputControlViewModel()
        {
        }

        private void UpdatedValue(string value)
        {
            _view.RaiseValueChangedEvent(value);
        }

        public void Initialize(PasswordInputControlView v)
        {
			_view = v;
        }
    }
}
