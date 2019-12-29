using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DomainGeneratorUI.Models
{
    public class Project
    {
        public const string LogicalName = "Project";

        public Guid Id { get; set; }

        [Required]
        [Description("Name of the record")]
        public string Name { get; set; }

        [Required]
        [Description("Path for save the project files")]
        public string Path { get; set; }
    }
}
