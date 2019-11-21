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
    public class ModelControlViewModel : BaseViewModel
    {
        public DomainEventManager EventManager { get { return GetValue<DomainEventManager>(); } set { SetValue(value); } }

        public ModelModel Model { get { return GetValue<ModelModel>(); } set { SetValue(value, UpdatedModel); } }
        public string Properties { get { return GetValue<string>(); } set { SetValue(value); } }
        public bool IsGeneralOpen { get { return GetValue<bool>(); } set { SetValue(value); } }
        
        private ModelControlView _view;

		public ModelControlViewModel()
        {
			
        }

        public void Initialize(ModelControlView v)
        {
			_view = v;
        }
        
        private void UpdatedModel(ModelModel model)
        {
            Properties = string.Join(", ", model.Properties);
        }
    }
}
