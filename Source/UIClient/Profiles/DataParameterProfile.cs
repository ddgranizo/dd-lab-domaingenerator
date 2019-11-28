using AutoMapper;
using DD.DomainGenerator.Models;
using System;
using System.Collections.Generic;
using System.Text;
using UIClient.Models;

namespace UIClient.Profiles
{
    public class DataParameterProfile : Profile
    {
        public DataParameterProfile()
        {
            CreateMap<DataParameter, DataParameterModel>();
            CreateMap<DataParameterModel, DataParameter>();
        }
    }
}
