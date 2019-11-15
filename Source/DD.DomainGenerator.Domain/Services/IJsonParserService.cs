using System;
using System.Collections.Generic;
using System.Text;

namespace DD.DomainGenerator.Services
{
    public interface IJsonParserService
    {
        T Objectify<T>(string json);
        string Stringfy<T>(T instance);
    }
}
