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
    public class ErrorControlViewModel : BaseViewModel
    {
		public ErrorExecutionActionModel Error { get { return GetValue<ErrorExecutionActionModel>(); } set { SetValue(value); } }
        
		private ErrorControlView _view;

		public ErrorControlViewModel()
        {
			
        }

        public void Initialize(ErrorControlView v)
        {
			_view = v;
        }
    }
}
