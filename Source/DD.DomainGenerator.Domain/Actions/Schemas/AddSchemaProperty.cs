using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using DD.DomainGenerator.Actions.Base;
using DD.DomainGenerator.Extensions;
using DD.DomainGenerator.Models;
using DD.DomainGenerator.Utilities;

namespace DD.DomainGenerator.Actions.Schemas
{
    public class AddSchemaProperty : ActionBase
    {
        public const string ActionName = "AddSchemaProperty";
        public ActionParameterDefinition SchemaNameParameter { get; set; }
        public ActionParameterDefinition NameParameter { get; set; }
        public ActionParameterDefinition TypeParameter { get; set; }
        public ActionParameterDefinition LengthParameter { get; set; }
        public ActionParameterDefinition IsPrimaryKeyParameter { get; set; }
        public ActionParameterDefinition IsNullableParameter { get; set; }
        public ActionParameterDefinition IsUniqueParameter { get; set; }
        public ActionParameterDefinition IsAutoIncrementalParameter { get; set; }
        public AddSchemaProperty() : base(ActionName)
        {
            SchemaNameParameter = new ActionParameterDefinition(
                "schemaname", ActionParameterDefinition.TypeValue.String, "Schema name", "s", string.Empty)
            { IsSchemaSuggestion = true };
            NameParameter = new ActionParameterDefinition(
                "name", ActionParameterDefinition.TypeValue.String, "Name. Use PascalCase or the name will be converted to PascalCase automatically", "n", string.Empty);
            TypeParameter = new ActionParameterDefinition(
                "type", ActionParameterDefinition.TypeValue.String, "Type. Possible values: Guid = 1, Boolean = 2, Integer = 3, Decimal = 4,  Float = 5, Time = 6, DateTime = 7, String = 8, LongString = 9, Password = 99, ",
                "t", string.Empty)
            { InputSuggestions = SchemaModelProperty.GetUseCaseTypesList() };

            LengthParameter = new ActionParameterDefinition(
                "length", ActionParameterDefinition.TypeValue.Integer, "Length. Use only for String types", "l", 0);
            IsPrimaryKeyParameter = new ActionParameterDefinition(
               "primarykey", ActionParameterDefinition.TypeValue.Boolean, "Is primary key", "pk", false);
            IsNullableParameter = new ActionParameterDefinition(
               "nullable", ActionParameterDefinition.TypeValue.Boolean, "Is nullable", "nl", false);
            IsUniqueParameter = new ActionParameterDefinition(
              "unique", ActionParameterDefinition.TypeValue.Boolean, "Is unique key", "un", false);
            IsAutoIncrementalParameter = new ActionParameterDefinition(
             "autoincrement", ActionParameterDefinition.TypeValue.Boolean, "Is autoincrement. Only valid for Integer types", "ai", false);

            ActionParametersDefinition.Add(SchemaNameParameter);
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
            return IsParamOk(parameters, SchemaNameParameter) && IsParamOk(parameters, NameParameter) && IsParamOk(parameters, TypeParameter);
        }

        public override void ExecuteStateChange(ProjectState project, List<ActionParameter> parameters)
        {
            var schemaName = GetStringParameterValue(parameters, SchemaNameParameter).ToWordPascalCase();
            var name = GetStringParameterValue(parameters, NameParameter).ToWordPascalCase();
            var type = GetStringParameterValue(parameters, TypeParameter);
            var length = GetIntParameterValue(parameters, LengthParameter);
            var isPrimaryKey = GetBoolParameterValue(parameters, IsPrimaryKeyParameter);
            var isNullable = GetBoolParameterValue(parameters, IsNullableParameter);
            var isUnique = GetBoolParameterValue(parameters, IsUniqueParameter);
            var isAutoincremental = GetBoolParameterValue(parameters, IsAutoIncrementalParameter);

            var schema = project.GetSchema(schemaName);
            if (schema == null)
            {
                throw new Exception($"Can't find any schema named '{schemaName}'");
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

            schema.AddProperty(property);
        }
    }
}
