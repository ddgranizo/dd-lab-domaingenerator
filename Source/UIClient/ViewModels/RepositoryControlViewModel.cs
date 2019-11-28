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
    public class RepositoryControlViewModel : BaseViewModel
    {
        public DomainEventManager EventManager { get { return GetValue<DomainEventManager>(); } set { SetValue(value); } }

        public RepositoryModel Repository { get { return GetValue<RepositoryModel>(); } set { SetValue(value, UpdatedRepository); } }
        public bool IsGeneralOpen { get { return GetValue<bool>(); } set { SetValue(value); } }
        public bool IsMethodsOpen { get { return GetValue<bool>(); } set { SetValue(value); } }

        


        private RepositoryControlView _view;

		public RepositoryControlViewModel()
        {
			
        }

        public void Initialize(RepositoryControlView v)
        {
			_view = v;
        }

        private void UpdatedRepository(RepositoryModel repository)
        {

        }

		

    }
}
