using System;
using System.Collections.Generic;
using System.Text;

namespace UIClient
{
    public class Definitions
    {
        public const string DefaultStoredDataFolder = "DomainGenerator";
        public const string RecentProjectStoredDataFile = "RecentProjects{0}.xml";

        public struct UseCaseEditorDefinitions
        {
            public struct UseCaseModelAttibutes
            {
                public const string Type = "type";
                public const string Name = "name";
                public const string EnumerableType = "enumerabletype";
                public const string DictionaryKeyType = "dictionarykeytype";
                public const string DictionaryValueType = "dictionaryvaluetype";
            }
        }
    }
}
