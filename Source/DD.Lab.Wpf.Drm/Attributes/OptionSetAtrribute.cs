using System;
using System.Collections.Generic;
using System.Text;

namespace DD.Lab.Wpf.Drm.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public class OptionSetAtrribute : Attribute
    {
        public string DisplayName { get; set; }
        public int Value { get; set; }

    }
}
