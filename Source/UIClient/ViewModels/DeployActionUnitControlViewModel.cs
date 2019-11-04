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
    public class DeployActionUnitControlViewModel : BaseViewModel
    {
		public DeployActionUnitModel DeployActionUnit { get { return GetValue<DeployActionUnitModel>(); } set { SetValue(value, UpdatedAction); } }
        public string ActionParameters { get { return GetValue<string>(); } set { SetValue(value); } }
        public string ResponseParameters { get { return GetValue<string>(); } set { SetValue(value); } }

        private DeployActionUnitControlView _view;

		public DeployActionUnitControlViewModel()
        {
			
        }

        public void Initialize(DeployActionUnitControlView v)
        {
			_view = v;
        }

        private void UpdatedAction(DeployActionUnitModel action)
        {
            ActionParameters = string.Join(Environment.NewLine, action.ActionExecution.Parameters
                 .Where(k => k.Key != "help")
                .Select(k => $"{k.Key}={k.Value}"));

            ResponseParameters = string.Join(Environment.NewLine, action.ResponseParameters
               .Select(k => $"{k.Key}={k.Value}"));
        }
    }
}
