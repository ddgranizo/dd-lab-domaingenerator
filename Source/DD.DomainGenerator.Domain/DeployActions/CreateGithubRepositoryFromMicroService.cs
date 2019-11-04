using DD.DomainGenerator.GitHub;
using DD.DomainGenerator.GitHub.Extensions;
using DD.DomainGenerator.GitHub.Services;
using DD.DomainGenerator.Models;
using DD.DomainGenerator.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DD.DomainGenerator.DeployActions
{
    public class CreateGithubRepositoryFromMicroService : DeployActionUnit
    {
        public const string ActionName = "CreateGithubRepositoryFromMicroService";
        public const string ActionDescription = "Create Github microservice repository";

        public IGithubClientService GithubClientService { get; }

        public CreateGithubRepositoryFromMicroService(
            ActionExecution actionExecution,
            IGithubClientService githubClientService)
            : base(actionExecution, ActionName, ActionDescription, DeployManager.Phases.EmptyProject)
        {
            GithubClientService = githubClientService ?? throw new ArgumentNullException(nameof(githubClientService));
        }

        public override DeployActionUnitResponse ExecuteCheck(
            ProjectState projectState,
            ActionExecution sourceActionExecution,
            List<DeployActionUnit> currentExecutionDeployActions)
        {
            try
            {
                var microServiceName = ActionExecution.Parameters[Definitions.ActionsParametersDefinitions.AddMicroService.Name] as string;
                var githubSetting = GetCurrentGithubSetting(projectState);
                GithubClientService.InitializeClientWithToken(githubSetting.OauthToken);
                var completeName = GetRepositoryName(projectState, microServiceName);
                var repository = GithubClientService.SearchRepository(completeName);
                if (repository != null)
                {
                    return new DeployActionUnitResponse()
                        .Ok(repository.ToDictionary(), DeployActionUnitResponse.DeployActionResponseType.AlreadyCompletedJob);
                }
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
                var microServiceName = ActionExecution.Parameters[Definitions.ActionsParametersDefinitions.AddMicroService.Name] as string;
                GithubSetting githubSetting = GetCurrentGithubSetting(projectState);
                GithubClientService.InitializeClientWithToken(githubSetting.OauthToken);
                var completeName = GetRepositoryName(projectState, microServiceName);
                var repository = GithubClientService.CreateRepository(completeName);
                return new DeployActionUnitResponse()
                    .Ok(repository.ToDictionary());
            }
            catch (Exception ex)
            {
                return new DeployActionUnitResponse()
                    .Error(ex);
            }
        }

        private static string GetRepositoryName(ProjectState state, string microserviceName)
        {
            return string.Format("{0}.{1}.{2}", state.NameSpace, state.Name, microserviceName)
                                                    .ToRepositoryNameFormat();
        }

        private static GithubSetting GetCurrentGithubSetting(ProjectState state)
        {
            return state.GithubSettings.First();
        }

        private static string GetMicroserviceName(ActionExecution action, string parameterKeyName)
        {
            return ((string)action.Parameters[parameterKeyName]).ToNamespacePascalCase();
        }
    }
}
