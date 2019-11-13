using DD.DomainGenerator.DeployActions.Base;
using DD.DomainGenerator.Models;
using DD.DomainGenerator.Services;
using System;
using System.Collections.Generic;
using System.Text;
using static DD.DomainGenerator.Definitions;

namespace DD.DomainGenerator.DeployActions.Project
{
   
    public class CreateDomainProjectFolder : DeployActionUnit
    {

        public const string ActionName = "CreateDomainProjectFolder";
        public const string ActionDescription = "Create domain project folder";
        public CreateDomainProjectFolder(ActionExecution actionExecution, IFileService fileService)
            : base(actionExecution, ActionName, ActionDescription, DeployManager.Phases.EmptyProject, Positions.Second, Positions.Second)
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
                var cloneRepositoryFolderDependency = GetDependencyFromSameSource<CreateDomainSolutionFile>
                    (sourceActionExecution, currentExecutionDeployActions);

                var solutionPathParameter = DeployResponseParametersDefinitions.MicroServices.CreateSolutionFile.Path;
                var solutionPath = cloneRepositoryFolderDependency.ResponseParameters[solutionPathParameter] as string;
                var projectFolderName = $"{projectState.NameSpace}.{projectState.Name}.Domain";
                var completeFolder = FileService.ConcatDirectoryAndFileOrFolder(solutionPath, projectFolderName);
                var exisistsFolder = FileService.ExistsFolder(completeFolder);
                return exisistsFolder
                    ? new DeployActionUnitResponse()
                        .Ok(GetResponseParameters(completeFolder), DeployActionUnitResponse.DeployActionResponseType.AlreadyCompletedJob)
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
                { DeployResponseParametersDefinitions.Project.CreateDomainProjectFolder.Path, path },
            };
        }

        public override DeployActionUnitResponse ExecuteDeploy(
            ProjectState projectState,
            ActionExecution sourceActionExecution,
            List<DeployActionUnit> currentExecutionDeployActions)
        {
            try
            {
                var cloneRepositoryFolderDependency = GetDependencyFromSameSource<CreateDomainSolutionFile>
                   (sourceActionExecution, currentExecutionDeployActions);

                var solutionPathParameter = DeployResponseParametersDefinitions.MicroServices.CreateSolutionFile.Path;
                var solutionPath = cloneRepositoryFolderDependency.ResponseParameters[solutionPathParameter] as string;
                var projectFolderName = $"{projectState.NameSpace}.{projectState.Name}.Domain";
                var completeFolder = FileService.ConcatDirectoryAndFileOrFolder(solutionPath, projectFolderName);
                var exisistsFolder = FileService.ExistsFolder(completeFolder);
                if (!exisistsFolder)
                {
                    FileService.CreateFolder(completeFolder);
                }
                return new DeployActionUnitResponse()
                        .Ok(GetResponseParameters(completeFolder), DeployActionUnitResponse.DeployActionResponseType.AlreadyCompletedJob);
            }
            catch (Exception ex)
            {
                return new DeployActionUnitResponse()
                    .Error(ex);
            }
        }


    }
}
