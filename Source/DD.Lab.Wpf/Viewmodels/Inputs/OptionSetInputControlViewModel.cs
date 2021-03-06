using DD.Lab.Wpf.Controls.Inputs;
using DD.Lab.Wpf.Models.Inputs;
using DD.Lab.Wpf.ViewModels.Base;
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


namespace DD.Lab.Wpf.Viewmodels.Inputs
{
    public class OptionSetInputControlViewModel : BaseViewModel
    {
        public WpfEventManager WpfEventManager { get { return GetValue<WpfEventManager>(); } set { SetValue(value); } }


        public OptionSetValue DefaultValue { get { return GetValue<OptionSetValue>(); } set { SetValue(value); } }

		public List<OptionSetValue> Options { get { return GetValue<List<OptionSetValue>>(); } set { SetValue(value, UpdatedOptionsValue); } }

        public List<OptionSetValue> AvailableOptions { get { return GetValue<List<OptionSetValue>>(); } set { SetValue(value); UpdateListToCollection(value, AvailableOptionsCollection); } }
        public ObservableCollection<OptionSetValue> AvailableOptionsCollection { get; set; } = new ObservableCollection<OptionSetValue>();

        public OptionSetValue CurrentValue { get { return GetValue<OptionSetValue>(); } set { SetValue(value, UpdatedCurrentValue); } }

        private OptionSetInputControlView _view;

		public OptionSetInputControlViewModel()
        {
			
        }

        public void Initialize(OptionSetInputControlView v)
        {
			_view = v;
        }

        private void UpdatedCurrentValue(OptionSetValue value)
        {
            _view.RaiseValueChangedEvent(value);
        }


        //private void UpdatedDefaultValue(OptionSetValue value)
        //{
        //    CurrentValue = value;
        //}

        private void UpdatedOptionsValue(List<OptionSetValue> options)
        {
            AvailableOptions = options;
            if (DefaultValue != null)
            {
                var option = AvailableOptions.FirstOrDefault(k => k.Value == DefaultValue.Value);
                if (option != null)
                {
                    CurrentValue = option;
                }
            }
        }

    }
}
