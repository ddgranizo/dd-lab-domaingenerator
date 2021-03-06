﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Input;
using UIClient.Commands.Base;

namespace UIClient.Models.Base
{
    public class BaseModel : INotifyPropertyChanged
    {
        public List<ICommand> Commands { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        private Dictionary<string, object> values = new Dictionary<string, object>();

        public BaseModel()
        {
            Commands = new List<ICommand>();
        }

        internal void SetValue<T>(T value, [CallerMemberName]string property = "")
        {
            values[property] = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
            RaiseCanExecuteCommandChanged();
        }

        internal void SetValue<T>(T value, Action<T> handler, [CallerMemberName]string property = "")
        {
            SetValue<T>(value, property);
            handler?.Invoke(value);
        }


        public void UpdateListToCollection<T>(List<T> listItems, ObservableCollection<T> collection)
        {
            if (collection == null)
            {
                throw new Exception("Tried to set values into null observable collection at UpdateListToCollection");
            }
            collection.Clear();
            if (listItems != null)
            {
                foreach (var item in listItems)
                {
                    collection.Add(item);
                }
            }
        }

        internal void SetCollection<T>(List<T> value, ObservableCollection<T> observable, [CallerMemberName]string property = "")
        {
            SetValue(value, property);
            if (observable == null)
            {
                observable = new ObservableCollection<T>();
            }
            observable.Clear();
            foreach (var item in value)
            {
                observable.Add(item);
            }
        }


        internal ObservableCollection<T> SetCollection<T>(List<T> value, [CallerMemberName]string property = "")
        {
            SetValue(value, property);
            var targetCollection = new ObservableCollection<T>();
            if (value != null)
            {
                targetCollection = new ObservableCollection<T>(value);
            }
            return targetCollection;
        }

        internal void RaisePropertyChange(params string[] raiseOtherProperties)
        {
            foreach (var item in raiseOtherProperties)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(item));
            }
        }

        internal void RaiseAllPropertiesChange()
        {
            foreach (var item in values)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(item.Key));
            }
        }

        internal T GetValue<T>([CallerMemberName]string property = "")
        {
            if (values.ContainsKey(property))
            {
                return (T)values[property];
            }
            values[property] = default(T);
            return (T)values[property];
        }

        internal void RegisterCommand(ICommand command)
        {
            Commands.Add(command);
        }

        public void RaiseCanExecuteCommandChanged(object param = null)
        {
            foreach (var command in Commands)
            {
                RaiseCanExecuteCommandChanged(command, param);
            }
        }

        protected void RaiseCanExecuteCommandChanged(ICommand command, object param = null)
        {
            System.Windows.Application app = System.Windows.Application.Current;
            if (app != null)
            {
                if (!app.Dispatcher.CheckAccess())
                {
                    app.Dispatcher.Invoke(new System.Action(() => this.RaiseCanExecuteCommandChanged(command, param)));
                }
                else
                {
                    ((RelayCommand)command).RaiseCanExecuteChanged(param);
                }
            }
        }

    }
}
