using AutoMapper;
using DD.DomainGenerator.Models;
using System;
using System.Collections.Generic;
using System.Text;
using UIClient.Models;

namespace UIClient.Profiles
{
    public class SchemaViewProfile : Profile
    {
        public SchemaViewProfile()
        {
            CreateMap<RepositoryMethod, RepositoryMethodModel>();
            CreateMap<RepositoryMethodModel, RepositoryMethod>();
        }
    }
}
