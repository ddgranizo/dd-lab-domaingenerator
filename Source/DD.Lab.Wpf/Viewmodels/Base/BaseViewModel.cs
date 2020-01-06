using DD.Lab.Wpf.Models.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows;
using System.Windows.Input;


namespace DD.Lab.Wpf.ViewModels.Base
{
    public class BaseViewModel : BaseModel
    {
        
        public BaseViewModel()
        {
            
        }

        public virtual void RaiseError(string message)
        {
            MessageBox.Show(message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        public virtual void RaiseOkCancelDialog(string message, string title, Action okAction, Action cancelAction = null)
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

        public static T GetDictionaryValue<T>(Dictionary<string, object> data, string key, T defaultValue = default)
        {
            if (data.ContainsKey(key))
            {
                return (T)data[key];
            }
            return defaultValue;
        }

        //public string GetInputText(string description, string caption)
        //{
        //    var view = new InputTextBox(description, caption);
        //    view.ShowDialog();
        //    return view.ReturnedText;
        //}
        
    }
}
