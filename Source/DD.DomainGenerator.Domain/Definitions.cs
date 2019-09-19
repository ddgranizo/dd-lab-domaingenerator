using System;
using System.Collections.Generic;
using System.Text;

namespace DD.DomainGenerator
{
    public class Definitions
    {

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
