using AutoMapper;
using DD.DomainGenerator.Models;
using DD.DomainGenerator.Sentences.Base;
using System;
using System.Collections.Generic;
using System.Text;
using UIClient.Models;
using UIClient.Models.Sentences.Base;

namespace UIClient.Profiles
{
    public class ExecutionSentenceBaseProfile : Profile
    {
        public ExecutionSentenceBaseProfile()
        {
            CreateMap<ExecutionSentenceBase, ExecutionSentenceBaseModel>();
            CreateMap<ExecutionSentenceBaseModel, ExecutionSentenceBase>();
        }
    }
}
