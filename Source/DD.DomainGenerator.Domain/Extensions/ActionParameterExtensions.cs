using DD.DomainGenerator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DD.DomainGenerator.Extensions
{
    public static class ActionParameterExtensions
    {
        public static Dictionary<string, object> ToDictionary(this List<ActionParameter> parameters, List<ActionParameterDefinition> parametersDefinitions)
        {
            Dictionary<string, object> output = new Dictionary<string, object>();
            foreach (var item in parameters)
            {
                var definition = parametersDefinitions.FirstOrDefault(k => k.Name == item.ParameterName);
                if (definition != null)
                {
                    object value = null;
                    if (definition.Type == ActionParameterDefinition.TypeValue.Boolean)
                    {
                        value = item.ValueBool;
                    }
                    else if (definition.Type == ActionParameterDefinition.TypeValue.Decimal)
                    {
                        value = item.ValueDecimal;
                    }
                    else if (definition.Type == ActionParameterDefinition.TypeValue.Guid)
                    {
                        value = item.ValueGuid;
                    }
                    else if (definition.Type == ActionParameterDefinition.TypeValue.Integer)
                    {
                        value = item.ValueInt;
                    }
                    else if (definition.Type == ActionParameterDefinition.TypeValue.String)
                    {
                        value = item.ValueString;
                    }
                    else if (definition.Type == ActionParameterDefinition.TypeValue.Password)
                    {
                        value = item.ValueString;
                    }
                    output.Add(item.ParameterName, value);
                }
            }
            return output;
        }
    }
}
