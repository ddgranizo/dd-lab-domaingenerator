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
    public class DomainControlViewModel : BaseViewModel
    {
        public DomainEventManager EventManager { get { return GetValue<DomainEventManager>(); } set { SetValue(value); } }

        public DomainModel Domain { get { return GetValue<DomainModel>(); } set { SetValue(value); } }

        public bool IsGeneralOpen { get { return GetValue<bool>(); } set { SetValue(value); } }
        public bool IsSchemasOpen { get { return GetValue<bool>(); } set { SetValue(value); } }
        private DomainControlView _view;

		public DomainControlViewModel()
        {
			
        }

        public void Initialize(DomainControlView v)
        {
			_view = v;
        }
    }
}
