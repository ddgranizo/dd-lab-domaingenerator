using DD.DomainGenerator.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DD.DomainGenerator.Extensions
{
    public static class DictionaryExtensions
    {

        public static List<ActionParameter> ToActionParametersList(this Dictionary<string, object> dictionary)
        {
            var output = new List<ActionParameter>();

            foreach (var item in dictionary)
            {
                var line = new ActionParameter();
                line.ParameterName = item.Key;
                var value = item.Value;
                if (value.GetType() == typeof(bool))
                {
                    line.ValueBool = (bool)value;
                }
                else if (value.GetType() == typeof(int))
                {
                    line.ValueInt = (int)value;
                }
                else if (value.GetType() == typeof(decimal))
                {
                    line.ValueDecimal = (decimal)value;
                }
                else if (value.GetType() == typeof(Guid))
                {
                    line.ValueGuid = (Guid)value;
                }
                else if (value.GetType() == typeof(string))
                {
                    line.ValueString = (string)value;
                }
                output.Add(line);
            }
            return output;
        }
    }
}
