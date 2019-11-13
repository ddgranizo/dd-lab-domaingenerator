using System;
using System.Collections.Generic;
using System.Text;

namespace DD.DomainGenerator.Services
{
    public interface IFileService
    {
        bool ExistsFolder(string path);
        bool ExistsFile(string path);
        string GetCurrentPath();
        string GetAbsoluteCurrentPath(string absoluteRelativePath);
        void SaveFile(string path, string content);
        string OpenFile(string path);
        string ConcatDirectoryAndFileOrFolder(string folder, string fileName);
        void CreateFolder(string path);
        bool FolderIsEmpty(string path);
        void CleanFolder(string path);
        void DeleteFolder(string path);
        void CopyFolder(string sourceFolder, string destinationFolder);
    }
}
