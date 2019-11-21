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
    public class ViewControlViewModel : BaseViewModel
    {
		public ViewModel View { get { return GetValue<ViewModel>(); } set { SetValue(value, UpdatedSchemaView); } }
        public string ParameterList { get { return GetValue<string>(); } set { SetValue(value); } }
        public DomainEventManager EventManager { get { return GetValue<DomainEventManager>(); } set { SetValue(value); } }


        private ViewControlView _view;

        public bool IsGeneralOpen { get { return GetValue<bool>(); } set { SetValue(value); } }
        public bool IsAttributesOpen { get { return GetValue<bool>(); } set { SetValue(value); } }
        public ViewControlViewModel()
        {
			
        }

        private void UpdatedSchemaView(ViewModel schemaViewModel)
        {
            ParameterList = string.Format("({0})", 
                string.Join(", ", schemaViewModel.Parameters.Select(k => $"{k.Type} {k.Name}")));
        }

        public void Initialize(ViewControlView v)
        {
			_view = v;
        }
    }
}
