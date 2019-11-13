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
                public struct CreateDomainProjectFolder
                {
                    public const string Path = "Path";
                }
                public struct CreateRepositoriesFolder
                {
                    public const string RepositoryPath = "RepositoryPath";
                    public const string TempPath = "TempPath";
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

                public struct CreateDomainSolutionFile
                {
                    public const string Path = "Path";
                    public const string SolutionFileName = "SolutionFileName";
                    public const string SolutionFilePath = "SolutionFilePath";
                }

                public struct CreateDomainRepositoryFolderStructure
                {
                    public const string Source = "Source";
                    public const string Doc = "Doc";
                }

                public struct CreateDomainProject
                {
                    public const string ProjectFileName = "ProjectFileName";
                    public const string ProjectFilePath = "ProjectFilePath";
                    public const string Path = "Path";
                }
                public struct CloneDomainGitRepository
                {
                    public const string Path = "Path";
                    public const string TempPath = "TempPath";
                }
                public struct CleanDomainRepositoryFolder
                {
                    public const string Path = "Path";
                }
                public struct CheckOutMasterDomainRepository
                {
                    public const string Branch = "branch";
                }
                public struct AddDomainProjectToDomainSolution
                {
                    public const string DomainSolutionFolder = "DomainSolutionFolder";
                    public const string DomainSolutionName = "DomainSolutionName";
                    public const string RelativePath = "RelativePath";
                }
            }
            public struct MicroServices
            {
                
                public struct CloneGitRepository
                {
                    public const string Path = "Path";
                    public const string TempPath = "TempPath";
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
            public const string TempFolderName = "Temp";
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

        public struct DefaultViewNames
        {
            public const string All = "GetAll";
            public const string Active = "GetActive";
            public const string Inactive = "GetInactive";
            public const string CreatedOnAtYear = "GetByCreatedOnAtYear";
            public const string CreatedOnAtMonth = "GetByCreatedOnAtMonth";
            public const string CreatedOnAtDay = "GetByCreatedOnAtDay";
            public const string CreatedOnAtDayAndHour = "GetByCreatedOnAtDayAndHour";
            public const string CreatedOnAtDayAndHourAndMinute = "GetByCreatedOnAtDayAndHourAndMinute";
            public const string CreatedOnAtDayAndHourAndMinuteSecond = "GetByCreatedOnAtDayAndHourAndMinuteSecond";
            public const string CreatedOnBetween = "GetByCreatedOnBetween";
            public const string CreatedOnBefore = "GetByCreatedOnBefore";
            public const string CreatedOnAfter = "GetByCreatedOnAfter";
            public const string ModifiedOnAtYear = "GetByModifiedOnAtYear";
            public const string ModifiedOnAtMonth = "GetByModifiedOnAtMonth";
            public const string ModifiedOnAtDay = "GetByModifiedOnAtDay";
            public const string ModifiedOnAtDayAndHour = "GetByModifiedOnAtDayAndHour";
            public const string ModifiedOnAtDayAndHourAndMinute = "GetByModifiedOnAtDayAndHourAndMinute";
            public const string ModifiedOnAtDayAndHourAndMinuteSecond = "GetByModifiedOnAtDayAndHourAndMinuteSecond";
            public const string ModifiedOnBetween = "GetByModifiedOnBetween";
            public const string ModifiedOnBefore = "GetByModifiedOnBefore";
            public const string ModifiedOnAfter = "GetByModifiedOnAfter";

            public const string CreatedBy = "GetByCreatedBy";
            public const string ModifiedBy = "GetByModifiedBy";
            public const string Owner = "GetByOwner";
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
            public const string DDCliDomainProjectTemplatePath = "DDCliDomainProjectTemplatePath";
        }

        public static string[] Settings = new string[]
        {
            SettingsDefinitions.GitExePath,
            SettingsDefinitions.DotNetExePath,
            SettingsDefinitions.DDCliExePath,
            SettingsDefinitions.DDCliDomainProjectTemplatePath,
        };

        public struct BranchDefinitions
        {
            public const string Master = "master";
        }
        

        public struct ProjectTemplates
        {
            public struct DomainProject
            {
                public struct TemplateParameters
                {
                    public const string Namespace = "MYNAMESPACE";
                    public const string App = "MYAPP";
                }
            }
        }
    }
}
