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
    public class SchemaInDomainViewModel : BaseViewModel
    {
		public SchemaInDomainModel SchemaInDomain { get { return GetValue<SchemaInDomainModel>(); } set { SetValue(value); } }
        
		private SchemaInDomainControlView _view;

		public SchemaInDomainViewModel()
        {
			
        }

        public void Initialize(SchemaInDomainControlView v)
        {
			_view = v;
        }
    }
}
