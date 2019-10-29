using AutoMapper;
using DD.DomainGenerator.Models;
using System;
using System.Collections.Generic;
using System.Text;
using UIClient.Models;

namespace UIClient.Profiles
{
    
    public class DomainInMicroServiceProfile : Profile
    {
        public DomainInMicroServiceProfile()
        {
            CreateMap<DomainInMicroService, DomainInMicroServiceModel>();
            CreateMap<DomainInMicroServiceModel, DomainInMicroService>();
        }
    }
}
