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
            //RetrieveMultiple = 40,
            //RetrieveMultipleIntersection = 46,
            //Authorise = 99
            View = 40,
            Custom = 99,
        }




        public string Name { get; set; }
        public UseCaseTypes Type { get; set; }
        public Schema Schema { get; set; }
        public bool IsCustom { get; set; }

        public string DisplayName { get; set; }
        public string Description { get; set; }

        public List<UseCaseParameter> InputParameters { get; set; }
        public List<UseCaseParameter> OutputParameters { get; set; }

        public ExecutionSequence ExecutionSequence { get; set; }

        public UseCase()
        {
            InputParameters = new List<UseCaseParameter>();
            OutputParameters = new List<UseCaseParameter>();
        }

        public UseCase(string name)
            :this()
        {
            Type = UseCaseTypes.Custom;
            Name = name;
            IsCustom = true;
        }

        public UseCase(UseCaseTypes type)
            : this()
        {
            Type = type;
            Name = type.ToString();
            IsCustom = false;
            if (type == UseCaseTypes.RetrieveByPk)
            {
                InputParameters.Add(new UseCaseParameter(UseCaseParameter.InputType.Guid, "Id"));
                OutputParameters.Add(new UseCaseParameter(UseCaseParameter.InputType.DomainEntity, "Entity"));
            }
            else if(type == UseCaseTypes.RetrieveByUn)
            {
                InputParameters.Add(new UseCaseParameter(UseCaseParameter.InputType.String, "Name"));
                OutputParameters.Add(new UseCaseParameter(UseCaseParameter.InputType.DomainEntity, "Entity"));
            }
            else if (type == UseCaseTypes.Create)
            {
                InputParameters.Add(new UseCaseParameter(UseCaseParameter.InputType.DomainEntity, "Entity"));
                OutputParameters.Add(new UseCaseParameter(UseCaseParameter.InputType.Guid, "Id"));
            }
            else if (type == UseCaseTypes.Update)
            {
                InputParameters.Add(new UseCaseParameter(UseCaseParameter.InputType.DomainEntity, "Entity"));
            }
            else if (type == UseCaseTypes.DeleteByPk)
            {
                InputParameters.Add(new UseCaseParameter(UseCaseParameter.InputType.Guid, "Id"));
            }
            else if (type == UseCaseTypes.DeleteByUn)
            {
                InputParameters.Add(new UseCaseParameter(UseCaseParameter.InputType.String, "Name"));
            }
        }


        public UseCase(UseCaseTypes type, Schema schema)
            : this()
        {
            Type = type;
            Name = type.ToString();
            Schema = schema ?? throw new ArgumentNullException(nameof(schema));
            IsCustom = false;
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
