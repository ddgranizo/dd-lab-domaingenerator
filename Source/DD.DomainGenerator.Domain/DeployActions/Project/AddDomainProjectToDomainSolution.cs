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
    

    public class AddDomainProjectToDomainSolution : DeployActionUnit
    {

        public const string ActionName = "AddDomainProjectToDomainSolution";
        public const string ActionDescription = "ADd domain project to domain solution";

        public IDotnetService DotnetService { get; }
        public IFileService FileService { get; }

        public AddDomainProjectToDomainSolution(
            ActionExecution actionExecution,
            IDotnetService dotnetService,
            IFileService fileService)
            : base(actionExecution, ActionName, ActionDescription, DeployManager.Phases.EmptyProject, Positions.Second, Positions.Forth)
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
                return new DeployActionUnitResponse()
                            .Ok(DeployActionUnitResponse.DeployActionResponseType.NotCompletedJob);
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
                var createDomainProject = GetDependencyFromSameSource<CreateDomainProject>
                    (sourceActionExecution, currentExecutionDeployActions);
                var createDomainSolution = GetDependencyFromSameSource<CreateDomainSolutionFile>
                    (sourceActionExecution, currentExecutionDeployActions);

                var domainSolutionFolderParameter = DeployResponseParametersDefinitions.Project.CreateDomainSolutionFile.Path;
                var domainSolutionNameParameter = DeployResponseParametersDefinitions.Project.CreateDomainSolutionFile.SolutionFileName;
                var domainSolutionFolder = createDomainSolution.ResponseParameters[domainSolutionFolderParameter] as string;
                var domainSolutionName = createDomainSolution.ResponseParameters[domainSolutionNameParameter] as string;

                var projectPathParameter = DeployResponseParametersDefinitions.Project.CreateDomainProject.ProjectFilePath;
                var projectPath = createDomainProject.ResponseParameters[projectPathParameter] as string;

                var relativePath = projectPath.Replace(domainSolutionFolder, "").Substring(1);

                DotnetService.AddProjectToSolutionFile(domainSolutionFolder, domainSolutionName, relativePath);
                return new DeployActionUnitResponse()
                            .Ok(GetParameters(domainSolutionFolder, domainSolutionName, relativePath), DeployActionUnitResponse.DeployActionResponseType.AlreadyCompletedJob);
            }
            catch (Exception ex)
            {
                return new DeployActionUnitResponse()
                    .Error(ex);
            }
        }


        private static Dictionary<string, object> GetParameters(string domainSolutionFolder, string domainSolutionName, string relativePath)
        {
            return new Dictionary<string, object>()
            {
                {DeployResponseParametersDefinitions.Project.AddDomainProjectToDomainSolution.DomainSolutionFolder, domainSolutionFolder },
                {DeployResponseParametersDefinitions.Project.AddDomainProjectToDomainSolution.DomainSolutionName, domainSolutionName },
                {DeployResponseParametersDefinitions.Project.AddDomainProjectToDomainSolution.RelativePath, relativePath },
            };
        }
    }
}
