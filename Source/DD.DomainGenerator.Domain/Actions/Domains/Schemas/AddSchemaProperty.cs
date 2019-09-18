using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using DD.DomainGenerator.Actions.Base;
using DD.DomainGenerator.Extensions;
using DD.DomainGenerator.Models;
using DD.DomainGenerator.Utilities;

namespace DD.DomainGenerator.Actions.Domains.Schemas
{
    public class AddSchemaProperty : ActionBase
    {
        public const string ActionName = "AddSchemaProperty";
        public ActionParameterDefinition DomainNameParameter { get; set; }
        public ActionParameterDefinition NameParameter { get; set; }
        public ActionParameterDefinition TypeParameter { get; set; }
        public ActionParameterDefinition LengthParameter { get; set; }
        public ActionParameterDefinition IsPrimaryKeyParameter { get; set; }
        public ActionParameterDefinition IsNullableParameter { get; set; }
        public ActionParameterDefinition IsUniqueParameter { get; set; }
        public ActionParameterDefinition IsAutoIncrementalParameter { get; set; }
        public AddSchemaProperty() : base(ActionName)
        {
            DomainNameParameter = new ActionParameterDefinition(
                "domainname", ActionParameterDefinition.TypeValue.String, "Domain name", "d");
            NameParameter = new ActionParameterDefinition(
                "name", ActionParameterDefinition.TypeValue.String, "Name. Use PascalCase or the name will be converted to PascalCase automatically", "n");
            TypeParameter = new ActionParameterDefinition(
                "type", ActionParameterDefinition.TypeValue.String, "Type. Possible values: Guid = 1, Boolean = 2, Integer = 3, Decimal = 4,  Float = 5, Time = 6, DateTime = 7, String = 8, LongString = 9, Password = 99, ",
                "t")
            {
                InputSuggestions = new List<string>()
                {
                    "Guid", "Boolean", "Integer", "Decimal", "Float","Time", "DateTime", "String", "LongString", "Password"
                }
            };

            LengthParameter = new ActionParameterDefinition(
                "length", ActionParameterDefinition.TypeValue.Integer, "Length. Use only for String types", "l");
            IsPrimaryKeyParameter = new ActionParameterDefinition(
               "primarykey", ActionParameterDefinition.TypeValue.Boolean, "Is primary key", "pk");
            IsNullableParameter = new ActionParameterDefinition(
               "nullable", ActionParameterDefinition.TypeValue.Boolean, "Is nullable", "nl");
            IsUniqueParameter = new ActionParameterDefinition(
              "unique", ActionParameterDefinition.TypeValue.Boolean, "Is unique key", "un");
            IsAutoIncrementalParameter = new ActionParameterDefinition(
             "autoincrement", ActionParameterDefinition.TypeValue.Boolean, "Is autoincrement. Only valid for Integer types", "ai");

            ActionParametersDefinition.Add(DomainNameParameter);
            ActionParametersDefinition.Add(NameParameter);
            ActionParametersDefinition.Add(TypeParameter);
            ActionParametersDefinition.Add(LengthParameter);
            ActionParametersDefinition.Add(IsPrimaryKeyParameter);
            ActionParametersDefinition.Add(IsNullableParameter);
            ActionParametersDefinition.Add(IsUniqueParameter);
            ActionParametersDefinition.Add(IsAutoIncrementalParameter);

        }

        public override bool CanExecute(ProjectState project, List<ActionParameter> parameters)
        {
            return IsParamOk(parameters, DomainNameParameter) && IsParamOk(parameters, NameParameter) && IsParamOk(parameters, TypeParameter);
        }

        public override void ExecuteStateChange(ProjectState project, List<ActionParameter> parameters)
        {
            var domainName = GetStringParameterValue(parameters, DomainNameParameter, string.Empty).ToWordPascalCase();
            var name = GetStringParameterValue(parameters, NameParameter, string.Empty).ToWordPascalCase();
            var type = GetStringParameterValue(parameters, TypeParameter, string.Empty);
            var length = GetIntParameterValue(parameters, LengthParameter, -1);
            var isPrimaryKey = GetBoolParameterValue(parameters, IsPrimaryKeyParameter, false);
            var isNullable = GetBoolParameterValue(parameters, IsNullableParameter, true);
            var isUnique = GetBoolParameterValue(parameters, IsUniqueParameter, false);
            var isAutoincremental = GetBoolParameterValue(parameters, IsAutoIncrementalParameter, false);

            var domain = Domain.FindChildDomain(project.Domain, domainName);
            if (domain == null)
            {
                throw new Exception($"Can't find any domain named '{domainName}'");
            }

            if (!domain.HasModel)
            {
                throw new Exception($"Domain has not yet initialized the schema");
            }

            var typedType = SchemaModelProperty.StringToType(type);
            var property = new SchemaModelProperty(name, typedType)
            {
                IsAutoIncremental = isAutoincremental,
                IsNullable = isNullable,
                IsPrimaryKey = isPrimaryKey,
                IsUnique = isUnique,
                Length = length
            };

            domain.Schema.AddProperty(property);
        }
    }
}
