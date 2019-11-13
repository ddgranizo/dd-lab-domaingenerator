using DD.DomainGenerator.DeployActions.Base;
using DD.DomainGenerator.Models;
using DD.DomainGenerator.Services;
using System;
using System.Collections.Generic;
using System.Text;
using static DD.DomainGenerator.Definitions;

namespace DD.DomainGenerator.DeployActions.Project
{
   

    public class CleanDomainRepositoryFolder : DeployActionUnit
    {

        public const string ActionName = "CleanDomainRepositoryFolder";
        public const string ActionDescription = "Clean domain repository folder";

        public IFileService FileService { get; }

        public CleanDomainRepositoryFolder(
            ActionExecution actionExecution,
            IFileService fileService)
            : base(actionExecution, ActionName, ActionDescription, DeployManager.Phases.EmptyProject, Positions.First, Positions.Fifth)
        {
            FileService = fileService ?? throw new ArgumentNullException(nameof(fileService));
        }

        public override DeployActionUnitResponse ExecuteCheck(
            ProjectState projectState,
            ActionExecution sourceActionExecution,
            List<DeployActionUnit> currentExecutionDeployActions)
        {
            try
            {
                var cloneRepositoryFolderDependency = GetDependencyFromSameSource<CloneDomainGithubRepository>(sourceActionExecution, currentExecutionDeployActions);
                var pathParameter = DeployResponseParametersDefinitions.Project.CloneDomainGithubRepository.Path;
                var repositoryPath = cloneRepositoryFolderDependency.ResponseParameters[pathParameter] as string;

                var folderIsEmpty = FileService.FolderIsEmpty(repositoryPath);
                if (!folderIsEmpty)
                {
                    return new DeployActionUnitResponse()
                        .Ok(DeployActionUnitResponse.DeployActionResponseType.NotCompletedJob);
                }
                return new DeployActionUnitResponse()
                    .Ok(GetParameters(repositoryPath), DeployActionUnitResponse.DeployActionResponseType.AlreadyCompletedJob);
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
                var cloneRepositoryFolderDependency = GetDependencyFromSameSource<CloneDomainGithubRepository>(sourceActionExecution, currentExecutionDeployActions);
                var pathParameter = DeployResponseParametersDefinitions.Project.CloneDomainGithubRepository.Path;
                var repositoryPath = cloneRepositoryFolderDependency.ResponseParameters[pathParameter] as string;

                var folderIsEmpty = FileService.FolderIsEmpty(repositoryPath);
                if (!folderIsEmpty)
                {
                    FileService.CleanFolder(repositoryPath);
                }
                return new DeployActionUnitResponse()
                    .Ok(GetParameters(repositoryPath), DeployActionUnitResponse.DeployActionResponseType.AlreadyCompletedJob);

            }
            catch (Exception ex)
            {
                return new DeployActionUnitResponse()
                    .Error(ex);
            }
        }


        private static Dictionary<string, object> GetParameters(string path)
        {
            return new Dictionary<string, object>()
            {
                {DeployResponseParametersDefinitions.Project.CleanDomainRepositoryFolder.Path, path }
            };
        }
    }
}
