using System;
using System.Collections.Generic;
using System.Text;

namespace DD.DomainGenerator.Services
{
    public interface IDDService
    {
        void Initialize(string ddPath);
        void Template(string localPath, string templatePath, string @namespace, string appName);
    }
}
