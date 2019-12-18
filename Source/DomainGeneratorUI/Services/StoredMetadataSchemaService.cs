using DD.Lab.Services.System.Implementations.Base;
using DD.Lab.Services.System.Interfaces;
using DD.Lab.Wpf.Drm.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DomainGeneratorUI.Services
{

    public class StoredMetadataSchemaService : BaseStoredJsonData<MetadataModel>
    {
        public const string DataFolder = Definitions.StoreDataPathName;
        public const string DataFile = Definitions.StoreDataMetadataFileName;

        public StoredMetadataSchemaService(IJsonParserService jsonParserService, IFileService fileService)
            : base(jsonParserService, fileService, DataFolder, DataFile)
        {

        }

    }
}
