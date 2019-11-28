using AutoMapper;
using DD.DomainGenerator.Models;
using System;
using System.Collections.Generic;
using System.Text;
using UIClient.Models;

namespace UIClient.Profiles
{
    public class UseCaseExecutionProfile : Profile
    {
        public UseCaseExecutionProfile()
        {
            CreateMap<UseCaseExecution, UseCaseExecutionModel>();
            CreateMap<UseCaseExecutionModel, UseCaseExecution>();
        }
    }
}
