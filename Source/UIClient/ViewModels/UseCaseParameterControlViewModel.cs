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
    public class UseCaseParameterControlViewModel : BaseViewModel
    {

		public UseCaseParameterModel UseCaseParameter { get { return GetValue<UseCaseParameterModel>(); } set { SetValue(value); } }
        public bool IsGeneralOpen { get { return GetValue<bool>(); } set { SetValue(value); } }

        public DomainEventManager EventManager { get { return GetValue<DomainEventManager>(); } set { SetValue(value); } }
        
		private UseCaseParameterControlView _view;

		public UseCaseParameterControlViewModel()
        {
			
        }

        public void Initialize(UseCaseParameterControlView v)
        {
			_view = v;
        }

    }
}
