using DD.Lab.Wpf.Models.Inputs;
using DomainGeneratorUI.Models.Methods;
using DomainGeneratorUI.Viewmodels.Methods;
using System;
using System.Collections.Generic;
using System.Text;

namespace DomainGeneratorUI.Extensions
{
    public static class MethodParameterViewmodelExtensions
    {

        public static GenericFormModel ToGenericInputModel(this MethodParameterViewmodel parameter, string formDescription, OptionSetValue[] options)
        {
            var model = new GenericFormModel(formDescription);
            model.AddAttribute(
                GenericFormInputModel.TypeValue.String,
                nameof(MethodParameterViewmodel.Name),
                nameof(MethodParameterViewmodel.Name),
                nameof(MethodParameterViewmodel.Name),
                true,
                parameter.Name);


            var directionAttr = new GenericFormInputModel()
            {
                DisplayName = nameof(MethodParameterViewmodel.Direction),
                Key = nameof(MethodParameterViewmodel.Direction),
                Description = nameof(MethodParameterViewmodel.Direction),
                IsMandatory = true,
                Type = GenericFormInputModel.TypeValue.OptionSet,
                OptionSetValueOptions = new List<OptionSetValue>()
                {
                    {new OptionSetValue(MethodParameter.ParameterDirection.Input.ToString(), (int)MethodParameter.ParameterDirection.Input) },
                    {new OptionSetValue(MethodParameter.ParameterDirection.Output.ToString(), (int)MethodParameter.ParameterDirection.Output) }
                }.ToArray(),
                DefaultValue = ((int)parameter.Direction) > 0 ? new OptionSetValue((int)parameter.Direction) : null
            };


            var typeAttr = new GenericFormInputModel()
            {
                DisplayName = nameof(MethodParameterViewmodel.Type),
                Key = nameof(MethodParameterViewmodel.Type),
                Description = nameof(MethodParameterViewmodel.Type),
                IsMandatory = true,
                Type = GenericFormInputModel.TypeValue.OptionSet,
                OptionSetValueOptions = options,
                DefaultValue = ((int)parameter.Type) > 0 ? new OptionSetValue((int)parameter.Type) : null
            };

            var enumAttr = new GenericFormInputModel()
            {
                DisplayName = nameof(MethodParameterViewmodel.EnumerableType),
                Key = nameof(MethodParameterViewmodel.EnumerableType),
                Description = nameof(MethodParameterViewmodel.EnumerableType),
                IsMandatory = false,
                Type = GenericFormInputModel.TypeValue.OptionSet,
                OptionSetValueOptions = options,
                DefaultValue = ((int)parameter.EnumerableType) > 0 ? new OptionSetValue((int)parameter.EnumerableType) : null
            };


            var dicKeyAttr = new GenericFormInputModel()
            {
                DisplayName = nameof(MethodParameterViewmodel.DictionaryKeyType),
                Key = nameof(MethodParameterViewmodel.DictionaryKeyType),
                Description = nameof(MethodParameterViewmodel.DictionaryKeyType),
                IsMandatory = false,
                Type = GenericFormInputModel.TypeValue.OptionSet,
                OptionSetValueOptions = options,
                DefaultValue = ((int)parameter.DictionaryKeyType) > 0 ? new OptionSetValue((int)parameter.DictionaryKeyType) : null
            };


            var dicValueAttr = new GenericFormInputModel()
            {
                DisplayName = nameof(MethodParameterViewmodel.DictionaryValueType),
                Key = nameof(MethodParameterViewmodel.DictionaryValueType),
                Description = nameof(MethodParameterViewmodel.DictionaryValueType),
                IsMandatory = false,
                Type = GenericFormInputModel.TypeValue.OptionSet,
                OptionSetValueOptions = options,
                DefaultValue = ((int)parameter.DictionaryValueType) > 0 ? new OptionSetValue((int)parameter.DictionaryValueType) : null
            };

            model.Attributes.Add(directionAttr);
            model.Attributes.Add(typeAttr);
            model.Attributes.Add(enumAttr);
            model.Attributes.Add(dicKeyAttr);
            model.Attributes.Add(dicValueAttr);

            return model;
        }
    }
}
