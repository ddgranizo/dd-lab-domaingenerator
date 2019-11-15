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
    public class ProjectControlViewModel : BaseViewModel
    {
		public ProjectStateModel ProjectState { get { return GetValue<ProjectStateModel>(); } set { SetValue(value); } }

        public bool IsOpen { get { return GetValue<bool>(); } set { SetValue(value); } }

        private ProjectControlView _view;

		public ProjectControlViewModel()
        {
			
        }

        public void Initialize(ProjectControlView v)
        {
			_view = v;
        }
    }
}
