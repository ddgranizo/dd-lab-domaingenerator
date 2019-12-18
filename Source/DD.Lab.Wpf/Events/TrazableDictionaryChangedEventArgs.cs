using DD.Lab.Wpf.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DD.Lab.Wpf.Events
{
    public class TrazableDictionaryChangedEventArgs<K, V> : EventArgs
    {
        public TrazableDictionary<K,V>.Type Type { get; set; }

        public K Key { get; set; }

        public V Value { get; set; }

        public V OldValue { get; set; }
    }
}
