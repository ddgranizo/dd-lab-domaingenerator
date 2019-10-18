using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UIClient.Models.Stored;
using UIClient.Services.Base;
using UIClient.Utilities;

namespace UIClient.Services.Implementations
{
    public class StoredRecentProjectsService : BaseStoredData<RecentProjects>
    {
        public const string CliAppDataFolder = Definitions.DefaultStoredDataFolder;
        public const string CliAppDataFile = Definitions.RecentProjectStoredDataFile ;

        public StoredRecentProjectsService()
            :base(CliAppDataFolder, CliAppDataFile)
        {
           
        }

    }
}
