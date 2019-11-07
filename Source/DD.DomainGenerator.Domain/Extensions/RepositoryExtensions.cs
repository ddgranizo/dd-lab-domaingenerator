using Octokit;
using System;
using System.Collections.Generic;
using System.Text;

namespace DD.DomainGenerator.Extensions
{
    public static class RepositoryExtensions
    {
        public static Dictionary<string, object> ToDictionary(this Repository repository)
        {
            Dictionary<string, object> values = new Dictionary<string, object>();

            values[Definitions.DeployResponseParametersDefinitions.MicroServices.CreateGithubRepository.CloneUrl] = repository.CloneUrl;
            values[Definitions.DeployResponseParametersDefinitions.MicroServices.CreateGithubRepository.CreatedAt] = repository.CreatedAt;
            values[Definitions.DeployResponseParametersDefinitions.MicroServices.CreateGithubRepository.FullName] = repository.FullName;
            values[Definitions.DeployResponseParametersDefinitions.MicroServices.CreateGithubRepository.GitUrl] = repository.GitUrl;
            values[Definitions.DeployResponseParametersDefinitions.MicroServices.CreateGithubRepository.HtmlUrl] = repository.HtmlUrl;
            values[Definitions.DeployResponseParametersDefinitions.MicroServices.CreateGithubRepository.Name] = repository.Name;
            values[Definitions.DeployResponseParametersDefinitions.MicroServices.CreateGithubRepository.OwnerId] = repository.Owner.Id;
            values[Definitions.DeployResponseParametersDefinitions.MicroServices.CreateGithubRepository.OwnerLogin] = repository.Owner.Login;
            values[Definitions.DeployResponseParametersDefinitions.MicroServices.CreateGithubRepository.Private] = repository.Private;
            values[Definitions.DeployResponseParametersDefinitions.MicroServices.CreateGithubRepository.SshUrl] = repository.SshUrl;
            values[Definitions.DeployResponseParametersDefinitions.MicroServices.CreateGithubRepository.SvnUrl] = repository.SvnUrl;
            values[Definitions.DeployResponseParametersDefinitions.MicroServices.CreateGithubRepository.Url] = repository.Url;

            return values;

        }
    }
}