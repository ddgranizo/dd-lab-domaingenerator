using AutoMapper;
using DD.DomainGenerator.Models;
using System;
using System.Collections.Generic;
using System.Text;
using UIClient.Models;

namespace UIClient.Profiles
{
    public class UseCaseExecutionSentenceProfile : Profile
    {
        public UseCaseExecutionSentenceProfile()
        {
            CreateMap<UseCaseExecutionSentence, UseCaseExecutionSentenceModel>();
            CreateMap<UseCaseExecutionSentenceModel, UseCaseExecutionSentence>();
        }
    }
}
