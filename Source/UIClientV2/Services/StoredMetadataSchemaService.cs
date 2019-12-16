using DD.Lab.GenericUI.Core.Models;
using DD.Lab.Services.System.Implementations.Base;
using DD.Lab.Services.System.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace UIClientV2.Services
{

    public class StoredMetadataSchemaService : BaseStoredJsonData<MetadataModel>
    {
        public const string DataFolder = Definitions.StoreDataPathName;
        public const string DataFile = Definitions.StoreDataMetadataFileName;

        public StoredMetadataSchemaService(IJsonParserService jsonParserService, IFileService fileService )
            : base(jsonParserService, fileService, DataFolder, DataFile)
        {

        }

    }
}
