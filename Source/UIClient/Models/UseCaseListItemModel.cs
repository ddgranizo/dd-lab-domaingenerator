using DD.DomainGenerator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UIClient.Models.Base;

namespace UIClient.Models
{
    public class UseCaseListItemModel : BaseModel
    {
        public UseCaseModel UseCase { get { return GetValue<UseCaseModel>(); } set { SetValue(value); } }
        public DomainModel Domain { get { return GetValue<DomainModel>(); } set { SetValue(value); } }
        public SchemaModel Schema { get { return GetValue<SchemaModel>(); } set { SetValue(value); } }

        public string GetTypeDisplayName(UseCaseParameter.InputType type)
        {
            return type == UseCaseParameter.InputType.DomainEntity
                            ? Schema.Name
                            : type.ToString();
        }


        public string Namespace
        {
            get
            {
                return $"{Domain.Name}.{Schema.Name}";
            }
        }

        public string DisplayName
        {
            get
            {
                return $"{UseCase.Name}";
            }
        }

        public string CompleteDisplayName
        {
            get
            {
                return $"{Namespace} {OutputTypeDisplayName} {DisplayName} ({InputTypesDisplayName})";
            }
        }

        public string InputTypesDisplayName
        {
            get
            {
                return string.Join(",", UseCase.InputParameters.Select(k => $"{GetTypeDisplayName(k.Type)} {k.Name}"));
            }
        }

        public string OutputTypeDisplayName
        {
            get
            {
                if (UseCase.OutputParameters.Count == 0)
                {
                    return "Void";
                }
                var outputParameter = UseCase.OutputParameters.First();
                if (outputParameter.Type == UseCaseParameter.InputType.DomainEntity)
                {
                    return Schema.Name;
                }
                else if (outputParameter.Type == UseCaseParameter.InputType.Enumerable)
                {
                    var enumerableType = GetTypeDisplayName(outputParameter.EnumerableType);
                    return $"IEnumerable<{enumerableType}>";
                }
                else if (outputParameter.Type == UseCaseParameter.InputType.Dictionary)
                {
                    var keyType = GetTypeDisplayName(outputParameter.DictionaryKeyType);
                    var valueType = GetTypeDisplayName(outputParameter.DictionaryValueType);
                    return $"Dictionary<{keyType}, {valueType}>";
                }
                else
                {
                    return outputParameter.Type.ToString();
                }
            }
        }
    }
}
