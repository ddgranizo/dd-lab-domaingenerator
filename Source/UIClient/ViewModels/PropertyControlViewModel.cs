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
    public class PropertyControlViewModel : BaseViewModel
    {

        public DomainEventManager EventManager { get { return GetValue<DomainEventManager>(); } set { SetValue(value); } }

        public SchemaPropertyModel Property { get { return GetValue<SchemaPropertyModel>(); } set { SetValue(value); } }
        public bool IsGeneralOpen { get { return GetValue<bool>(); } set { SetValue(value); } }


        private PropertyControlView _view;

		public PropertyControlViewModel()
        {
			
        }

        public void Initialize(PropertyControlView v)
        {
			_view = v;
        }

		

    }
}
