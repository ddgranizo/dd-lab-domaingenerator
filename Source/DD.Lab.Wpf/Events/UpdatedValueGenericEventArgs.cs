using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace DD.Lab.Wpf.Events
{
    public class UpdatedValueGenericEventArgs<T> : RoutedEventArgs
    {
        public T Data { get; set; }

        public UpdatedValueGenericEventArgs()
        {
        }

        public UpdatedValueGenericEventArgs(T data)
        {
            Data = data;
        }
    }
}
