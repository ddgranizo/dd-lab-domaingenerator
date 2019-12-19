using DD.Lab.Wpf.Models.Inputs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DomainGeneratorUI.Models
{
    public class Repository
    {
        public Guid Id { get; set; }

        [Required]
        [Description("Name of the record")]
        public string Name { get; set; }

        [Description("Is Main Repository")]
        public bool IsMainRepository { get; set; }

        [Required]
        [Description("Schema")]
        public EntityReferenceValue SchemaId { get; set; }
    }
}
