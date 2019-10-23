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
    public class UseCaseControlViewModel : BaseViewModel
    {


		public UseCaseModel UseCase { get { return GetValue<UseCaseModel>(); } set { SetValue(value); } }


        
		private UseCaseControlView _view;

		public UseCaseControlViewModel()
        {
			
        }

        public void Initialize(UseCaseControlView v)
        {
			_view = v;
        }

		

    }
}
