using AutoMapper;
using System;
using System.Collections.Generic;
using System.DirectoryServices.ActiveDirectory;
using System.Text;
using UIClient.Models;

namespace UIClient.Profiles
{
    public class DomainProfile : Profile
    {
        public DomainProfile()
        {
            CreateMap<Domain, DomainModel>();
            CreateMap<DomainModel, Domain>();
        }
    }
}
