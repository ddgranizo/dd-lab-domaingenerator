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
    public class RepositoryMethod
    {
        public const string LogicalName = "RepositoryMethod";

        public enum RepositoryMethodType
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
        [Description("Repository")]
        public EntityReferenceValue RepositoryId { get; set; }

        [Required]
        [Description("Type of method")]
        [EnumAtrribute(EnumName = nameof(RepositoryMethodType))]
        public OptionSetValue Type { get; set; }

        [Description("Repository method definition")]
        [CustomContentAttribute(ModuleName = CustomModules.RepositoryMethodContentModule)]
        public string Content { get; set; }
    }
}
