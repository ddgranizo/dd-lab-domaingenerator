using AutoMapper;
using DD.DomainGenerator.Models;
using System;
using System.Collections.Generic;
using System.Text;
using UIClient.Models;

namespace UIClient.Profiles
{
    public class ActionExecutionProfile : Profile
    {
        public ActionExecutionProfile()
        {
            CreateMap<ActionExecution, ActionExecutionModel>();
            CreateMap<ActionExecutionModel, ActionExecution>();
        }
    }
}
