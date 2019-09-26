using AutoMapper;
using DD.DomainGenerator.Models;
using System;
using System.Collections.Generic;
using System.Text;
using UIClient.Models;

namespace UIClient.Profiles
{
    public class UseCaseProfile : Profile
    {
        public UseCaseProfile()
        {
            CreateMap<UseCase, UseCaseModel>();
            CreateMap<UseCaseModel, UseCase>();
        }
    }
}
