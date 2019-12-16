using DD.Lab.GenericUI.Core.Models;
using DD.Lab.GenericUI.Core.Models.Data;
using DD.Lab.Services.System.Implementations.Base;
using DD.Lab.Services.System.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace UIClientV2.Services
{

    public class StoredGenericValuesService : BaseStoredJsonData<DataSetModel>
    {
        public const string DataFolder = Definitions.StoreDataPathName;
        public const string DataFileBase = Definitions.StoreGenericDataFileName;

        public StoredGenericValuesService(IJsonParserService jsonParserService, IFileService fileService)
            : base(jsonParserService, fileService, DataFolder, DataFileBase)
        {
        }


        public void SetContextFile(string entityName)
        {
            FileNameFormat = string.Format(DataFileBase, entityName);
            CheckIfExistsData();
        }
    }
}
