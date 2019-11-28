using AutoMapper;
using DD.DomainGenerator.Models;
using System;
using System.Collections.Generic;
using System.Text;
using UIClient.Models;

namespace UIClient.Profiles
{
    public class UseCaseExecutionContextParameterProfile : Profile
    {
        public UseCaseExecutionContextParameterProfile()
        {
            CreateMap<UseCaseExecutionContextParameter, UseCaseExecutionContextParameterModel>();
            CreateMap<UseCaseExecutionContextParameterModel, UseCaseExecutionContextParameter>();
        }
    }
}
