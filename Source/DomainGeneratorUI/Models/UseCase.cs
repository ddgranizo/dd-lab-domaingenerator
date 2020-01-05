using DD.Lab.Wpf.Drm.Attributes;
using DD.Lab.Wpf.Models.Inputs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;
using static DomainGeneratorUI.Definitions;

namespace DomainGeneratorUI.Models
{
    public class UseCase
    {
        public const string LogicalName = "UseCase";

        public enum UseCaseType
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

        public Guid Id { get; set; }

        [Required]
        [Description("Name of the record")]
        public string Name { get; set; }

        [Required]
        [Description("Type of use case")]
        [EnumAtrribute(EnumName = nameof(UseCaseType))]
        public OptionSetValue Type { get; set; }

        [Required]
        [Description("Display name")]
        public string DisplayName { get; set; }

        [Required]
        [Description("Description")]
        public string Description { get; set; }

        [Description("Use case definition")]
        [CustomContentAttribute(ModuleName = CustomModules.UseCaseContentModule)]
        public string Content { get; set; }

        [Required]
        [Description("Schema")]
        public EntityReferenceValue SchemaId { get; set; }
    }
}
