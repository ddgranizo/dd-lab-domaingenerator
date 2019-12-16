using System;
using System.Collections.Generic;
using System.Text;

namespace DD.Lab.GenericUI.Core.Services
{
    public interface IJsonParserService
    {
        T Objectify<T>(string json);
        string Stringfy<T>(T instance);
    }
}
