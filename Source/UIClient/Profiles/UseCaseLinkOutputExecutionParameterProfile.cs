using AutoMapper;
using DD.DomainGenerator.Models;
using System;
using System.Collections.Generic;
using System.Text;
using UIClient.Models;

namespace UIClient.Profiles
{
    public class UseCaseLinkOutputExecutionParameterProfile : Profile
    {
        public UseCaseLinkOutputExecutionParameterProfile()
        {
            CreateMap<UseCaseLinkOutputExecutionParameter, UseCaseLinkOutputExecutionParameterModel>();
            CreateMap<UseCaseLinkOutputExecutionParameterModel, UseCaseLinkOutputExecutionParameter>();
        }
    }
}
