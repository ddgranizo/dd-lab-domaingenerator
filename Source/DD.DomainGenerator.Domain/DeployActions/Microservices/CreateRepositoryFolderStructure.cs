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
    public class CreateRepositoryFolderStructure : DeployActionUnit
    {
        
        public const string ActionName = "CreateRepositoryFolderStructure";
        public const string ActionDescription = "Create repository folder structure";
        public CreateRepositoryFolderStructure(ActionExecution actionExecution, IFileService fileService)
            : base(actionExecution, ActionName, ActionDescription, DeployManager.Phases.EmptyProject, Positions.First, Positions.Sixth)
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
                var cloneRepositoryFolderDependency = GetDependencyFromSameSource<CloneGitRepository>
                    (sourceActionExecution, currentExecutionDeployActions);
                var pathParameter = DeployResponseParametersDefinitions.MicroServices.CloneGitRepository.Path;
                var repositoryPath = cloneRepositoryFolderDependency.ResponseParameters[pathParameter] as string;

                var sourceFolder = FileService.ConcatDirectoryAndFileOrFolder
                    (repositoryPath, DeployDefinitions.SourceFolderName);
                var existsRepositoriesFolder = FileService.ExistsFolder(sourceFolder);
                var docFolder = FileService.ConcatDirectoryAndFileOrFolder
                    (repositoryPath, DeployDefinitions.DocFolderName);
                var existsDocFolder = FileService.ExistsFolder(docFolder);

                var existsAll = existsRepositoriesFolder
                    && existsDocFolder;
                return existsAll
                    ? new DeployActionUnitResponse()
                        .Ok(GetResponseParameters(sourceFolder, docFolder), DeployActionUnitResponse.DeployActionResponseType.AlreadyCompletedJob)
                    : new DeployActionUnitResponse()
                        .Ok(DeployActionUnitResponse.DeployActionResponseType.NotCompletedJob);
            }
            catch (Exception ex)
            {
                return new DeployActionUnitResponse()
                    .Error(ex);
            }
        }


        private Dictionary<string, object> GetResponseParameters(string sourcePath, string docPath)
        {
            return new Dictionary<string, object>()
            {
                { DeployResponseParametersDefinitions.MicroServices.CreateRepositoryFolderStructure.Source, sourcePath },
                 { DeployResponseParametersDefinitions.MicroServices.CreateRepositoryFolderStructure.Doc, docPath }
            };
        }

        public override DeployActionUnitResponse ExecuteDeploy(
            ProjectState projectState,
            ActionExecution sourceActionExecution,
            List<DeployActionUnit> currentExecutionDeployActions)
        {
            try
            {
                var cloneRepositoryFolderDependency = GetDependencyFromSameSource<CloneGitRepository>
                    (sourceActionExecution, currentExecutionDeployActions);
                var pathParameter = DeployResponseParametersDefinitions.MicroServices.CloneGitRepository.Path;
                var repositoryPath = cloneRepositoryFolderDependency.ResponseParameters[pathParameter] as string;

                var sourceFolder = FileService.ConcatDirectoryAndFileOrFolder
                    (repositoryPath, DeployDefinitions.SourceFolderName);
                var existsRepositoriesFolder = FileService.ExistsFolder(sourceFolder);
                var docFolder = FileService.ConcatDirectoryAndFileOrFolder
                    (repositoryPath, DeployDefinitions.DocFolderName);
                var existsDocFolder = FileService.ExistsFolder(docFolder);
                if (!existsRepositoriesFolder)
                {
                    FileService.CreateFolder(sourceFolder);
                }
                if (!existsDocFolder)
                {
                    FileService.CreateFolder(docFolder);
                }
                return new DeployActionUnitResponse()
                        .Ok(GetResponseParameters(sourceFolder, docFolder), 
                        DeployActionUnitResponse.DeployActionResponseType.AlreadyCompletedJob);
                    
            }
            catch (Exception ex)
            {
                return new DeployActionUnitResponse()
                    .Error(ex);
            }
        }
    }
}
