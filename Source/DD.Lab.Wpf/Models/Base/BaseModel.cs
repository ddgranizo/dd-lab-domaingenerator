using DD.Lab.Wpf.Commands.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Input;

namespace DD.Lab.Wpf.Models.Base
{
    public class BaseModel : INotifyPropertyChanged
    {
        public List<ICommand> Commands { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        private TrazableDictionary<string, object> _values = new TrazableDictionary<string, object>();
        private Dictionary<string, object> _previewsValues = new Dictionary<string, object>();

        public List<PropertiesTrigger> PropertiesTriggers { get; set; }

        public BaseModel()
        {
            Commands = new List<ICommand>();
            PropertiesTriggers = new List<PropertiesTrigger>();

            _values.OnDictionaryChanged += Values_OnDictionaryChanged;
        }

        private void Values_OnDictionaryChanged(object sender, Events.TrazableDictionaryChangedEventArgs<string, object> e)
        {
            if (e.Type == TrazableDictionary<string, object>.Type.AddItem)
            {
                _previewsValues.Add(e.Key, null);
            }
            else if(e.Type == TrazableDictionary<string, object>.Type.Clear)
            {
                _previewsValues.Clear();
            }
            else if (e.Type == TrazableDictionary<string, object>.Type.ModifiedItem)
            {
                _previewsValues[e.Key] = e.OldValue;
            }
            else if (e.Type == TrazableDictionary<string, object>.Type.RemoveItem)
            {
                _previewsValues[e.Key] = e.OldValue;
            }
        }

        public void SetValue<T>(T value, [CallerMemberName]string property = "")
        {
            _values[property] = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
            if (PropertyHasChanged(property))
            {
                RaiseCanExecuteCommandChanged();
                CheckTriggers(property);
            }
        }

        public void SetValue<T>(T value, Action<T> handler, [CallerMemberName]string property = "")
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
            foreach (var item in _values)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(item.Key));
            }
        }

        public T GetValue<T>([CallerMemberName]string property = "")
        {
            if (_values.ContainsKey(property))
            {
                return (T)_values[property];
            }
            _values[property] = default(T);
            return (T)_values[property];
        }

        public void RegisterCommand(ICommand command)
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

        public void AddSetterPropertiesTrigger(PropertiesTrigger trigger)
        {
            PropertiesTriggers.Add(trigger);
        }

        private void CheckTriggers(string triggerProperty)
        {
            foreach (var item in PropertiesTriggers.Where(k=>k.Properties.Any(l=>l == triggerProperty)))
            {
                if (PropertiesHasChanged(item.Properties) )
                {
                    if (item.Mode == PropertiesTrigger.TriggerMode.EveryChange 
                            || (item.Mode == PropertiesTrigger.TriggerMode.OnlyOnce 
                                && item.ExecutionTimes == 0))
                    {
                        item.Action.Invoke();
                        item.ExecutionTimes++;
                    }
                }
            }
        }

        private bool PropertiesHasChanged(string[] properties)
        {
            bool hasChanged = false;
            foreach (var item in properties)
            {
                hasChanged = hasChanged ? hasChanged : PropertyHasChanged(item);
            }
            return hasChanged;
        }

        private bool PropertyHasChanged(string property)
        {
            return _values[property] != _previewsValues[property];
        }

    }
}
