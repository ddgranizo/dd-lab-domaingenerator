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
    public class GenericFormInputControlViewModel : BaseViewModel
    {
        public WpfEventManager WpfEventManager { get { return GetValue<WpfEventManager>(); } set { SetValue(value); } }

        public GenericFormInputModel InputModel { get { return GetValue<GenericFormInputModel>(); } set { SetValue(value); } }

       

        private GenericFormInputControlView _view;

        public GenericFormInputControlViewModel()
        {

        }

        public void Initialize(GenericFormInputControlView v)
        {
            _view = v;
        }

        
    }
}
