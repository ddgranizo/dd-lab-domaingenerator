using DD.Lab.Services.System.Interfaces;
using DD.Lab.Services.System.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace DD.Lab.Services.System.Implementations.Base
{
    public class BaseStoredXmlData<T> : IStoredDataService<T> where T: class, new()
    {

        public BaseStoredXmlData(string folderName, string fileNameFormat)
        {
            if (string.IsNullOrEmpty(folderName))
            {
                throw new ArgumentException("message", nameof(folderName));
            }

            if (string.IsNullOrEmpty(fileNameFormat))
            {
                throw new ArgumentException("message", nameof(fileNameFormat));
            }

            FolderName = folderName;
            FileNameFormat = fileNameFormat;
            CheckIfExistsFolder();
            CheckIfExistsData();
        }

        public string FolderName { get; }
        public string FileNameFormat { get; }

     

        public T GetStoredData() 
        {

            return StoredDataManager.GetStoredData<T>(GetFilePath());
        }

        public void SaveStoredData(T data)
        {
            StoredDataManager.SaveStoredData<T>(GetFilePath(), data);
        }



        public  string GetFilePath(bool backup = false)
        {
            string basePath = GetFolderPath();
            return string.Format("{0}\\{1}", basePath,
                string.Format(FileNameFormat, backup ? $"_Backup_{DateTime.Now.ToString("yyyyMMddHHmmss")}" : string.Empty));
        }

        public string GetFolderPath()
        {
            var basePath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            return string.Format("{0}\\{1}", basePath, FolderName);
        }

        private void CheckIfExistsFolder()
        {
            if (!Directory.Exists(GetFolderPath()))
            {
                Directory.CreateDirectory(GetFolderPath());
            }
        }


        private void CheckIfExistsData()
        {
            if (!File.Exists(GetFilePath()))
            {
                SaveStoredData(new T());
            }
        }
    }
}
