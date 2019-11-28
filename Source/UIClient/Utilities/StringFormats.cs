using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UIClient.Models;
using static DD.DomainGenerator.Definitions;

namespace UIClient.Utilities
{
    public static class StringFormats
    {

        public static string GetDataParametersDisplayName(List<DataParameterModel> parameters, string schemaName = null)
        {
            if (parameters == null)
            {
                return string.Empty;
            }
            if (parameters.Count == 0)
            {
                return "Void";
            }
            var outputParameter = parameters.First();
            if (outputParameter.Type == DomainInputType.DomainEntity)
            {
                return schemaName ?? "DomainEntity";
            }
            else if (outputParameter.Type == DomainInputType.Enumerable)
            {
                var enumerableType = GetTypeDisplayName(outputParameter.EnumerableType, schemaName);
                return $"IEnumerable<{enumerableType}>";
            }
            else if (outputParameter.Type == DomainInputType.Dictionary)
            {
                var keyType = GetTypeDisplayName(outputParameter.DictionaryKeyType, schemaName);
                var valueType = GetTypeDisplayName(outputParameter.DictionaryValueType, schemaName);
                return $"Dictionary<{keyType}, {valueType}>";
            }
            else
            {
                return outputParameter.Type.ToString();
            }
        }

        public static string GetTypeDisplayName(DomainInputType type, string schemaName = null)
        {
            return type == DomainInputType.DomainEntity
                            ? schemaName ?? "DomainEntity"
                            : type.ToString();
        }
    }
}
