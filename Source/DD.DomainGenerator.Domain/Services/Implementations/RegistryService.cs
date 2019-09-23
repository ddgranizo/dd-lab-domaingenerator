using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace DD.DomainGenerator.Services.Implementations
{
    public class RegistryService : IRegistryService
    {

        private const string RegistryPathFormat = @"SOFTWARE\{0}";
        private readonly RegistryKey _registryKey;

        public RegistryService()
        {
            var assemblyName = Assembly.GetExecutingAssembly().GetName().Name;
            _registryKey = Registry.CurrentUser.CreateSubKey(string.Format(RegistryPathFormat, assemblyName));
        }


        public string GetValue(string key)
        {
            return _registryKey.GetValue(key)?.ToString();
        }

        public void SetValue(string key, string value)
        {
            _registryKey.SetValue(key, value.ToString());
        }
    }
}
