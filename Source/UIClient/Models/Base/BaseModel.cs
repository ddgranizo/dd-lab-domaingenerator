using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace UIClient.Models.Base
{
    public class BaseModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private Dictionary<string, object> values = new Dictionary<string, object>();

        internal void SetValue<T>(T value, [CallerMemberName]string property = "")
        {
            values[property] = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }

        internal void SetValue<T>(T value, Action<T> handler, [CallerMemberName]string property = "")
        {
            SetValue<T>(value, property);
            handler?.Invoke(value);
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

        internal void RaiseChange(params string[] raiseOtherProperties)
        {
            foreach (var item in raiseOtherProperties)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(item));
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

    }
}
