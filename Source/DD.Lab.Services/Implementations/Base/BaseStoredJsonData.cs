using DD.Lab.Services.System.Interfaces;
using DD.Lab.Services.System.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace DD.Lab.Services.System.Implementations.Base
{
    public class BaseStoredJsonData<T> : IStoredDataService<T> where T: class, new()
    {
        public BaseStoredJsonData(IJsonParserService jsonParserService,  IFileService fileService, string folderName, string fileNameFormat)
        {
            if (string.IsNullOrEmpty(folderName))
            {
                throw new ArgumentException("message", nameof(folderName));
            }

            if (string.IsNullOrEmpty(fileNameFormat))
            {
                throw new ArgumentException("message", nameof(fileNameFormat));
            }

            JsonParserService = jsonParserService ?? throw new ArgumentNullException(nameof(jsonParserService));
            FileService = fileService ?? throw new ArgumentNullException(nameof(fileService));
            FolderName = folderName;
            FileNameFormat = fileNameFormat;
            CheckIfExistsFolder();
            CheckIfExistsData();
        }

        public IJsonParserService JsonParserService { get; }
        public IFileService FileService { get; }
        public string FolderName { get; }
        public string FileNameFormat { get; set; }

        public T GetStoredData() 
        {
            var json = FileService.OpenFile(GetFilePath());
            return JsonParserService.Objectify<T>(json);
        }

        public void SaveStoredData(T data)
        {
            var json = JsonParserService.Stringfy<T>(data);
            FileService.SaveFile(GetFilePath(), json);
        }


        public string GetFilePath(bool backup = false)
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


        public void CheckIfExistsData()
        {
            if (!File.Exists(GetFilePath()))
            {
                SaveStoredData(new T());
            }
        }


    }
}
