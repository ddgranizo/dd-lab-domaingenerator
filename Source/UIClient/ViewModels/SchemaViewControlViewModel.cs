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
    public class SchemaViewControlViewModel : BaseViewModel
    {
		public SchemaViewModel SchemaView { get { return GetValue<SchemaViewModel>(); } set { SetValue(value, UpdatedSchemaView); } }
        public string ParameterList { get { return GetValue<string>(); } set { SetValue(value); } }

        private SchemaViewControlView _view;

		public SchemaViewControlViewModel()
        {
			
        }

        private void UpdatedSchemaView(SchemaViewModel schemaViewModel)
        {
            ParameterList = string.Format("({0})", 
                string.Join(", ", schemaViewModel.Parameters.Select(k => $"{k.Type} {k.Name}")));
        }

        public void Initialize(SchemaViewControlView v)
        {
			_view = v;
        }
    }
}
