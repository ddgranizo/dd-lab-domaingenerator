using DD.Lab.Wpf.Drm.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DD.Lab.Wpf.Drm.Extensions
{
    public static class DictionaryExtensions
    {
        public static T ToEntity<T>(this Dictionary<string, object> dic) where T : new()
        {
            return Entity.DictionartyToEntity<T>(dic);
        }
    }
}
