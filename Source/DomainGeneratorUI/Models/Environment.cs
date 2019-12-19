using DD.Lab.Wpf.Models.Inputs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DomainGeneratorUI.Models
{
    public class Environment
    {
        public Guid Id { get; set; }

        [Required]
        [Description("Name of the record")]
        public string Name { get; set; }

        [Required]
        [Description("ShortName")]
        public string ShortName { get; set; }

        [Required]
        [Description("Order")]
        public int Order { get; set; }

        [Required]
        [Description("Project")]
        public EntityReferenceValue ProjectId { get; set; }
    }
}
