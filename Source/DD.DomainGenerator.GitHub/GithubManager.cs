using Octokit;
using System;
using System.Threading.Tasks;

namespace DD.DomainGenerator.GitHub
{
    public class GithubManager
    {
        public Credentials CurrentCredentials { get; set; }
        public GitHubClient Client { get; set; }
        public string Token { get; }

        public GithubManager(string token)
        {
            if (string.IsNullOrEmpty(token))
            {
                throw new ArgumentException("message", nameof(token));
            }

            Token = token;
            CurrentCredentials = new Credentials(Token);
        }


        public Repository CreateNewRepository(string repoName)
        {
            var github = new GitHubClient(new ProductHeaderValue("DomainGenerator"))
            {
                Credentials = CurrentCredentials
            };

            var createdRepository = github.Repository.Create(new NewRepository(repoName)
            {
                Private = true
            });
            var response = createdRepository.Result;
            return response;
        }

    }
}
