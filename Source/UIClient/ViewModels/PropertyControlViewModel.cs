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


		public SchemaModelPropertyModel Property { get { return GetValue<SchemaModelPropertyModel>(); } set { SetValue(value); } }


        
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
