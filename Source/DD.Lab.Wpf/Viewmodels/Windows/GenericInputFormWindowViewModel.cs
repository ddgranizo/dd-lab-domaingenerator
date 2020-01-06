using DD.Lab.Wpf.Commands.Base;
using DD.Lab.Wpf.Models.Inputs;
using DD.Lab.Wpf.ViewModels.Base;
using DD.Lab.Wpf.Windows;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace DD.Lab.Wpf.Viewmodels.Windows
{
    public class GenericInputFormWindowViewModel: BaseViewModel
    {
        public GenericFormModel Model { get { return GetValue<GenericFormModel>(); } set { SetValue(value); } }
        public Dictionary<string, object> Values { get { return GetValue<Dictionary<string, object>>(); } set { SetValue(value, UpdatedValues); } }

        public GenericInputFormWindowViewModel()
        {
            InitializeCommands();
        }

        public bool HasUpdated { get; set; } = false;

        private GenericInputFormWindow _view;
        public void Initialize(GenericInputFormWindow view, GenericFormModel model)
        {
            Model = model ?? throw new ArgumentNullException(nameof(model));
            _view = view;
        }

        public ICommand SaveCommand { get; set; }

        private void InitializeCommands()
        {
            SaveCommand = new RelayCommand((input) =>
            {
                _view.Values = Values;
                _view.Response = WindowResponse.OK;
                _view.Close();
            });

            RegisterCommand(SaveCommand);
        }


        private void UpdatedValues(Dictionary<string, object> data)
        {
            HasUpdated = true;
        }
    }
}
