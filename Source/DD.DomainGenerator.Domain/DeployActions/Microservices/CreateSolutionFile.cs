using DD.DomainGenerator.DeployActions.Base;
using DD.DomainGenerator.Models;
using DD.DomainGenerator.Services;
using System;
using System.Collections.Generic;
using System.Text;
using static DD.DomainGenerator.Definitions;

namespace DD.DomainGenerator.DeployActions.Microservices
{

    public class CreateSolutionFile : DeployActionUnit
    {

        public const string ActionName = "CreateSolutionFile";
        public const string ActionDescription = "Create solution .sln file";

        public IDotnetService DotnetService { get; }
        public IFileService FileService { get; }

        public CreateSolutionFile(
            ActionExecution actionExecution,
            IDotnetService dotnetService,
            IFileService fileService)
            : base(actionExecution, ActionName, ActionDescription, DeployManager.Phases.AvailableInfrastructure, Positions.Second, Positions.First)
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
                var createrepositoryFolderStructureDependency = GetDependencyFromSameSource<CreateRepositoryFolderStructure>
                    (sourceActionExecution, currentExecutionDeployActions);
                var pathParameter = DeployResponseParametersDefinitions.MicroServices.CreateRepositoryFolderStructure.Source;
                var sourcePath = createrepositoryFolderStructureDependency.ResponseParameters[pathParameter] as string;

                var githubDependency = GetDependencyFromSameSource<CreateGithubRepository>
                    (sourceActionExecution, currentExecutionDeployActions);
                var repoNameParameter = DeployResponseParametersDefinitions.MicroServices.CreateGithubRepository.Name;
                var repoName = githubDependency.ResponseParameters[repoNameParameter] as string;

                var settingDotnet = GetSetting(projectState, Definitions.SettingsDefinitions.DotNetExePath);
                DotnetService.Initialize(settingDotnet);
                var solutionFile = GetSolutionFileName(repoName);
                var solutionFilePath = FileService.ConcatDirectoryAndFileOrFolder(sourcePath, solutionFile);

                var existsSolution = FileService.ExistsFile(solutionFilePath);
                if (!existsSolution)
                {
                    return new DeployActionUnitResponse()
                        .Ok(DeployActionUnitResponse.DeployActionResponseType.NotCompletedJob);
                }
                return new DeployActionUnitResponse()
                        .Ok(GetParameters(sourcePath, solutionFile, solutionFilePath), DeployActionUnitResponse.DeployActionResponseType.AlreadyCompletedJob);
            }
            catch (Exception ex)
            {
                return new DeployActionUnitResponse()
                    .Error(ex);
            }
        }

        private static string GetSolutionFileName(string repoName)
        {
            return $"{repoName}.sln";
        }

        public override DeployActionUnitResponse ExecuteDeploy(
            ProjectState projectState,
            ActionExecution sourceActionExecution,
            List<DeployActionUnit> currentExecutionDeployActions)
        {
            try
            {
                var createrepositoryFolderStructureDependency = GetDependencyFromSameSource<CreateRepositoryFolderStructure>
                    (sourceActionExecution, currentExecutionDeployActions);
                var pathParameter = DeployResponseParametersDefinitions.MicroServices.CreateRepositoryFolderStructure.Source;
                var sourcePath = createrepositoryFolderStructureDependency.ResponseParameters[pathParameter] as string;

                var githubDependency = GetDependencyFromSameSource<CreateGithubRepository>
                    (sourceActionExecution, currentExecutionDeployActions);
                var repoNameParameter = DeployResponseParametersDefinitions.MicroServices.CreateGithubRepository.Name;
                var repoName = githubDependency.ResponseParameters[repoNameParameter] as string;

                var settingDotnet = GetSetting(projectState, Definitions.SettingsDefinitions.DotNetExePath);
                DotnetService.Initialize(settingDotnet);
                var solutionFileName = GetSolutionFileName(repoName);
                var solutionFilePath = FileService.ConcatDirectoryAndFileOrFolder(sourcePath, solutionFileName);

                var existsSolution = FileService.ExistsFile(solutionFilePath);
                if (!existsSolution)
                {
                    DotnetService.CreateSolutionFile(sourcePath, repoName);
                }
                return new DeployActionUnitResponse()
                        .Ok(GetParameters(sourcePath, solutionFileName, solutionFilePath));
            }
            catch (Exception ex)
            {
                return new DeployActionUnitResponse()
                    .Error(ex);
            }
        }


        private static Dictionary<string, object> GetParameters(string path, string solutionName, string solutionPath)
        {
            return new Dictionary<string, object>()
            {
                {DeployResponseParametersDefinitions.MicroServices.CreateSolutionFile.SolutionFileName, solutionName },
                {DeployResponseParametersDefinitions.MicroServices.CreateSolutionFile.SolutionFilePath, solutionPath },
                {DeployResponseParametersDefinitions.MicroServices.CreateSolutionFile.Path, path },
            };
        }
    }
}
