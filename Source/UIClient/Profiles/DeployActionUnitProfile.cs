using AutoMapper;
using DD.DomainGenerator.DeployActions.Base;
using DD.DomainGenerator.Models;
using System;
using System.Collections.Generic;
using System.Text;
using UIClient.Models;

namespace UIClient.Profiles
{
    public class DeployActionUnitProfile : Profile
    {
        public DeployActionUnitProfile()
        {
            CreateMap<DeployActionUnit, DeployActionUnitModel>();
            CreateMap<DeployActionUnitModel, DeployActionUnit>();
        }
    }
}
