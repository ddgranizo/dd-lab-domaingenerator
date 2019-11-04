using DD.DomainGenerator.Actions.Base;
using DD.DomainGenerator.DeployActions;
using DD.DomainGenerator.GitHub;
using DD.DomainGenerator.GitHub.Extensions;
using DD.DomainGenerator.GitHub.Services;
using DD.DomainGenerator.Models;
using DD.DomainGenerator.Services;
using DD.DomainGenerator.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DD.DomainGenerator.Actions.MicroServices
{
    public class AddMicroService : ActionBase
    {
        public const string ActionName = "AddMicroService";
        public ActionParameterDefinition NameParameter { get; set; }
        public IFileService FileService { get; }
        public IGithubClientService GithubClientService { get; }

        public AddMicroService(IFileService fileService, IGithubClientService githubClientService) : base(ActionName)
        {
            NameParameter = new ActionParameterDefinition(
                Definitions.ActionsParametersDefinitions.AddMicroService.Name,
                ActionParameterDefinition.TypeValue.String, "Micro service name. Must be unique. Is mandatory to use PascalCase for the name. Otherwise the name will be converterd", "n", string.Empty);

            ActionParametersDefinition.Add(NameParameter);
            FileService = fileService ?? throw new ArgumentNullException(nameof(fileService));
            GithubClientService = githubClientService ?? throw new ArgumentNullException(nameof(githubClientService));
        }

        public override bool CanExecute(ProjectState project, List<ActionParameter> parameters)
        {
            return IsParamOk(parameters, NameParameter);
        }

        public override void ExecuteStateChange(ProjectState project, List<ActionParameter> parameters)
        {
            string name = GetServiceName(parameters);
            bool isRepeated = project.MicroServices.FirstOrDefault(k => k.Name == name) != null;
            if (isRepeated)
            {
                throw new Exception("Found repeated microservice name. Microservice name must be unique");
            }
            MicroService microService = new MicroService(name);
            project.MicroServices.Add(microService);
        }

        private string GetServiceName(List<ActionParameter> parameters)
        {
            return GetStringParameterValue(parameters, NameParameter).ToWordPascalCase();
        }


        public override List<DeployActionUnit> GetDeployActionUnits(ActionExecution actionExecution)
        {
            return new List<DeployActionUnit>()
            {
                new CreateGithubRepositoryFromMicroService(actionExecution, GithubClientService),
                new CreateRepositoryFolderFromMicroService(actionExecution, FileService),
            };
        }

    }
}
