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
    public class MicroServiceControlViewModel : BaseViewModel
    {
		public MicroServiceModel MicroService { get { return GetValue<MicroServiceModel>(); } set { SetValue(value); } }
        
		private MicroServiceControlView _view;

		public MicroServiceControlViewModel()
        {
			
        }

        public void Initialize(MicroServiceControlView v)
        {
			_view = v;
        }

    }
}
