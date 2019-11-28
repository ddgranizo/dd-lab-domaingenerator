using AutoMapper;
using DD.DomainGenerator.Models;
using System;
using System.Collections.Generic;
using System.Text;
using UIClient.Models;

namespace UIClient.Profiles
{
    public class DataParameterValueProfile : Profile
    {
        public DataParameterValueProfile()
        {
            CreateMap<UseCaseExecutionContextParameter, DataParameterValueModel>();
            CreateMap<DataParameterValueModel, UseCaseExecutionContextParameter>();
        }
    }
}
