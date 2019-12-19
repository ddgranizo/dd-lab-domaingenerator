using DD.Lab.Wpf.Drm.Attributes;
using DD.Lab.Wpf.Models.Inputs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DomainGeneratorUI.Models
{
    public class MethodParameter
    {
        public enum ParameterDirection
        {
            Input = 1,
            Output = 2
        }


        public enum ParameterInputType
        {
            Boolean = 1,
            Integer = 2,
            Decimal = 3,
            Double = 4,
            Guid = 9,
            String = 10,
            Datetime = 20,

            Enumerable = 80,
            Dictionary = 90,

            Entity = 99,
        }

        public Guid Id { get; set; }

        [Required]
        [Description("Name of the record")]
        public string Name { get; set; }

        [Required]
        [Description("Parameter direction")]
        [EnumAtrribute(EnumName = nameof(ParameterDirection))]
        public OptionSetValue Direction { get; set; }

        [Required]
        [Description("Parameter type")]
        [EnumAtrribute(EnumName = nameof(ParameterInputType))]
        public OptionSetValue Type { get; set; }

        [Required]
        [Description("Enumerable type")]
        [EnumAtrribute(EnumName = nameof(ParameterInputType))]
        public OptionSetValue EnumerableType { get; set; }

        [Required]
        [Description("Dictionary Key type")]
        [EnumAtrribute(EnumName = nameof(ParameterInputType))]
        public OptionSetValue DictionaryKeyType { get; set; }

        [Required]
        [Description("Dictionary Value type")]
        [EnumAtrribute(EnumName = nameof(ParameterInputType))]
        public OptionSetValue DictionaryValueType { get; set; }

        [Required]
        [Description("Repository method")]
        public EntityReferenceValue RepositoryMethodId { get; set; }

    }
}
