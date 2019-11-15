using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DD.DomainGenerator.Models
{
    public class UseCase
    {
        public enum UseCaseTypes
        {
            RetrieveByPk = 1,
            RetrieveByUn = 2,
            Create = 10,
            DeleteByPk = 20,
            DeleteByUn = 21,
            Update = 30,
            RetrieveMultiple = 40,
            RetrieveMultipleIntersection = 46,
            Authorise = 99
        }

        public UseCaseTypes Type { get; set; }
        public SchemaModelProperty Attribute { get; set; }
        public Schema Schema { get; set; }

        public bool NeedsAuthorization { get; set; }

        public UseCase()
        {

        }

        public UseCase(UseCaseTypes type)
        {
            Type = type;
        }

        public UseCase(UseCaseTypes type, SchemaModelProperty attribute)
        {
            Type = type;
            Attribute = attribute ?? throw new ArgumentNullException(nameof(attribute));
        }

        public UseCase(UseCaseTypes type, Schema schema)
        {
            Type = type;
            Schema = schema ?? throw new ArgumentNullException(nameof(schema));
        }

        public static List<string> GetUseCaseTypesList()
        {
            return Enum.GetNames(typeof(UseCaseTypes)).ToList();
        }


        public static UseCaseTypes StringToType(string type)
        {
            foreach (var item in Enum.GetValues(typeof(UseCaseTypes)))
            {
                var name = Enum.GetName(typeof(UseCaseTypes), item);
                if (name == type)
                {
                    return (UseCaseTypes)item;
                }
            }
            throw new Exception($"Can't find type named {type}");
        }
    }
}
