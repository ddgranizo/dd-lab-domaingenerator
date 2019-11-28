using AutoMapper;
using DD.DomainGenerator.Models;
using System;
using System.Collections.Generic;
using System.Text;
using UIClient.Models;

namespace UIClient.Profiles
{
    public class UseCaseExecutionContextProfile : Profile
    {
        public UseCaseExecutionContextProfile()
        {
            CreateMap<UseCaseExecutionContext, UseCaseExecutionContextModel>();
            CreateMap<UseCaseExecutionContextModel, UseCaseExecutionContext>();
        }
    }
}
