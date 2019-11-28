﻿using AutoMapper;
using DD.DomainGenerator.Models;
using System;
using System.Collections.Generic;
using System.Text;
using UIClient.Models;

namespace UIClient.Profiles
{
    public class UseCaseLinkInputExecutionParameterProfile : Profile
    {
        public UseCaseLinkInputExecutionParameterProfile()
        {
            CreateMap<UseCaseLinkInputExecutionParameter, UseCaseLinkInputExecutionParameterModel>();
            CreateMap<UseCaseLinkInputExecutionParameterModel, UseCaseLinkInputExecutionParameter>();
        }
    }
}
