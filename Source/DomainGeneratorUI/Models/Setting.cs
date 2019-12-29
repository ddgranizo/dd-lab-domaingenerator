using DD.Lab.Wpf.Drm.Attributes;
using DD.Lab.Wpf.Models.Inputs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DomainGeneratorUI.Models
{
    public class Setting
    {
        public const string LogicalName = "Setting";

        public Guid Id { get; set; }

        [Required]
        [Description("Path for the setting")]
        public string Path { get; set; }

        [Required]
        [Description("Type of setting")]
        [OptionSetAtrribute(DisplayName = "GitExePath", Value = 1)]
        [OptionSetAtrribute(DisplayName = "DotNetExePath", Value = 2)]
        [OptionSetAtrribute(DisplayName = "DDCliExePath", Value = 3)]
        [OptionSetAtrribute(DisplayName = "DDCliDomainProjectTemplatePath", Value = 4)]
        public OptionSetValue Type { get; set; }

        [Required]
        [Description("Project")]
        public EntityReferenceValue ProjectId { get; set; }
    }
}
