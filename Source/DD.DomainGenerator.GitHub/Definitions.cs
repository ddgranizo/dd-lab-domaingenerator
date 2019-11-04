using System;
using System.Collections.Generic;
using System.Text;

namespace DD.DomainGenerator.GitHub
{
    public class Definitions
    {
        public struct DeployResponseParametersDefinitions
        {
            public struct CreateGithubRepositoryFromMicroService
            {
                public const string CloneUrl = "CloneUrl";
                public const string CreatedAt = "CreatedAt";
                public const string FullName = "FullName";
                public const string GitUrl = "GitUrl";
                public const string HtmlUrl = "HtmlUrl";
                public const string Name = "Name";
                public const string OwnerId = "Owner.Id";
                public const string OwnerLogin = "Owner.Login";
                public const string Private = "Private";
                public const string SshUrl = "SshUrl";
                public const string SvnUrl = "SvnUrl";
                public const string Url = "Url";
            }
        }
    }
}
