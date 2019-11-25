using AutoMapper;
using DD.DomainGenerator.Models;
using System;
using System.Collections.Generic;
using System.Text;
using UIClient.Models;

namespace UIClient.Profiles
{
    public class UseCaseParameterProfile : Profile
    {
        public UseCaseParameterProfile()
        {
            CreateMap<UseCaseParameter, UseCaseParameterModel>();
            CreateMap<UseCaseParameterModel, UseCaseParameter>();
        }
    }
}
