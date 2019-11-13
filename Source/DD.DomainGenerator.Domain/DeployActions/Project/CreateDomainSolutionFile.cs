using DD.DomainGenerator.DeployActions.Base;
using DD.DomainGenerator.Models;
using DD.DomainGenerator.Services;
using DD.DomainGenerator.Utilities;
using System;
using System.Collections.Generic;
using System.Text;
using static DD.DomainGenerator.Definitions;

namespace DD.DomainGenerator.DeployActions.Project
{

    public class CreateDomainSolutionFile : DeployActionUnit
    {

        public const string ActionName = "CreateDomainSolutionFile";
        public const string ActionDescription = "Create domain solution .sln file";

        public IDotnetService DotnetService { get; }
        public IFileService FileService { get; }

        public CreateDomainSolutionFile(
            ActionExecution actionExecution,
            IDotnetService dotnetService,
            IFileService fileService)
            : base(actionExecution, ActionName, ActionDescription, DeployManager.Phases.EmptyProject, Positions.Second, Positions.First)
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
                var createrepositoryFolderStructureDependency = GetDependencyFromSameSource<CreateDomainRepositoryFolderStructure>
                    (sourceActionExecution, currentExecutionDeployActions);
                var pathParameter = DeployResponseParametersDefinitions.MicroServices.CreateRepositoryFolderStructure.Source;
                var sourcePath = createrepositoryFolderStructureDependency.ResponseParameters[pathParameter] as string;

                var settingDotnet = GetSetting(projectState, Definitions.SettingsDefinitions.DotNetExePath);
                DotnetService.Initialize(settingDotnet);
                var projectName = $"{projectState.NameSpace}.{projectState.Name}.Domain";
                var solutionFile = GetSolutionFileName(projectName);
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

        private static string GetSolutionFileName(string composedNamespace)
        {
            var solutionName = composedNamespace.ToRepositoryNameFormat();
            return $"{solutionName}.sln";
        }

        public override DeployActionUnitResponse ExecuteDeploy(
            ProjectState projectState,
            ActionExecution sourceActionExecution,
            List<DeployActionUnit> currentExecutionDeployActions)
        {
            try
            {
                var createrepositoryFolderStructureDependency = GetDependencyFromSameSource<CreateDomainRepositoryFolderStructure>
                    (sourceActionExecution, currentExecutionDeployActions);
                var pathParameter = DeployResponseParametersDefinitions.MicroServices.CreateRepositoryFolderStructure.Source;
                var sourcePath = createrepositoryFolderStructureDependency.ResponseParameters[pathParameter] as string;

                var settingDotnet = GetSetting(projectState, Definitions.SettingsDefinitions.DotNetExePath);
                DotnetService.Initialize(settingDotnet);
                var projectName = $"{projectState.NameSpace}.{projectState.Name}.Domain";
                var solutionFileName = GetSolutionFileName(projectName);
                var solutionFilePath = FileService.ConcatDirectoryAndFileOrFolder(sourcePath, solutionFileName);

                var existsSolution = FileService.ExistsFile(solutionFilePath);
                if (!existsSolution)
                {
                    DotnetService.CreateSolutionFile(sourcePath, solutionFileName);
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
                {DeployResponseParametersDefinitions.Project.CreateDomainSolutionFile.SolutionFileName, solutionName },
                {DeployResponseParametersDefinitions.Project.CreateDomainSolutionFile.SolutionFilePath, solutionPath },
                {DeployResponseParametersDefinitions.Project.CreateDomainSolutionFile.Path, path },
            };
        }
    }
}
