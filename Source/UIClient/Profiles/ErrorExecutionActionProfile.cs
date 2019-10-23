using AutoMapper;
using DD.DomainGenerator.Events;
using System;
using System.Collections.Generic;
using System.Text;
using UIClient.Models;

namespace UIClient.Profiles
{
  
    public class ErrorExecutionActionProfile : Profile
    {
        public ErrorExecutionActionProfile()
        {
            CreateMap<ErrorExecutionActionEventArgs, ErrorExecutionActionModel>();
            CreateMap<ErrorExecutionActionModel, ErrorExecutionActionEventArgs>();
        }
    }
}
