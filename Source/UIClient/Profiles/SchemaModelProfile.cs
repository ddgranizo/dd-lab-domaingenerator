using AutoMapper;
using DD.DomainGenerator.Models;
using System;
using System.Collections.Generic;
using System.Text;
using UIClient.Models;

namespace UIClient.Profiles
{
    public class SchemaModelProfile : Profile
    {
        public SchemaModelProfile()
        {
            CreateMap<Schema, SchemaModel>();
            CreateMap<SchemaModel, SchemaModel>();
        }
    }
}
