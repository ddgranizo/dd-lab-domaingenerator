using DD.DomainGenerator.GitHub;
using DD.DomainGenerator.GitHub.Extensions;
using DD.DomainGenerator.Models;
using DD.DomainGenerator.Services;
using DD.DomainGenerator.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DD.DomainGenerator.DeployActions
{
    public class CreateRepositoryFolderFromMicroService : DeployActionUnit
    {
        public const string RepositoriesFolderName = "Repositories";
        public const string ActionName = "CreateRepositoryFolderFromMicroService";
        public const string ActionDescription = "Create folder for checkout microservice repository";
        public CreateRepositoryFolderFromMicroService(ActionExecution actionExecution, IFileService fileService)
            : base(actionExecution, ActionName, ActionDescription, DeployManager.Phases.EmptyProject, Positions.Second)
        {
            FileService = fileService ?? throw new ArgumentNullException(nameof(fileService));
        }

        public IFileService FileService { get; }

        public override DeployActionUnitResponse ExecuteCheck(
            ProjectState projectState,
            ActionExecution sourceActionExecution,
            List<DeployActionUnit> currentExecutionDeployActions)
        {
            try
            {
                var sameSourceDeployActions = currentExecutionDeployActions
                    .Where(k => k.ActionExecution.Id == sourceActionExecution.Id && k.State == DeployState.Completed);

                string baseFolder = GetBaseFolder(projectState, sameSourceDeployActions);

                var responseParameters = sameSourceDeployActions.First().ResponseParameters;
                var repoName = responseParameters["Name"] as string;
                var completedFolder = FileService.ConcatDirectoryAndFileOrFolder(baseFolder, $"{RepositoriesFolderName}\\{repoName}");
                var existsRepositoryFolder = FileService.ExistsFolder(completedFolder);
                return existsRepositoryFolder
                    ? new DeployActionUnitResponse()
                        .Ok(GetResponseParameters(completedFolder), DeployActionUnitResponse.DeployActionResponseType.AlreadyCompletedJob)
                    : new DeployActionUnitResponse()
                        .Ok(DeployActionUnitResponse.DeployActionResponseType.NotCompletedJob);
            }
            catch (Exception ex)
            {
                return new DeployActionUnitResponse()
                    .Error(ex);
            }
        }


        private Dictionary<string, object> GetResponseParameters(string path)
        {
            return new Dictionary<string, object>()
            {
                { "Path", path }
            };
        }

        public override DeployActionUnitResponse ExecuteDeploy(
            ProjectState projectState,
            ActionExecution sourceActionExecution,
            List<DeployActionUnit> currentExecutionDeployActions)
        {
            try
            {
                var sameSourceDeployActions = currentExecutionDeployActions
                    .Where(k => k.ActionExecution.Id == sourceActionExecution.Id && k.State == DeployState.Completed);

                string baseFolder = GetBaseFolder(projectState, sameSourceDeployActions);

                var responseParameters = sameSourceDeployActions.First().ResponseParameters;
                var repoName = responseParameters["Name"] as string;
                var completedFolder = FileService.ConcatDirectoryAndFileOrFolder(baseFolder, $"{RepositoriesFolderName}\\{repoName}");
                var existsRepositoryFolder = FileService.ExistsFolder(completedFolder);
                if (!existsRepositoryFolder)
                {
                    FileService.CreateFolder(completedFolder);
                }
                return new DeployActionUnitResponse()
                        .Ok(GetResponseParameters(completedFolder));
            }
            catch (Exception ex)
            {
                return new DeployActionUnitResponse()
                    .Error(ex);
            }
        }

        private string GetBaseFolder(ProjectState projectState, IEnumerable<DeployActionUnit> sameSourceDeployActions)
        {
            var baseFolder = projectState.ProjectPath;
            if (string.IsNullOrEmpty(baseFolder))
            {
                throw new Exception("Project path folder undefined");
            }

            var createRepositoryAction = sameSourceDeployActions
                .OfType<CreateGithubRepositoryFromMicroService>();
            if (createRepositoryAction.Count() == 0)
            {
                throw new Exception("Can't find any 'CreateGithubRepositoryFromMicroService' deploy action required before this action");
            }
            var repositoriesFolder = FileService.ConcatDirectoryAndFileOrFolder(baseFolder, RepositoriesFolderName);
            var existsRepositoriesFolder = FileService.ExistsFolder(repositoriesFolder);
            if (!existsRepositoriesFolder)
            {
                FileService.CreateFolder(repositoriesFolder);
            }

            return baseFolder;
        }
    }
}
