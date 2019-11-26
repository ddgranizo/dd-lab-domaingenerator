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
    public class GenericFormControlViewModel : BaseViewModel
    {
		public GenericFormModel FormModel { get { return GetValue<GenericFormModel>(); } set { SetValue(value); } }

		public Dictionary<string, object> InitialValues { get { return GetValue<Dictionary<string, object>>(); } set { SetValue(value); } }
        
		private GenericFormControlView _view;

		public GenericFormControlViewModel()
        {
			
        }

        public void Initialize(GenericFormControlView v)
        {
			_view = v;
        }

    }
}
