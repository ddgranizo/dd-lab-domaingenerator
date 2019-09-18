using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace DD.DomainGenerator.Services.Implementations
{
    public class FileService : IFileService
    {
        public string GetAbsoluteCurrentPath(string absoluteRelativePath)
        {
            if (Path.IsPathRooted(absoluteRelativePath))
            {
                return absoluteRelativePath;
            }
            var currentPath = GetCurrentPath();
            return string.Format("{0}\\{1}", currentPath, absoluteRelativePath);
        }

        public string GetCurrentPath()
        {
            return Directory.GetCurrentDirectory();
        }

        public string OpenFile(string path)
        {
            return File.ReadAllText(path);
        }

        public void SaveFile(string path, string content)
        {
            File.WriteAllText(path, content);
        }
    }
}
