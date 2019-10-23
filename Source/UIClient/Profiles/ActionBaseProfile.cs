using AutoMapper;
using DD.DomainGenerator.Actions.Base;
using DD.DomainGenerator.Models;
using System;
using System.Collections.Generic;
using System.Text;
using UIClient.Models;

namespace UIClient.Profiles
{
    public class ActionBaseProfile : Profile
    {
        public ActionBaseProfile()
        {
            CreateMap<ActionBase, ActionBaseModel>();
            CreateMap<ActionBaseModel, ActionBase>();
        }
    }
}
