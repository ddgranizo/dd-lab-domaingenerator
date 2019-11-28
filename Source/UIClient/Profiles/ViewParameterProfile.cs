using AutoMapper;
using DD.DomainGenerator.Models;
using System;
using System.Collections.Generic;
using System.Text;
using UIClient.Models;

namespace UIClient.Profiles
{
   

    public class ViewParameterProfile : Profile
    {
        public ViewParameterProfile()
        {
            CreateMap<RepositoryMethodParameter, RepositoryMethodParameterModel>();
            CreateMap<RepositoryMethodParameterModel, RepositoryMethodParameter>();
        }
    }
}
