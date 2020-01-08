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

        public static GenericFormModel ToGenericInputModel(this MethodParameterViewModel parameter, string formDescription, OptionSetValue[] options)
        {
            var model = new GenericFormModel(formDescription);
            model.AddAttribute(
                GenericFormInputModel.TypeValue.String,
                nameof(MethodParameterViewModel.Name),
                nameof(MethodParameterViewModel.Name),
                nameof(MethodParameterViewModel.Name),
                true,
                parameter.Name);


            var directionAttr = new GenericFormInputModel()
            {
                DisplayName = nameof(MethodParameterViewModel.Direction),
                Key = nameof(MethodParameterViewModel.Direction),
                Description = nameof(MethodParameterViewModel.Direction),
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
                DisplayName = nameof(MethodParameterViewModel.Type),
                Key = nameof(MethodParameterViewModel.Type),
                Description = nameof(MethodParameterViewModel.Type),
                IsMandatory = true,
                Type = GenericFormInputModel.TypeValue.OptionSet,
                OptionSetValueOptions = options,
                DefaultValue = ((int)parameter.Type) > 0 ? new OptionSetValue((int)parameter.Type) : null
            };

            var enumAttr = new GenericFormInputModel()
            {
                DisplayName = nameof(MethodParameterViewModel.EnumerableType),
                Key = nameof(MethodParameterViewModel.EnumerableType),
                Description = nameof(MethodParameterViewModel.EnumerableType),
                IsMandatory = false,
                Type = GenericFormInputModel.TypeValue.OptionSet,
                OptionSetValueOptions = options,
                DefaultValue = ((int)parameter.EnumerableType) > 0 ? new OptionSetValue((int)parameter.EnumerableType) : null
            };


            var dicKeyAttr = new GenericFormInputModel()
            {
                DisplayName = nameof(MethodParameterViewModel.DictionaryKeyType),
                Key = nameof(MethodParameterViewModel.DictionaryKeyType),
                Description = nameof(MethodParameterViewModel.DictionaryKeyType),
                IsMandatory = false,
                Type = GenericFormInputModel.TypeValue.OptionSet,
                OptionSetValueOptions = options,
                DefaultValue = ((int)parameter.DictionaryKeyType) > 0 ? new OptionSetValue((int)parameter.DictionaryKeyType) : null
            };


            var dicValueAttr = new GenericFormInputModel()
            {
                DisplayName = nameof(MethodParameterViewModel.DictionaryValueType),
                Key = nameof(MethodParameterViewModel.DictionaryValueType),
                Description = nameof(MethodParameterViewModel.DictionaryValueType),
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
