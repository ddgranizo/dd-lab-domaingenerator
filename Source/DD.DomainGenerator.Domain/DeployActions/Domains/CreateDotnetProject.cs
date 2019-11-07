using DD.DomainGenerator.DeployActions.Base;
using DD.DomainGenerator.DeployActions.Microservices;
using DD.DomainGenerator.Models;
using DD.DomainGenerator.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static DD.DomainGenerator.Definitions.ActionsParametersDefinitions;

namespace DD.DomainGenerator.DeployActions.Domains
{


    public class CreateDotnetProject : DeployActionUnit
    {

        public const string ActionName = "CreateDotnetProject";
        public const string ActionDescription = "Create dotnet project";

        public IDotnetService DotnetService { get; }
        public IFileService FileService { get; }

        public CreateDotnetProject(
            ActionExecution actionExecution,
            IDotnetService dotnetService,
            IFileService fileService)
            : base(actionExecution, ActionName, ActionDescription, DeployManager.Phases.AvailableSolutionFile, Positions.First, Positions.First)
        {
            DotnetService = dotnetService ?? throw new ArgumentNullException(nameof(dotnetService));
            FileService = fileService ?? throw new ArgumentNullException(nameof(fileService));
        }

        public override DeployActionUnitResponse ExecuteCheck(
            ProjectState projectState,
            ActionExecution sourceActionExecution,
            List<DeployActionUnit> currentExecutionDeployActions)
        {
            try
            {
                var domainName = sourceActionExecution.InputParameters[AddDomainInMicroservice.DomainName] as string;
                var microserviceName = sourceActionExecution.InputParameters[AddDomainInMicroservice.MicroserviceName] as string;

                var domain = projectState.Domains.First(k => k.Name == domainName);
                var microservice = projectState.Microservices.First(k => k.Name == microserviceName);

                var solutionDependency = GetDependency<CreateSolutionFile>
                    (sourceActionExecution, currentExecutionDeployActions, 
                        k => 
                            k.ActionExecution.OutputParameters.ContainsKey(AddMicroService.Name) 
                            && k.ActionExecution.OutputParameters[AddMicroService.Name] as string == microserviceName);


                //var domain = projectState.DomainInMicroServices.FirstOrDefault(k=>k.Domain.Name == )
                //var cloneRepositoryFolderDependency = GetDependencyFromSameSource<CloneGitRepository>(sourceActionExecution, currentExecutionDeployActions);
                //var pathParameter = Definitions.DeployResponseParametersDefinitions.MicroServices.CloneGitRepository.Path;
                //var repositoryPath = cloneRepositoryFolderDependency.ResponseParameters[pathParameter] as string;

                //var settingGit = GetSetting(projectState, Definitions.SettingsDefinitions.GitExePath);
                //GitClientService.Initialize(settingGit);

                //var existsFolder = FileService.ExistsFolder(repositoryPath);
                //if (!existsFolder)
                //{
                //    return new DeployActionUnitResponse()
                //        .Ok(DeployActionUnitResponse.DeployActionResponseType.NotCompletedJob);
                //}
                //var existsRepoInPath = GitClientService.ExistsRepositoryInFolder(repositoryPath);
                //if (!existsRepoInPath)
                //{
                //    return new DeployActionUnitResponse()
                //        .Ok(DeployActionUnitResponse.DeployActionResponseType.NotCompletedJob);
                //}
                //var currentBranch = GitClientService.GetCurrentBranchRepository(repositoryPath);
                //if (currentBranch != BranchDefinitions.Master)
                //{
                //    return new DeployActionUnitResponse()
                //        .Ok(DeployActionUnitResponse.DeployActionResponseType.NotCompletedJob);
                //}
                //return new DeployActionUnitResponse()
                //    .Ok(GetParameters(currentBranch), DeployActionUnitResponse.DeployActionResponseType.AlreadyCompletedJob);
                return null;
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
                //var cloneRepositoryFolderDependency = GetDependencyFromSameSource<CloneGitRepository>(sourceActionExecution, currentExecutionDeployActions);
                //var pathParameter = Definitions.DeployResponseParametersDefinitions.MicroServices.CloneGitRepository.Path;
                //var repositoryPath = cloneRepositoryFolderDependency.ResponseParameters[pathParameter] as string;

                //var settingGit = GetSetting(projectState, Definitions.SettingsDefinitions.GitExePath);
                //GitClientService.Initialize(settingGit);

                //var existsFolder = FileService.ExistsFolder(repositoryPath);
                //if (!existsFolder)
                //{
                //    return new DeployActionUnitResponse()
                //        .Error($"Folder {existsFolder} doesn't exists");
                //}
                //var existsRepoInPath = GitClientService.ExistsRepositoryInFolder(repositoryPath);
                //if (!existsRepoInPath)
                //{
                //    return new DeployActionUnitResponse()
                //        .Error($"Folder {existsFolder} doesn't contain git repository");
                //}
                //var currentBranch = GitClientService.GetCurrentBranchRepository(repositoryPath);
                //if (currentBranch != BranchDefinitions.Master)
                //{
                //    GitClientService.CheckoutBranch(repositoryPath, BranchDefinitions.Master);
                //}
                //return new DeployActionUnitResponse()
                //    .Ok(GetParameters(BranchDefinitions.Master), DeployActionUnitResponse.DeployActionResponseType.AlreadyCompletedJob);
                return null;
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
