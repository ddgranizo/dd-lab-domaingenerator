﻿using AutoMapper;
using DD.DomainGenerator.Models;
using System;
using System.Collections.Generic;
using System.Text;
using UIClient.Models;

namespace UIClient.Profiles
{
    public class SchemaModelPropertyProfile : Profile
    {
        public SchemaModelPropertyProfile()
        {
            CreateMap<SchemaProperty, SchemaPropertyModel>();
            CreateMap<SchemaPropertyModel, SchemaProperty>();
        }
    }
}
