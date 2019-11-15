using DD.DomainGenerator.DeployActions.Base;
using DD.DomainGenerator.Models;
using DD.DomainGenerator.Services;
using DD.DomainGenerator.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static DD.DomainGenerator.Definitions;

namespace DD.DomainGenerator.DeployActions.Project
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
                var baseFolder = projectState.Path;
                if (string.IsNullOrEmpty(baseFolder))
                {
                    throw new Exception("Project path folder undefined");
                }

                var completeRepositoriesName = $"{projectState.Name}\\{Definitions.DeployDefinitions.RepositoriesFolderName}";
                var repositoriesFolder = FileService.ConcatDirectoryAndFileOrFolder(baseFolder, completeRepositoriesName);
                var completeTempName = $"{projectState.Name}\\{Definitions.DeployDefinitions.TempFolderName}";
                var tempFolder = FileService.ConcatDirectoryAndFileOrFolder(baseFolder, completeTempName);
                var existsRepositoriesFolder = FileService.ExistsFolder(repositoriesFolder) && FileService.ExistsFolder(tempFolder);
                return existsRepositoriesFolder
                    ? new DeployActionUnitResponse()
                        .Ok(GetResponseParameters(repositoriesFolder, tempFolder), DeployActionUnitResponse.DeployActionResponseType.AlreadyCompletedJob)
                    : new DeployActionUnitResponse()
                        .Ok(DeployActionUnitResponse.DeployActionResponseType.NotCompletedJob);
            }
            catch (Exception ex)
            {
                return new DeployActionUnitResponse()
                    .Error(ex);
            }
        }


        private Dictionary<string, object> GetResponseParameters(string repositoriesPath, string tempPath)
        {
            return new Dictionary<string, object>()
            {
                { DeployResponseParametersDefinitions.Project.CreateRepositoriesFolder.RepositoryPath, repositoriesPath },
                { DeployResponseParametersDefinitions.Project.CreateRepositoriesFolder.TempPath, tempPath },
            };
        }

        public override DeployActionUnitResponse ExecuteDeploy(
            ProjectState projectState,
            ActionExecution sourceActionExecution,
            List<DeployActionUnit> currentExecutionDeployActions)
        {
            try
            {
                var baseFolder = projectState.Path;
                if (string.IsNullOrEmpty(baseFolder))
                {
                    throw new Exception("Project path folder undefined");
                }
                var completeName = $"{projectState.Name}\\{Definitions.DeployDefinitions.RepositoriesFolderName}";
                var repositoriesFolder = FileService.ConcatDirectoryAndFileOrFolder(baseFolder, completeName);
                var completeTempName = $"{projectState.Name}\\{Definitions.DeployDefinitions.TempFolderName}";
                var tempFolder = FileService.ConcatDirectoryAndFileOrFolder(baseFolder, completeTempName);

                var existsRepositoriesFolder = FileService.ExistsFolder(repositoriesFolder);
                if (!existsRepositoriesFolder)
                {
                    FileService.CreateFolder(repositoriesFolder);
                }
                var existsTempFolder = FileService.ExistsFolder(tempFolder);
                if (!existsTempFolder)
                {
                    FileService.CreateFolder(tempFolder);
                }
                return new DeployActionUnitResponse()
                        .Ok(GetResponseParameters(repositoriesFolder, tempFolder));
            }
            catch (Exception ex)
            {
                return new DeployActionUnitResponse()
                    .Error(ex);
            }
        }


    }
}
