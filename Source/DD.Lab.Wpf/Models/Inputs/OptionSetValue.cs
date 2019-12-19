using System;
using System.Collections.Generic;
using System.Text;

namespace DD.Lab.Wpf.Models.Inputs
{
    public class OptionSetValue
    {
        public string DisplayName { get; set; }
        public int Value { get; set; }

        public OptionSetValue(string displayName, int value)
        {
            DisplayName = displayName;
            Value = value;
        }

        public OptionSetValue(int value)
        {
            Value = value;
        }

        public OptionSetValue()
        {
        }
    }
}
