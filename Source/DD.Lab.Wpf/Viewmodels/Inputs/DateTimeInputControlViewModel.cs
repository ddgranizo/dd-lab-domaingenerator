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
    public class DateTimeInputControlViewModel : BaseViewModel
    {
        public WpfEventManager WpfEventManager { get { return GetValue<WpfEventManager>(); } set { SetValue(value); } }


        public DateTime DefaultValue { get { return GetValue<DateTime>(); } set { SetValue(value); } }

		private DateTimeInputControlView _view;

		public DateTimeInputControlViewModel()
        {
			
        }

        public void Initialize(DateTimeInputControlView v)
        {
			_view = v;
        }


    }
}
