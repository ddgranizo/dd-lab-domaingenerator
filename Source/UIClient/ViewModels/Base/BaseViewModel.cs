using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows;
using System.Windows.Input;
using UIClient.Models.Base;

namespace UIClient.ViewModels.Base
{
    public class BaseViewModel : BaseModel
    {
        public List<ICommand> Commands { get; set; }
        public BaseViewModel()
        {
            Commands = new List<ICommand>();
        }

        protected virtual void RaiseError(string message)
        {
            MessageBox.Show(message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        protected virtual void RaiseOkCancelDialog(string message, string title, Action okAction, Action cancelAction = null)
        {
            var response = MessageBox.Show(message, "", MessageBoxButton.OKCancel, MessageBoxImage.Information);
            if (response == MessageBoxResult.OK)
            {
                okAction.Invoke();
            }
            else
            {
                cancelAction?.Invoke();
            }
        }

        internal void RegisterCommand(ICommand command)
        {
            Commands.Add(command);
        }
    }
}
