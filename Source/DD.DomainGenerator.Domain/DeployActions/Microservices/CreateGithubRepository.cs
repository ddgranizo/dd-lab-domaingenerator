using DD.DomainGenerator.DeployActions.Base;
using DD.DomainGenerator.Extensions;
using DD.DomainGenerator.Models;
using DD.DomainGenerator.Services;
using DD.DomainGenerator.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DD.DomainGenerator.DeployActions.Microservices
{
    public class CreateGithubRepository : DeployActionUnit
    {
        public const string ActionName = "CreateGithubRepository";
        public const string ActionDescription = "Create Github microservice repository";

        public IGithubClientService GithubClientService { get; }

        public CreateGithubRepository(
            ActionExecution actionExecution,
            IGithubClientService githubClientService)
            : base(actionExecution, ActionName, ActionDescription, DeployManager.Phases.AvailableInfrastructure, Positions.First, Positions.First)
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
                var microServiceName = ActionExecution.InputParameters[Definitions.ActionsParametersDefinitions.AddMicroService.Name] as string;
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
                var microServiceName = ActionExecution.InputParameters[Definitions.ActionsParametersDefinitions.AddMicroService.Name] as string;
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

    }
}
