using System;
using System.Collections.Generic;
using System.Text;

namespace DD.Lab.Services.System.Interfaces
{
    public interface IJsonParserService
    {
        T Objectify<T>(string json);
        string Stringfy<T>(T instance);
    }
}
