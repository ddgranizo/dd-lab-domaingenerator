using DD.Lab.Wpf.Drm.Attributes;
using DD.Lab.Wpf.Models.Inputs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DomainGeneratorUI.Models
{
    public class Model
    {
        public Guid Id { get; set; }

        [Required]
        [Description("Name of the record")]
        public string Name { get; set; }

        [Description("Is MainModel")]
        public bool IsMainModel { get; set; }

        [Description("All Properties")]
        public bool AllProperties { get; set; }
      
        [Required]
        [Description("Schema")]
        public EntityReferenceValue SchemaId { get; set; }
    }
}
