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
    public class DomainInMicroServiceControlViewModel : BaseViewModel
    {

		public DomainInMicroServiceModel DomainInMicroService { get { return GetValue<DomainInMicroServiceModel>(); } set { SetValue(value); } }
        
		private DomainInMicroServiceControlView _view;

		public DomainInMicroServiceControlViewModel()
        {
			
        }

        public void Initialize(DomainInMicroServiceControlView v)
        {
			_view = v;
        }
    }
}
