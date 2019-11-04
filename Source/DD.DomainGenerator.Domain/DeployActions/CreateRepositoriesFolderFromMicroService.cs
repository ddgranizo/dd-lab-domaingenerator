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
    public class CreateRepositoriesFolderFromMicroService : DeployActionUnit
    {
        
        public const string ActionName = "CreateRepositoriesFolderFromMicroService";
        public const string ActionDescription = "Create repositories folder for checkout microservice repository";
        public CreateRepositoriesFolderFromMicroService(ActionExecution actionExecution, IFileService fileService)
            : base(actionExecution, ActionName, ActionDescription, DeployManager.Phases.EmptyProject, Positions.First, Positions.Second)
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
                var baseFolder = projectState.ProjectPath;
                if (string.IsNullOrEmpty(baseFolder))
                {
                    throw new Exception("Project path folder undefined");
                }

                var completeName = $"{projectState.Name}\\{Definitions.DeployDefinitions.RepositoriesFolderName}";
                var repositoriesFolder = FileService.ConcatDirectoryAndFileOrFolder(baseFolder, completeName);
                var existsRepositoriesFolder = FileService.ExistsFolder(repositoriesFolder);
                return existsRepositoriesFolder
                    ? new DeployActionUnitResponse()
                        .Ok(GetResponseParameters(repositoriesFolder), DeployActionUnitResponse.DeployActionResponseType.AlreadyCompletedJob)
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
                { Definitions.DeployResponseParametersDefinitions.CreateRepositoriesFolderFromMicroService.Path, path }
            };
        }

        public override DeployActionUnitResponse ExecuteDeploy(
            ProjectState projectState,
            ActionExecution sourceActionExecution,
            List<DeployActionUnit> currentExecutionDeployActions)
        {
            try
            {
                var baseFolder = projectState.ProjectPath;
                if (string.IsNullOrEmpty(baseFolder))
                {
                    throw new Exception("Project path folder undefined");
                }
                var completeName = $"{projectState.Name}\\{Definitions.DeployDefinitions.RepositoriesFolderName}";
                var repositoriesFolder = FileService.ConcatDirectoryAndFileOrFolder(baseFolder, completeName);
                var existsRepositoriesFolder = FileService.ExistsFolder(repositoriesFolder);
                if (!existsRepositoriesFolder)
                {
                    FileService.CreateFolder(repositoriesFolder);
                }
                return new DeployActionUnitResponse()
                        .Ok(GetResponseParameters(repositoriesFolder));
            }
            catch (Exception ex)
            {
                return new DeployActionUnitResponse()
                    .Error(ex);
            }
        }

     
    }
}
