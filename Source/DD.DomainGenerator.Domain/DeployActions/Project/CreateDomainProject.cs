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
    

    public class CreateDomainProject : DeployActionUnit
    {

        public const string ActionName = "CreateDomainProject";
        public const string ActionDescription = "Create domain project";

        public IDDService DDService { get; }
        public IFileService FileService { get; }

        public CreateDomainProject(
            ActionExecution actionExecution,
            IDDService dDService,
            IFileService fileService)
            : base(actionExecution, ActionName, ActionDescription, DeployManager.Phases.EmptyProject, Positions.Second, Positions.Third)
        {
            DDService = dDService ?? throw new ArgumentNullException(nameof(dDService));
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
                var createProjectFolder = GetDependencyFromSameSource<CreateDomainProjectFolder>
                    (sourceActionExecution, currentExecutionDeployActions);
                var pathParameter = DeployResponseParametersDefinitions.Project.CreateDomainProjectFolder.Path;
                var projectPath = createProjectFolder.ResponseParameters[pathParameter] as string;

                

                var settingDD = GetSetting(projectState, SettingsDefinitions.DDCliExePath);
                var settingDomainTemplate = GetSetting(projectState, SettingsDefinitions.DDCliDomainProjectTemplatePath);
                var projectFileName = $"{projectState.NameSpace}.{projectState.Name}.Domain.csproj";
                var completeProjectPath = FileService.ConcatDirectoryAndFileOrFolder(projectPath, projectFileName);
                DDService.Initialize(settingDD);
                DDService.Template(projectPath, settingDomainTemplate, projectState.NameSpace, projectState.Name);
                
                return new DeployActionUnitResponse()
                            .Ok(GetParameters(projectPath, projectFileName, completeProjectPath), DeployActionUnitResponse.DeployActionResponseType.AlreadyCompletedJob);
            }
            catch (Exception ex)
            {
                return new DeployActionUnitResponse()
                    .Error(ex);
            }
        }


        private static Dictionary<string, object> GetParameters(string path, string projectName, string projectPath)
        {
            return new Dictionary<string, object>()
            {
                {DeployResponseParametersDefinitions.Project.CreateDomainProject.ProjectFileName, projectName },
                {DeployResponseParametersDefinitions.Project.CreateDomainProject.ProjectFilePath, projectPath },
                {DeployResponseParametersDefinitions.Project.CreateDomainProject.Path, path },
            };
        }
    }
}
