using DD.Lab.Wpf.Models.Inputs;
using System;
using System.Collections.Generic;
using System.Text;

namespace DomainGeneratorUI.Extensions
{
    public static class EnumExtensions
    {
        public static OptionSetValue ToOptionSetValue<T>(string value) where T : System.Enum
        {
            var values = Enum.GetValues(typeof(T));
            foreach (var item in values)
            {
                var name = Enum.GetName(typeof(T), item);
                if (name == value)
                {
                    return new OptionSetValue((int)item);
                }
            }
            throw new Exception($"Can't find any option named '{value}' for this enum");
        }
    }
}
