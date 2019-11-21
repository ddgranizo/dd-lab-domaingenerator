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
    public class ProjectControlViewModel : BaseViewModel
    {
        public DomainEventManager EventManager { get { return GetValue<DomainEventManager>(); } set { SetValue(value); } }

        public ProjectStateModel ProjectState { get { return GetValue<ProjectStateModel>(); } set { SetValue(value); } }

        public bool IsGeneralOpen { get { return GetValue<bool>(); } set { SetValue(value); } }
        public bool IsDomainsOpen { get { return GetValue<bool>(); } set { SetValue(value); } }
        public bool IsSettingsOpen { get { return GetValue<bool>(); } set { SetValue(value); } }
        public bool IsAzurePipelineSettingsOpen { get { return GetValue<bool>(); } set { SetValue(value); } }
        public bool IsGithubSettingsOpen { get { return GetValue<bool>(); } set { SetValue(value); } }
        public bool IsEnvironmentsOpen { get { return GetValue<bool>(); } set { SetValue(value); } }

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
