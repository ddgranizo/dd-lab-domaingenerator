using AutoMapper;
using DD.DomainGenerator.Models;
using System;
using System.Collections.Generic;
using System.Text;
using UIClient.Models;

namespace UIClient.Profiles
{

    public class RepositoryProfile : Profile
    {
        public RepositoryProfile()
        {
            CreateMap<Repository, RepositoryModel>();
            CreateMap<RepositoryModel, Repository>();
        }
    }
}
