using DD.Lab.Wpf.Drm.Attributes;
using DD.Lab.Wpf.Models.Inputs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DomainGeneratorUI.Models
{
    public class Property
    {

        public enum PropertyType
        {
            PrimaryKey = 1,
            State = 2,
            Status = 3,
            ForeingKey = 4,
            Boolean = 10,
            Integer = 11,
            Decimal = 12,
            Float = 13,
            Time = 30,
            DateTime = 31,
            String = 40,
            LongString = 41,
            Owner = 50,
            Password = 99,
        }


        public Guid Id { get; set; }

        [Required]
        [Description("Name of the record")]
        public string Name { get; set; }

        [Required]
        [Description("Type of setting")]
        [EnumAtrribute(EnumName = nameof(PropertyType))]
        public OptionSetValue Type { get; set; }

        [Description("Foreing Schema if Type = ForeignKey")]
        public EntityReferenceValue ForeingSchemaId { get; set; }


        [Description("Is PrimaryKey")]
        public bool IsPrimaryKey { get; set; }

        [Description("Length")]
        public int Length { get; set; }

        [Description("Is Nullable")]
        public bool IsNullable { get; set; }

        [Description("Is Unique")]
        public bool IsUnique { get; set; }

        [Description("Is AutoIncremental")]
        public bool IsAutoIncremental { get; set; }

        [Required]
        [Description("Schema")]
        public EntityReferenceValue SchemaId { get; set; }
    }
}
