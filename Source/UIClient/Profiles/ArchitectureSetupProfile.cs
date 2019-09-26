using AutoMapper;
using DD.DomainGenerator.Models;
using System;
using System.Collections.Generic;
using System.Text;
using UIClient.Models;

namespace UIClient.Profiles
{
    public class ArchitectureSetupProfile : Profile
    {
        public ArchitectureSetupProfile()
        {
            CreateMap<ArchitectureSetup, ArchitectureSetupModel>();
            CreateMap<ArchitectureSetupModel, ArchitectureSetup>();
        }
    }
}
