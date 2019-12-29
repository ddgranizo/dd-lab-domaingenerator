using DD.Lab.Wpf.Models.Inputs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DomainGeneratorUI.Models
{
    public class Schema
    {
        public const string LogicalName = "Schema";

        public Guid Id { get; set; }

        [Required]
        [Description("Name of the record")]
        public string Name { get; set; }


        [Description("Has State")]
        public bool HasState { get; set; }

        [Description("Has Owner")]
        public bool HasOwner { get; set; }

        [Description("Has User Relationship")]
        public bool HasUserRelationship { get; set; }

        [Required]
        [Description("Domain")]
        public EntityReferenceValue DomainId { get; set; }
    }
}
