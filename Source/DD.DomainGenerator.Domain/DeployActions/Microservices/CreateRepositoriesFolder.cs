using DD.DomainGenerator.DeployActions.Base;
using DD.DomainGenerator.Models;
using DD.DomainGenerator.Services;
using DD.DomainGenerator.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static DD.DomainGenerator.Definitions;

namespace DD.DomainGenerator.DeployActions.Microservices
{
    public class CreateRepositoriesFolder : DeployActionUnit
    {
        
        public const string ActionName = "CreateRepositoriesFolder";
        public const string ActionDescription = "Create repositories folder for checkout microservice repository";
        public CreateRepositoriesFolder(ActionExecution actionExecution, IFileService fileService)
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
                { DeployResponseParametersDefinitions.MicroServices.CreateRepositoriesFolder.Path, path }
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
