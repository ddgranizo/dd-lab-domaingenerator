using DD.DomainGenerator.Actions.Base;
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
    public class ActionControlViewModel : BaseViewModel
    {
		public ActionExecutionModel Action { get { return GetValue<ActionExecutionModel>(); } set { SetValue(value, UpdatedAction); } }
        public string ActionInputParameters { get { return GetValue<string>(); } set { SetValue(value); } }
        public string ActionOutputParameters { get { return GetValue<string>(); } set { SetValue(value); } }
        public bool InOutAreSame { get { return GetValue<bool>(); } set { SetValue(value); } }
        private ActionControlView _view;

		public ActionControlViewModel()
        {
			
        }

        public void Initialize(ActionControlView v)
        {
			_view = v;
        }

		private void UpdatedAction(ActionExecutionModel action)
        {
            ActionInputParameters = string.Join(Environment.NewLine, action.InputParameters
                .Where(k=>k.Key != "help")
                .Select(k => $"{k.Key}={k.Value}"));
            ActionOutputParameters = string.Join(Environment.NewLine, action.OutputParameters
                .Where(k => k.Key != "help")
                .Select(k => $"{k.Key}={k.Value}"));
            InOutAreSame = ActionInputParameters == ActionOutputParameters;
        }

    }
}
