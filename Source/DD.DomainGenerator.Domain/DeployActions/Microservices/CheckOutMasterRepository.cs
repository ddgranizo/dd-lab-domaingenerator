using DD.DomainGenerator.DeployActions.Base;
using DD.DomainGenerator.Models;
using DD.DomainGenerator.Services;
using System;
using System.Collections.Generic;
using System.Text;
using static DD.DomainGenerator.Definitions;

namespace DD.DomainGenerator.DeployActions.Microservices
{


    public class CheckOutMasterRepository : DeployActionUnit
    {
        
        public const string ActionName = "CheckOutMasterRepository";
        public const string ActionDescription = "Checkout master repository";

        public IGitClientService GitClientService { get; }
        public IFileService FileService { get; }

        public CheckOutMasterRepository(
            ActionExecution actionExecution,
            IGitClientService gitClientService,
            IFileService fileService)
            : base(actionExecution, ActionName, ActionDescription, DeployManager.Phases.EmptyProject, Positions.First, Positions.Forth)
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
                var cloneRepositoryFolderDependency = GetDependencyFromSameSource<CloneGitRepository>(sourceActionExecution, currentExecutionDeployActions);
                var pathParameter = Definitions.DeployResponseParametersDefinitions.MicroServices.CloneGitRepository.Path;
                var repositoryPath = cloneRepositoryFolderDependency.ResponseParameters[pathParameter] as string;

                var settingGit = GetSetting(projectState, Definitions.SettingsDefinitions.GitExePath);
                GitClientService.Initialize(settingGit);

                var existsFolder = FileService.ExistsFolder(repositoryPath);
                if (!existsFolder)
                {
                    return new DeployActionUnitResponse()
                        .Ok(DeployActionUnitResponse.DeployActionResponseType.NotCompletedJob);
                }
                var existsRepoInPath = GitClientService.ExistsRepositoryInFolder(repositoryPath);
                if (!existsRepoInPath)
                {
                    return new DeployActionUnitResponse()
                        .Ok(DeployActionUnitResponse.DeployActionResponseType.NotCompletedJob);
                }
                var currentBranch = GitClientService.GetCurrentBranchRepository(repositoryPath);
                if (currentBranch != BranchDefinitions.Master)
                {
                    return new DeployActionUnitResponse()
                        .Ok(DeployActionUnitResponse.DeployActionResponseType.NotCompletedJob);
                }
                return new DeployActionUnitResponse()
                    .Ok(GetParameters(currentBranch), DeployActionUnitResponse.DeployActionResponseType.AlreadyCompletedJob);
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
                var cloneRepositoryFolderDependency = GetDependencyFromSameSource<CloneGitRepository>(sourceActionExecution, currentExecutionDeployActions);
                var pathParameter = Definitions.DeployResponseParametersDefinitions.MicroServices.CloneGitRepository.Path;
                var repositoryPath = cloneRepositoryFolderDependency.ResponseParameters[pathParameter] as string;

                var settingGit = GetSetting(projectState, Definitions.SettingsDefinitions.GitExePath);
                GitClientService.Initialize(settingGit);

                var existsFolder = FileService.ExistsFolder(repositoryPath);
                if (!existsFolder)
                {
                    return new DeployActionUnitResponse()
                        .Error($"Folder {existsFolder} doesn't exists");
                }
                var existsRepoInPath = GitClientService.ExistsRepositoryInFolder(repositoryPath);
                if (!existsRepoInPath)
                {
                    return new DeployActionUnitResponse()
                        .Error($"Folder {existsFolder} doesn't contain git repository");
                }
                var currentBranch = GitClientService.GetCurrentBranchRepository(repositoryPath);
                if (currentBranch != BranchDefinitions.Master)
                {
                    GitClientService.CheckoutBranch(repositoryPath, BranchDefinitions.Master);
                }
                return new DeployActionUnitResponse()
                    .Ok(GetParameters(BranchDefinitions.Master), DeployActionUnitResponse.DeployActionResponseType.AlreadyCompletedJob);

            }
            catch (Exception ex)
            {
                return new DeployActionUnitResponse()
                    .Error(ex);
            }
        }


        private static Dictionary<string, object> GetParameters(string branch)
        {
            return new Dictionary<string, object>()
            {
                {Definitions.DeployResponseParametersDefinitions.MicroServices.CheckOutMasterRepository.Branch, branch }
            };
        }
    }
}
