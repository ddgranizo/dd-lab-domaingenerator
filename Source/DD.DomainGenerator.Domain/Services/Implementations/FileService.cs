using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace DD.DomainGenerator.Services.Implementations
{
    public class FileService : IFileService
    {
        public string ConcatDirectoryAndFileOrFolder(string folder, string fileName)
        {
            var folderCopy = folder;
            if (folderCopy.Last() != '\\' && folderCopy.Last() != '/')
            {
                folderCopy = string.Format("{0}\\", folderCopy);
            }
            return string.Format("{0}{1}", folderCopy, fileName);
        }

        public void CreateFolder(string path)
        {
            Directory.CreateDirectory(path);
        }

        public bool ExistsFolder(string path)
        {
            return Directory.Exists(path);
        }

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
