using System;
using System.Collections.Generic;
using System.Text;

namespace DD.DomainGenerator
{
    public class Definitions
    {
        public struct ActionsParametersDefinitions
        {
            public struct AddMicroService
            {
                public const string Name = "name";
            }
        }

        public struct DefaultEnvironmentNames
        {
            public struct Production
            {
                public const string Name = "Production";
                public const string Shortname = "pro";
            }
            public struct Preproduction
            {
                public const string Name = "Preproduction";
                public const string Shortname = "pre";
            }
            public struct Integration
            {
                public const string Name = "Integration";
                public const string Shortname = "int";
            }
            public struct Development
            {
                public const string Name = "Development";
                public const string Shortname = "dev";
            }
        }

        public static string[] AvailableTrueStrings = new string[] {
                "true",
                "yes",
                "1",
                "si" };

        public struct DefaultBasicDomainNames
        {
            public const string User = "User";
        }

        public struct DefaultAttributesSchemaNames
        {
            public const string Id = "Id";
            public const string State = "State";
            public const string Status = "Status";
            public const string CreatedBy = "CreatedBy";
            public const string ModifiedBy = "ModifiedBy";
            public const string CreatedOn = "CreatedOn";
            public const string ModifiedOn = "ModifiedOn";
            public const string Owner = "Owner";
        }

    }
}
