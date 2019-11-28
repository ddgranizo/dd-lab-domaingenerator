using AutoMapper;
using DD.DomainGenerator.Models;
using System;
using System.Collections.Generic;
using System.Text;
using UIClient.Models;

namespace UIClient.Profiles
{
    public class UseCaseLinkExecutionParameterProfile : Profile
    {
        public UseCaseLinkExecutionParameterProfile()
        {
            CreateMap<UseCaseLinkExecutionParameter, UseCaseLinkExecutionParameterModel>();
            CreateMap<UseCaseLinkExecutionParameterModel, UseCaseLinkExecutionParameter>();
        }
    }
}
