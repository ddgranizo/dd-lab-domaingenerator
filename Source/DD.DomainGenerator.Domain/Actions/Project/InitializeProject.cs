using DD.DomainGenerator.Actions.Base;
using DD.DomainGenerator.DeployActions.Base;
using DD.DomainGenerator.DeployActions.Project;
using DD.DomainGenerator.Models;
using DD.DomainGenerator.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace DD.DomainGenerator.Actions.Project
{

    public class InitializeProject : ActionBase
    {
        public const string ActionName = "InitializeProject";

        public ActionParameterDefinition NameParameter { get; set; }
        public ActionParameterDefinition NamespaceParameter { get; set; }
        public ActionParameterDefinition ProjectPathParameter { get; set; }
        public IFileService FileService { get; }
        public IGithubClientService GithubClientService { get; }
        public IGitClientService GitClientService { get; }

        public InitializeProject(
            IFileService fileService,
            IGithubClientService githubClientService,
            IGitClientService gitClientService) : base(ActionName)
        {
            NameParameter = new ActionParameterDefinition(
               "name", ActionParameterDefinition.TypeValue.String, "Domain name. Must be unique. Is mandatory to use PascalCase for the name. Otherwise the name will be converterd", "n", string.Empty);
            NamespaceParameter = new ActionParameterDefinition(
                "namespace", ActionParameterDefinition.TypeValue.String, "Namespace. Is mandatory to use My.Domain.Project.Convention for your namespace. Otherwise the namespace will be converterd", "s", string.Empty);
            ProjectPathParameter = new ActionParameterDefinition(
                "projectpath", ActionParameterDefinition.TypeValue.String, "Path for locate the project files", "p", string.Empty);

            ActionParametersDefinition.Add(NameParameter);
            ActionParametersDefinition.Add(NamespaceParameter);
            ActionParametersDefinition.Add(ProjectPathParameter);
            FileService = fileService ?? throw new ArgumentNullException(nameof(fileService));
            GithubClientService = githubClientService ?? throw new ArgumentNullException(nameof(githubClientService));
            GitClientService = gitClientService ?? throw new ArgumentNullException(nameof(gitClientService));
        }

        public override bool CanExecute(ProjectState project, List<ActionParameter> parameters)
        {
            return IsParamOk(parameters, NameParameter)
                && IsParamOk(parameters, NamespaceParameter)
                && IsParamOk(parameters, ProjectPathParameter);
        }

        public override void Execute(ProjectState project, List<ActionParameter> parameters)
        {
            var name = GetStringParameterValue(parameters, NameParameter);
            var nameSpace = GetStringParameterValue(parameters, NamespaceParameter);
            var path = GetStringParameterValue(parameters, ProjectPathParameter);
            var absolutePath = FileService.GetAbsoluteCurrentPath(path);
            project.ProjectPath = absolutePath;
            project.Name = name;
            project.NameSpace = nameSpace;
        }

        public override List<DeployActionUnit> GetDeployActionUnits(ActionExecution actionExecution)
        {
            return new List<DeployActionUnit>()
            {
                new CreateDomainGithubRepository(actionExecution, GithubClientService),
                new CreateRepositoriesFolder(actionExecution, FileService),
                new CloneDomainGithubRepository(actionExecution, GitClientService, FileService),
            };
        }
    }
}
