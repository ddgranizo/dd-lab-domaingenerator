using AutoMapper;
using DD.DomainGenerator.Models;
using System;
using System.Collections.Generic;
using System.Text;
using UIClient.Models;

namespace UIClient.Profiles
{
    public class AzurePipelineSettingProfile : Profile
    {
        public AzurePipelineSettingProfile()
        {
            CreateMap<AzurePipelineSetting, AzurePipelineSettingModel>();
            CreateMap<AzurePipelineSettingModel, AzurePipelineSetting>();
        }
    }
}
