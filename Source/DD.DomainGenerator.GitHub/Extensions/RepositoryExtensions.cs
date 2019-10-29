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

            values[nameof(repository.CloneUrl)] = repository.CloneUrl;
            values[nameof(repository.CreatedAt)] = repository.CreatedAt;
            values[nameof(repository.FullName)] = repository.FullName;
            values[nameof(repository.GitUrl)] = repository.GitUrl;
            values[nameof(repository.HtmlUrl)] = repository.HtmlUrl;
            values[nameof(repository.Name)] = repository.Name;
            values[$"{nameof(repository.Owner)}.Id"] = repository.Owner.Id;
            values[$"{nameof(repository.Owner)}.Login"] = repository.Owner.Login;
            values[nameof(repository.Private)] = repository.Private;
            values[nameof(repository.SshUrl)] = repository.SshUrl;
            values[nameof(repository.SvnUrl)] = repository.SvnUrl;
            values[nameof(repository.UpdatedAt)] = repository.UpdatedAt;
            values[nameof(repository.Url)] = repository.Url;
            
            return values;

        }
    }
}
