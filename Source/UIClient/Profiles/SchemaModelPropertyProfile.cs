using AutoMapper;
using DD.DomainGenerator.Models;
using System;
using System.Collections.Generic;
using System.Text;
using UIClient.Models;

namespace UIClient.Profiles
{
    public class SchemaModelPropertyProfile : Profile
    {
        public SchemaModelPropertyProfile()
        {
            CreateMap<SchemaModelProperty, SchemaModelPropertyModel>();
            CreateMap<SchemaModelPropertyModel, SchemaModelProperty>();
        }
    }
}
