using System;
using System.Collections.Generic;
using System.Text;

namespace DD.DomainGenerator.Services
{
    public interface IRegistryService
    {
        string GetValue(string key);
        void SetValue(string key, string value);
    }
}
