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
    public class GithubSettingControlViewModel : BaseViewModel
    {
        public DomainEventManager EventManager { get { return GetValue<DomainEventManager>(); } set { SetValue(value); } }


        public GithubSettingModel GithubSetting { get { return GetValue<GithubSettingModel>(); } set { SetValue(value); } }

		private GithubSettingControlView _view;

        public bool IsOpen { get { return GetValue<bool>(); } set { SetValue(value); } }

        public GithubSettingControlViewModel()
        {
			
        }

        public void Initialize(GithubSettingControlView v)
        {
			_view = v;
        }
    }
}
