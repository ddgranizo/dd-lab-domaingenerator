using Octokit;
using System;
using System.Collections.Generic;
using System.Text;

namespace DD.DomainGenerator.GitHub.Extensions
{
    public static class RepositoryExtensions
    {
        public static Dictionary<string, object> ToDictionary(this Repository repository)
        {
            Dictionary<string, object> values = new Dictionary<string, object>();

            values[Definitions.DeployResponseParametersDefinitions.CreateGithubRepositoryFromMicroService.CloneUrl] = repository.CloneUrl;
            values[Definitions.DeployResponseParametersDefinitions.CreateGithubRepositoryFromMicroService.CreatedAt] = repository.CreatedAt;
            values[Definitions.DeployResponseParametersDefinitions.CreateGithubRepositoryFromMicroService.FullName] = repository.FullName;
            values[Definitions.DeployResponseParametersDefinitions.CreateGithubRepositoryFromMicroService.GitUrl] = repository.GitUrl;
            values[Definitions.DeployResponseParametersDefinitions.CreateGithubRepositoryFromMicroService.HtmlUrl] = repository.HtmlUrl;
            values[Definitions.DeployResponseParametersDefinitions.CreateGithubRepositoryFromMicroService.Name] = repository.Name;
            values[Definitions.DeployResponseParametersDefinitions.CreateGithubRepositoryFromMicroService.OwnerId] = repository.Owner.Id;
            values[Definitions.DeployResponseParametersDefinitions.CreateGithubRepositoryFromMicroService.OwnerLogin] = repository.Owner.Login;
            values[Definitions.DeployResponseParametersDefinitions.CreateGithubRepositoryFromMicroService.Private] = repository.Private;
            values[Definitions.DeployResponseParametersDefinitions.CreateGithubRepositoryFromMicroService.SshUrl] = repository.SshUrl;
            values[Definitions.DeployResponseParametersDefinitions.CreateGithubRepositoryFromMicroService.SvnUrl] = repository.SvnUrl;
            values[Definitions.DeployResponseParametersDefinitions.CreateGithubRepositoryFromMicroService.Url] = repository.Url;
            
            return values;

        }
    }
}
