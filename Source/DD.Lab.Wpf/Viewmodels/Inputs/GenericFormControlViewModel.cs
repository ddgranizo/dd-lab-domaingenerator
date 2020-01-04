using DD.Lab.Wpf.Commands.Base;
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
    public class GenericFormControlViewModel : BaseViewModel
    {


        public WpfEventManager WpfEventManager { get { return GetValue<WpfEventManager>(); } set { SetValue(value); } }

        public GenericFormModel FormModel { get { return GetValue<GenericFormModel>(); } set { SetValue(value, UpdatedFormModel); } }


        public List<GenericFormInputModel> Attributes { get { return GetValue<List<GenericFormInputModel>>(); } set { SetValue(value); UpdateListToCollection(value, AttributesCollection); } }
        public ObservableCollection<GenericFormInputModel> AttributesCollection { get; set; } = new ObservableCollection<GenericFormInputModel>();


        public Dictionary<string, object> Values { get { return GetValue<Dictionary<string, object>>(); } set { SetValue(value); } }

        private GenericFormControlView _view;

        public ICommand ConfirmCommand { get; set; }
        public ICommand CancelCommand { get; set; }

        public GenericFormControlViewModel()
        {
            Values = new Dictionary<string, object>();
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
            Attributes = model.Attributes;
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
            _view.RaiseValueSetChangedEvent(Values, IsDataCompleted());
        }

        private bool IsDataCompleted()
        {
            bool areAllCompleted = true;
            foreach (var item in FormModel.Attributes.Where(k => k.IsMandatory))
            {
                bool isThisCompleted = Values.ContainsKey(item.Key) && Values[item.Key] != null;
                areAllCompleted = !areAllCompleted ? areAllCompleted : isThisCompleted;
            }
            return areAllCompleted;
        }
    }
}
