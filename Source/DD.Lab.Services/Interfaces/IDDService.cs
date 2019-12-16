using System;
using System.Collections.Generic;
using System.Text;

namespace DD.Lab.Services.System.Interfaces
{
    public interface IDDService
    {
        void Initialize(string ddPath);
        void Template(string localPath, string templatePath, string @namespace, string appName);
    }
}
