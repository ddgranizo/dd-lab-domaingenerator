using AutoMapper;
using DD.DomainGenerator.Models;
using System;
using System.Collections.Generic;
using System.Text;
using UIClient.Models;

namespace UIClient.Profiles
{

    public class MicroServiceProfile : Profile
    {
        public MicroServiceProfile()
        {
            CreateMap<MicroService, MicroServiceModel>();
            CreateMap<MicroServiceModel, MicroService>();
        }
    }
}
