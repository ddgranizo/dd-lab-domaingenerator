using DD.DomainGenerator.Models;
using DD.DomainGenerator.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace DD.DomainGenerator.DeployActions
{


    public class CloneGitRepositoryFromMicroService : DeployActionUnit
    {
        public const string ActionName = "CloneGitRepositoryFromMicroService";
        public const string ActionDescription = "Clone Git repository";

        public IGitClientService GitClientService { get; }
        public IFileService FileService { get; }

        public CloneGitRepositoryFromMicroService(
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

                var createRepositoryFolderDependency = GetDependency<CreateRepositoriesFolderFromMicroService>(sourceActionExecution, currentExecutionDeployActions);
                var createGithubRepositoryDependency = GetDependency<CreateGithubRepositoryFromMicroService>(sourceActionExecution, currentExecutionDeployActions);
                var pathParameter = Definitions.DeployResponseParametersDefinitions.CreateRepositoriesFolderFromMicroService.Path;
                var repositoriesPath = createRepositoryFolderDependency.ResponseParameters[pathParameter] as string;
                var repositoryNameParameter = GitHub.Definitions.DeployResponseParametersDefinitions.CreateGithubRepositoryFromMicroService.Name;
                var repositoryName = createGithubRepositoryDependency.ResponseParameters[repositoryNameParameter] as string;
                var path = FileService.ConcatDirectoryAndFileOrFolder(repositoriesPath, repositoryName);

                var settingGit = GetSetting(projectState, Definitions.SettingsDefinitions.GitExePath);
                GitClientService.Initialize(settingGit);

                var existsFolder = FileService.ExistsFolder(path);
                if (!existsFolder)
                {
                    return new DeployActionUnitResponse()
                        .Ok(DeployActionUnitResponse.DeployActionResponseType.NotCompletedJob);
                }
                var existsRepoInPath = GitClientService.ExistsRepositoryInFolder(path);
                if (existsRepoInPath)
                {
                    return new DeployActionUnitResponse()
                        .Ok(GetParameters(path), DeployActionUnitResponse.DeployActionResponseType.AlreadyCompletedJob);
                }
                return new DeployActionUnitResponse()
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
                var createRepositoryFolderDependency = GetDependency<CreateRepositoriesFolderFromMicroService>(sourceActionExecution, currentExecutionDeployActions);
                var createGithubRepositoryDependency = GetDependency<CreateGithubRepositoryFromMicroService>(sourceActionExecution, currentExecutionDeployActions);
                var pathParameter = Definitions.DeployResponseParametersDefinitions.CreateRepositoriesFolderFromMicroService.Path;
                var repositoriesPath = createRepositoryFolderDependency.ResponseParameters[pathParameter] as string;
                var repositorySvnUrl = GitHub.Definitions.DeployResponseParametersDefinitions.CreateGithubRepositoryFromMicroService.SvnUrl;
                var repositoryNameParameter = GitHub.Definitions.DeployResponseParametersDefinitions.CreateGithubRepositoryFromMicroService.Name;

                var repositoryName = createGithubRepositoryDependency.ResponseParameters[repositoryNameParameter] as string;
                var path = FileService.ConcatDirectoryAndFileOrFolder(repositoriesPath, repositoryName);
                var repositoryUrl = createGithubRepositoryDependency.ResponseParameters[repositorySvnUrl] as string;

                var settingGit = GetSetting(projectState, Definitions.SettingsDefinitions.GitExePath);
                GitClientService.Initialize(settingGit);
                GitClientService.CloneRepository(repositoriesPath, repositoryUrl);
                return new DeployActionUnitResponse()
                    .Ok(GetParameters(path), DeployActionUnitResponse.DeployActionResponseType.NotCompletedJob);
            }
            catch (Exception ex)
            {
                return new DeployActionUnitResponse()
                    .Error(ex);
            }
        }


        private static Dictionary<string, object> GetParameters(string repoPath)
        {
            return new Dictionary<string, object>()
            {
                {"Path", repoPath }
            };
        }
    }
}
