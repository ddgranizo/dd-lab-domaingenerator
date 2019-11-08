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
            public struct AddDomainInMicroservice
            {
                public const string DomainName = "domainname";
                public const string MicroserviceName = "microservicename";
            }
        }

        public struct DeployResponseParametersDefinitions
        {
            public struct Project
            {
                public struct CreateRepositoriesFolder
                {
                    public const string Path = "Path";
                }
                public struct CreateDomainGithubRepository
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
                public struct CloneDomainGithubRepository
                {
                    public const string Path = "Path";
                }
            }
            public struct MicroServices
            {
                
                public struct CloneGitRepository
                {
                    public const string Path = "Path";
                }
                public struct CheckOutMasterRepository
                {
                    public const string Branch = "branch";
                }
                public struct CleanRepositoryFolder
                {
                    public const string Path = "Path";
                }
                public struct CreateRepositoryFolderStructure
                {
                    public const string Source = "Source";
                    public const string Doc = "Doc";
                }
                public struct CreateGithubRepository
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
                public struct CreateSolutionFile
                {
                    public const string Path = "Path";
                    public const string SolutionFileName = "SolutionFileName";
                    public const string SolutionFilePath = "SolutionFilePath";
                }
            }
        }

        public struct DeployDefinitions
        {
            public const string RepositoriesFolderName = "Repositories";
            public const string SourceFolderName = "Source";
            public const string DocFolderName = "Doc";
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

        public struct SettingsDefinitions
        {
            public const string GitExePath = "GitExePath";
            public const string DotNetExePath = "DotNetExePath";
            public const string DDCliExePath = "DDCliExePath";
        }

        public static string[] Settings = new string[]
        {
            SettingsDefinitions.GitExePath,
            SettingsDefinitions.DotNetExePath,
            SettingsDefinitions.DDCliExePath,
        };

        public struct BranchDefinitions
        {
            public const string Master = "master";
        }
        
    }
}
