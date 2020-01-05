using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static DD.DomainGenerator.Definitions;

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

        public List<DataParameter> InputParameters { get; set; }
        public List<DataParameter> OutputParameters { get; set; }

        public UseCaseExecution Execution { get; set; }

        public UseCase()
        {
            InputParameters = new List<DataParameter>();
            OutputParameters = new List<DataParameter>();
            Execution = new UseCaseExecution(this);
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
                InputParameters.Add(new DataParameter(DomainInputType.Guid, "Id"));
                OutputParameters.Add(new DataParameter(DomainInputType.DomainEntity, "Entity"));
            }
            else if(type == UseCaseTypes.RetrieveByUn)
            {
                InputParameters.Add(new DataParameter(DomainInputType.String, "Name"));
                OutputParameters.Add(new DataParameter(DomainInputType.DomainEntity, "Entity"));
            }
            else if (type == UseCaseTypes.Create)
            {
                InputParameters.Add(new DataParameter(DomainInputType.DomainEntity, "Entity"));
                OutputParameters.Add(new DataParameter(DomainInputType.Guid, "Id"));
            }
            else if (type == UseCaseTypes.Update)
            {
                InputParameters.Add(new DataParameter(DomainInputType.DomainEntity, "Entity"));
            }
            else if (type == UseCaseTypes.DeleteByPk)
            {
                InputParameters.Add(new DataParameter(DomainInputType.Guid, "Id"));
            }
            else if (type == UseCaseTypes.DeleteByUn)
            {
                InputParameters.Add(new DataParameter(DomainInputType.String, "Name"));
            }
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
