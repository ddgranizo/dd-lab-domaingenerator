using System;
using System.Collections.Generic;
using System.Text;

namespace DD.DomainGenerator.Services
{
    public interface IFileService
    {

        string GetCurrentPath();
        string GetAbsoluteCurrentPath(string absoluteRelativePath);

        void SaveFile(string path, string content);

        string OpenFile(string path);

    }
}
