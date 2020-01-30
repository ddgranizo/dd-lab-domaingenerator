using System;
using System.Collections.Generic;
using System.Text;

namespace DD.Lab.Wpf.Drm.Services
{
    public interface IJsonParserService
    {
        T Objectify<T>(string json);
        string Stringfy<T>(T instance);
        string StringfyWithTypes<T>(T instance);
    }
}
