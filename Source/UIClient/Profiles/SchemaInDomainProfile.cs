using AutoMapper;
using DD.DomainGenerator.Models;
using System;
using System.Collections.Generic;
using System.Text;
using UIClient.Models;

namespace UIClient.Profiles
{
    public class SchemaInDomainProfile : Profile
    {
        public SchemaInDomainProfile()
        {
            CreateMap<SchemaInDomain, SchemaInDomainModel>();
            CreateMap<SchemaInDomainModel, SchemaInDomain>();
        }
    }
}
