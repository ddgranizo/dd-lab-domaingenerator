using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DD.DomainGenerator.Services.Implementations
{
    public class FileService : IFileService
    {
        public void CleanFolder(string path)
        {
            DirectoryInfo di = new DirectoryInfo(path);
            foreach (FileInfo file in di.GetFiles())
            {
                file.Delete();
            }
            foreach (DirectoryInfo dir in di.GetDirectories())
            {
                dir.Delete(true);
            }
        }

        public string ConcatDirectoryAndFileOrFolder(string folder, string fileName)
        {
            var folderCopy = folder;
            if (folderCopy.Last() != '\\' && folderCopy.Last() != '/')
            {
                folderCopy = string.Format("{0}\\", folderCopy);
            }
            return string.Format("{0}{1}", folderCopy, fileName);
        }

        public void CopyFolder(string sourceFolder, string destinationFolder)
        {
            DirectoryInfo diSource = new DirectoryInfo(sourceFolder);
            DirectoryInfo diTarget = new DirectoryInfo(destinationFolder);
            CopyAll(diSource, diTarget);
        }

        public static void CopyAll(DirectoryInfo source, DirectoryInfo target)
        {
            Directory.CreateDirectory(target.FullName);
            foreach (FileInfo fi in source.GetFiles())
            {
                fi.CopyTo(Path.Combine(target.FullName, fi.Name), true);
            }
            foreach (DirectoryInfo diSourceSubDir in source.GetDirectories())
            {
                DirectoryInfo nextTargetSubDir =
                    target.CreateSubdirectory(diSourceSubDir.Name);
                CopyAll(diSourceSubDir, nextTargetSubDir);
            }
        }
       
        public void CreateFolder(string path)
        {
            Directory.CreateDirectory(path);
        }

        public void DeleteFolder(string path)
        {
            Directory.Delete(path, true);
        }

        public bool ExistsFile(string path)
        {
            return File.Exists(path);
        }

        public bool ExistsFolder(string path)
        {
            return Directory.Exists(path);
        }

        public bool FolderIsEmpty(string path)
        {
            DirectoryInfo di = new DirectoryInfo(path);
            return di.GetFiles().Length == 0 && di.GetDirectories().Length == 0;
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
