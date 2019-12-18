using DD.Lab.Wpf.Events;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace DD.Lab.Wpf.Models
{

    public class TrazableDictionary<K, V> : IDictionary<K, V>
    {
        public enum Type
        {
            AddItem,
            RemoveItem,
            ModifiedItem,
            Clear,
        }

        public Dictionary<K, V> GetDictionary() { return (Dictionary<K, V>)innerDict; }

        public delegate void DictionaryChanged(object sender, TrazableDictionaryChangedEventArgs<K, V> e);

        public event DictionaryChanged OnDictionaryChanged;

        private IDictionary<K, V> innerDict;

        public ICollection<K> Keys => innerDict.Keys;

        public ICollection<V> Values => innerDict.Values;

        public int Count => innerDict.Count;

        public bool IsReadOnly => innerDict.IsReadOnly;

        public V this[K key] { get => innerDict[key]; set => Modify(key, value); }

        public TrazableDictionary()
        {
            innerDict = new Dictionary<K, V>();
        }


        public void Modify(K key, V value)
        {
            if (!innerDict.ContainsKey(key))
            {
                Add(key, value);
            }
            else
            {
                var oldValue = innerDict[key];
                innerDict[key] = value;
                OnDictionaryChanged?.Invoke(this, new TrazableDictionaryChangedEventArgs<K, V>()
                {
                    Type = Type.ModifiedItem,
                    Key = key,
                    Value = value,
                    OldValue = oldValue,
                });
            }
           
        }

        public void Add(K key, V value)
        {
            innerDict.Add(key, value);
            OnDictionaryChanged?.Invoke(this, new TrazableDictionaryChangedEventArgs<K, V>()
            {
                Type = Type.AddItem,
                Key = key,
                Value = value
            });
        }

        public bool ContainsKey(K key)
        {
            return innerDict.ContainsKey(key);
        }

        public bool Remove(K key)
        {
            var oldValue = innerDict[key];
            bool removed = innerDict.Remove(key);
            OnDictionaryChanged?.Invoke(this, new TrazableDictionaryChangedEventArgs<K, V>()
            {
                Type = Type.RemoveItem,
                Key = key,
                OldValue = oldValue,
            });
            return removed;
        }

        public bool TryGetValue(K key, out V value)
        {
            return innerDict.TryGetValue(key, out value);
        }

        public void Add(KeyValuePair<K, V> item)
        {
            innerDict.Add(item);
        }

        public void Clear()
        {
            innerDict.Clear();
            OnDictionaryChanged?.Invoke(this, new TrazableDictionaryChangedEventArgs<K, V>()
            {
                Type = Type.Clear,
            });
        }

        public bool Contains(KeyValuePair<K, V> item)
        {
            return innerDict.Contains(item);
        }

        public void CopyTo(KeyValuePair<K, V>[] array, int arrayIndex)
        {
            innerDict.CopyTo(array, arrayIndex);
        }

        public bool Remove(KeyValuePair<K, V> item)
        {
            return innerDict.Remove(item);
        }

        public IEnumerator<KeyValuePair<K, V>> GetEnumerator()
        {
            return innerDict.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return innerDict.Keys.GetEnumerator();
        }

    }
}
