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
using UIClient.Commands;
using UIClient.Models.Inputs;
using UIClient.UserControls.Inputs;
using UIClient.ViewModels.Base;

namespace UIClient.ViewModels
{
    public class GenericFormControlViewModel : BaseViewModel
    {
		public GenericFormModel FormModel { get { return GetValue<GenericFormModel>(); } set { SetValue(value, UpdatedFormModel); } }

        public Dictionary<string, object> Values { get { return GetValue<Dictionary<string, object>>(); } set { SetValue(value); } }

        private GenericFormControlView _view;

        public ICommand ConfirmCommand { get; set; }
        public ICommand CancelCommand { get; set; }

        public GenericFormControlViewModel()
        {
            ConfirmCommand = new ConfirmGenericFormDialogCommand(this);
            CancelCommand = new CancelGenericFormDialogCommand(this);

            Values = new Dictionary<string, object>();

            RegisterCommand(ConfirmCommand);
            RegisterCommand(CancelCommand);
        }


        public void Initialize(GenericFormControlView v)
        {
			_view = v;
        }

        public void UpdatedFormModel(GenericFormModel model)
        
        {
            foreach (var item in model.Attributes)
            {
                if (item.DefaultValue != null)
                {
                    UpdateValue(item.Key, item.DefaultValue);
                }
            }
        }

        public void ConfirmValues()
        {
            _view.RaiseOnConfirmedValuesEvent(Values);
        }

        public void Cancel()
        {
            _view.RaiseOnCanceledValues();
        }

        public void UpdateValue(string key, object value)
        {
            if (Values.ContainsKey(key))
            {
                Values[key] = value;
            }
            else
            {
                Values.Add(key, value);
            }
        }
    }
}
