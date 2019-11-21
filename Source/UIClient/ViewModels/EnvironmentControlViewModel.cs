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
    public class EnvironmentControlViewModel : BaseViewModel
    {
        public DomainEventManager EventManager { get { return GetValue<DomainEventManager>(); } set { SetValue(value); } }

        public EnvironmentModel Environment { get { return GetValue<EnvironmentModel>(); } set { SetValue(value); } }
        
		private EnvironmentControlView _view;

        public bool IsOpen { get { return GetValue<bool>(); } set { SetValue(value); } }

        public EnvironmentControlViewModel()
        {
			
        }

        public void Initialize(EnvironmentControlView v)
        {
			_view = v;
        }
    }
}
