using AutoMapper;
using DD.DomainGenerator.Models;
using System.Collections.Generic;
using System.Text;
using UIClient.Models;

namespace UIClient.Profiles
{
    public class EnvironmentProfile : Profile
    {
        public EnvironmentProfile()
        {
            CreateMap<Environment, EnvironmentModel>();
            CreateMap<EnvironmentModel, Environment>();
        }
    }
}
