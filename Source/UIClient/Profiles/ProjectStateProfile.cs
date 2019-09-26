using AutoMapper;
using DD.DomainGenerator;
using System;
using System.Collections.Generic;
using System.Text;
using UIClient.Models;

namespace UIClient.Profiles
{
    public class ProjectStateProfile : Profile
    {
        public ProjectStateProfile()
        {
            CreateMap<ProjectState, ProjectStateModel>();
            CreateMap<ProjectStateModel, ProjectState>();
        }
    }
}
