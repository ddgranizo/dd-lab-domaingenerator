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
    public class UseCaseListItemControlViewModel : BaseViewModel
    {

		public UseCaseListItemModel UseCaseItem { get { return GetValue<UseCaseListItemModel>(); } set { SetValue(value); } }

		public DomainEventManager EventManager { get { return GetValue<DomainEventManager>(); } set { SetValue(value); } }
        
		private UseCaseListItemControlView _view;

		public UseCaseListItemControlViewModel()
        {
			
        }

        public void Initialize(UseCaseListItemControlView v)
        {
			_view = v;
        }

        public void SelectedUseCse()
        {
            EventManager.RaiseOnSelectedUseCaseCaseEvent(UseCaseItem.UseCase);
        }
    }
}
