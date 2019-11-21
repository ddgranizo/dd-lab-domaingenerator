using DD.DomainGenerator.Models;
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
using UIClient.Models;
using UIClient.UserControls;
using UIClient.ViewModels.Base;

namespace UIClient.ViewModels
{
    public class SettingControlViewModel : BaseViewModel
    {
		public SettingModel Setting { get { return GetValue<SettingModel>(); } set { SetValue(value); } }
        public DomainEventManager EventManager { get { return GetValue<DomainEventManager>(); } set { SetValue(value); } }

        public bool IsOpen { get { return GetValue<bool>(); } set { SetValue(value); } }

        private SettingControlView _view;

		public SettingControlViewModel()
        {
			
        }

        public void Initialize(SettingControlView v)
        {
			_view = v;
        }
    }
}
