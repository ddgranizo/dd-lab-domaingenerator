using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace DomainGeneratorUI.Extensions
{
    public static class MapperConfigurationExtensions
    {
        public static void CreateReversiveMap<U,V>(this IMapperConfigurationExpression mapper)
        {
            mapper.CreateMap<U, V>();
            mapper.CreateMap<V, U>();
        }
    }
}
