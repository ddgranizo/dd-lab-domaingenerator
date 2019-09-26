using AutoMapper;
using DD.DomainGenerator.Models;
using System;
using System.Collections.Generic;
using System.Text;
using UIClient.Models;

namespace UIClient.Profiles
{
    public class GithubSettingProfile : Profile
    {
        public GithubSettingProfile()
        {
            CreateMap<GithubSetting, GithubSettingModel>();
            CreateMap<GithubSettingModel, GithubSetting>();
        }
    }
}
