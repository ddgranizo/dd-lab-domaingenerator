using DD.DomainGenerator.DeployActions.Base;
using DD.DomainGenerator.Models;
using DD.DomainGenerator.Services;
using System;
using System.Collections.Generic;
using System.Text;
using static DD.DomainGenerator.Definitions;

namespace DD.DomainGenerator.DeployActions.Project
{
  
    public class CloneDomainGithubRepository : DeployActionUnit
    {
        public const string ActionName = "CloneDomainGithubRepository";
        public const string ActionDescription = "Clone domain Git repository";

        public IGitClientService GitClientService { get; }
        public IFileService FileService { get; }

        public CloneDomainGithubRepository(
            ActionExecution actionExecution,
            IGitClientService gitClientService,
            IFileService fileService)
            : base(actionExecution, ActionName, ActionDescription, DeployManager.Phases.EmptyProject, Positions.First, Positions.Third)
        {
            GitClientService = gitClientService ?? throw new ArgumentNullException(nameof(gitClientService));
            FileService = fileService ?? throw new ArgumentNullException(nameof(fileService));
        }

        public override DeployActionUnitResponse ExecuteCheck(
            ProjectState projectState,
            ActionExecution sourceActionExecution,
            List<DeployActionUnit> currentExecutionDeployActions)
        {
            try
            {
                return new  DeployActionUnitResponse()
                    .Ok(DeployActionUnitResponse.DeployActionResponseType.NotCompletedJob);
              
            }
            catch (Exception ex)
            {
                return new DeployActionUnitResponse()
                    .Error(ex);
            }
        }

        public override DeployActionUnitResponse ExecuteDeploy(
            ProjectState projectState,
            ActionExecution sourceActionExecution,
            List<DeployActionUnit> currentExecutionDeployActions)
        {
            try
            {
                var createRepositoryFolderDependency = GetDependency<CreateRepositoriesFolder>(sourceActionExecution, currentExecutionDeployActions);
                var createGithubRepositoryDependency = GetDependencyFromSameSource<CreateDomainGithubRepository>(sourceActionExecution, currentExecutionDeployActions);
                var tempPathParameter = DeployResponseParametersDefinitions.Project.CreateRepositoriesFolder.TempPath;
                var pathParameter = DeployResponseParametersDefinitions.Project.CreateRepositoriesFolder.RepositoryPath;
                var repositoriesPath = createRepositoryFolderDependency.ResponseParameters[pathParameter] as string;
                var tempPath = createRepositoryFolderDependency.ResponseParameters[tempPathParameter] as string;

                var repositoryNameParameter = DeployResponseParametersDefinitions.Project.CreateDomainGithubRepository.Name;
                var repositoryName = createGithubRepositoryDependency.ResponseParameters[repositoryNameParameter] as string;
                var path = FileService.ConcatDirectoryAndFileOrFolder(repositoriesPath, repositoryName);
                var completeTempPath = FileService.ConcatDirectoryAndFileOrFolder(tempPath, repositoryName);

                var repositorySvnUrl = DeployResponseParametersDefinitions.MicroServices.CreateGithubRepository.SvnUrl;
                var repositoryUrl = createGithubRepositoryDependency.ResponseParameters[repositorySvnUrl] as string;

                var settingGit = GetSetting(projectState, SettingsDefinitions.GitExePath);
                GitClientService.Initialize(settingGit);
                var repositoryPath = FileService.ConcatDirectoryAndFileOrFolder(repositoriesPath, repositoryName);
                if (FileService.ExistsFolder(repositoryPath))
                {
                    FileService.DeleteFolder(repositoryPath);
                }
                GitClientService.CloneRepository(repositoriesPath, repositoryUrl);
                if (FileService.ExistsFolder(completeTempPath))
                {
                    FileService.DeleteFolder(completeTempPath);
                }
                FileService.CopyFolder(repositoryPath, completeTempPath);
                return new DeployActionUnitResponse()
                    .Ok(GetParameters(path, completeTempPath));
            }
            catch (Exception ex)
            {
                return new DeployActionUnitResponse()
                    .Error(ex);
            }
        }


        private static Dictionary<string, object> GetParameters(string repoPath, string tempPath)
        {
            return new Dictionary<string, object>()
            {
                {DeployResponseParametersDefinitions.Project.CloneDomainGitRepository.Path, repoPath },
                {DeployResponseParametersDefinitions.Project.CloneDomainGitRepository.TempPath, tempPath },
            };
        }
    }
}
