using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DD.Basic.Extensions
{
    public static class EnumerableExtensions
    {

        public static IEnumerable<T> MoveItemUp<T>(this IEnumerable<T> enumerable, T item)
        {
            return MoveItem(enumerable, enumerable.ToList().IndexOf(item), enumerable.ToList().IndexOf(item) - 1);
        }

        public static IEnumerable<T> MoveItemDown<T>(this IEnumerable<T> enumerable, T item)
        {
            return MoveItem(enumerable, enumerable.ToList().IndexOf(item), enumerable.ToList().IndexOf(item) + 1);
        }

        public static IEnumerable<T> MoveItem<T>(this IEnumerable<T> enumerable, T item, int newIndex)
        {
            return MoveItem(enumerable, enumerable.ToList().IndexOf(item), newIndex);
        }

        public static IEnumerable<T> MoveItem<T>(this IEnumerable<T> enumerable, int oldIndex, int newIndex)
        {
            var list = enumerable.ToList();
            var item = list[oldIndex];
            list.RemoveAt(oldIndex);
            list.Insert(newIndex, item);
            return list;
        }
    }
}
