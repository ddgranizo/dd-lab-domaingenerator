using DD.DomainGenerator.Actions.Base;
using DD.DomainGenerator.DeployActions;
using DD.DomainGenerator.DeployActions.Base;
using DD.DomainGenerator.DeployActions.Microservices;
using DD.DomainGenerator.Models;
using DD.DomainGenerator.Services;
using DD.DomainGenerator.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DD.DomainGenerator.Actions.Microservices
{
    public class AddMicroService : ActionBase
    {
        public const string ActionName = "AddMicroService";
        public ActionParameterDefinition NameParameter { get; set; }
        public IFileService FileService { get; }
        public IGithubClientService GithubClientService { get; }
        public IGitClientService GitClientService { get; }
        public IDotnetService DotnetService { get; }

        public AddMicroService(
            IFileService fileService,
            IGithubClientService githubClientService,
            IGitClientService gitClientService,
            IDotnetService dotnetService) : base(ActionName)
        {
            NameParameter = new ActionParameterDefinition(
                Definitions.ActionsParametersDefinitions.AddMicroService.Name,
                ActionParameterDefinition.TypeValue.String, "Micro service name. Must be unique. Is mandatory to use PascalCase for the name. Otherwise the name will be converterd", "n", string.Empty);

            ActionParametersDefinition.Add(NameParameter);
            FileService = fileService ?? throw new ArgumentNullException(nameof(fileService));
            GithubClientService = githubClientService ?? throw new ArgumentNullException(nameof(githubClientService));
            GitClientService = gitClientService ?? throw new ArgumentNullException(nameof(gitClientService));
            DotnetService = dotnetService ?? throw new ArgumentNullException(nameof(dotnetService));
        }

        public override bool CanExecute(ProjectState project, List<ActionParameter> parameters)
        {
            return IsParamOk(parameters, NameParameter);
        }

        public override void Execute(ProjectState project, List<ActionParameter> parameters)
        {
            string name = GetServiceName(parameters);
            OverrideOutputParameter(NameParameter.Name, name);
            bool isRepeated = project.Microservices.FirstOrDefault(k => k.Name == name) != null;
            if (isRepeated)
            {
                throw new Exception("Found repeated microservice name. Microservice name must be unique");
            }
            MicroService microService = new MicroService(name);
            project.Microservices.Add(microService);
            OverrideOutputParameter(NameParameter.Name, name);

        }

        private string GetServiceName(List<ActionParameter> parameters)
        {
            return GetStringParameterValue(parameters, NameParameter).ToWordPascalCase();
        }


        public override List<DeployActionUnit> GetDeployActionUnits(ActionExecution actionExecution)
        {
            return new List<DeployActionUnit>()
            {
                new CreateGithubRepository(actionExecution, GithubClientService),
                new CreateRepositoriesFolder(actionExecution, FileService),
                new CloneGitRepository(actionExecution, GitClientService, FileService),
                new CheckOutMasterRepository(actionExecution, GitClientService, FileService),
                new CleanRepositoryFolder(actionExecution, FileService),
                new CreateRepositoryFolderStructure(actionExecution, FileService),
                new CreateSolutionFile(actionExecution, DotnetService, FileService),
            };
        }

    }
}
