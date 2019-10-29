using DD.DomainGenerator.Actions.Base;
using DD.DomainGenerator.GitHub;
using DD.DomainGenerator.GitHub.Extensions;
using DD.DomainGenerator.Models;
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

        public AddMicroService() : base(ActionName)
        {
            NameParameter = new ActionParameterDefinition(
                "name", ActionParameterDefinition.TypeValue.String, "Micro service name. Must be unique. Is mandatory to use PascalCase for the name. Otherwise the name will be converterd", "n", string.Empty);

            ActionParametersDefinition.Add(NameParameter);

            RegisterDeployActions();
        }

        public override bool CanExecute(ProjectState project, List<ActionParameter> parameters)
        {
            return IsParamOk(parameters, NameParameter);
        }

        public override void ExecuteStateChange(ProjectState project, List<ActionParameter> parameters)
        {
            var name = GetStringParameterValue(parameters, NameParameter).ToWordPascalCase();
            bool isRepeated = project.MicroServices.FirstOrDefault(k => k.Name == name) != null;
            if (isRepeated)
            {
                throw new Exception("Found repeated microservice name. Microservice name must be unique");
            }
            MicroService microService = new MicroService(name);
            project.MicroServices.Add(microService);
        }

        private void RegisterDeployActions()
        {
            var actionCreateRepo = new DeployActionUnit(
                "GenerateMicroserviceGithubRepository",
                DeployManager.Phases.EmptyProject, 1, 1,
                "Create Github microservice repository",
                (ActionExecution action, ProjectState state) =>
                {
                    var microserviceName = ((string)action.Parameters[NameParameter.Name]).ToNamespacePascalCase();
                    var githubSetting = state.GithubSettings.First();
                    var githubManager = new GithubManager(githubSetting.OauthToken);
                    var microService = state.MicroServices.First(k => k.Name == microserviceName);
                    var completeName = string.Format("{0}.{1}.{2}", state.NameSpace, state.Name, microserviceName)
                                        .ToRepositoryNameFormat();
                    var repository = githubManager.CreateNewRepository(completeName);
                    return new DeployActionUnitResponse().Ok(repository.ToDictionary());
                });

            RegisterDeployActionUnit(actionCreateRepo);
        }
    }
}
