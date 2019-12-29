using DD.Lab.Wpf.Models.Inputs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DomainGeneratorUI.Models
{
    public class Domain
    {

        public const string LogicalName = "Domain";

        public Guid Id { get; set; }

        [Required]
        [Description("Name of the record")]
        public string Name { get; set; }

        [Required]
        [Description("Project")]
        public EntityReferenceValue ProjectId { get; set; }
    }
}
