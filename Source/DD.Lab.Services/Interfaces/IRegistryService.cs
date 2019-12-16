using System;
using System.Collections.Generic;
using System.Text;

namespace DD.Lab.Services.System.Interfaces
{
    public interface IRegistryService
    {
        string GetValue(string key);
        void SetValue(string key, string value);
    }
}
