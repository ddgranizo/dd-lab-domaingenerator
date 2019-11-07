using Octokit;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace DD.DomainGenerator
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
            Client = new GitHubClient(new ProductHeaderValue("DomainGenerator"))
            {
                Credentials = CurrentCredentials
            };

        }


        public Repository SearchRepository(string repoName)
        {
            var createdRepository = Client.Search.SearchRepo(new SearchRepositoriesRequest(repoName));
            var response = createdRepository.Result;
            var repo = response.Items.OfType<Repository>().FirstOrDefault();
            return repo;
        }


        public Repository CreateRepository(string repoName)
        {
            var createdRepository = Client.Repository.Create(new NewRepository(repoName)
            {
                Private = true
            });
            var response = createdRepository.Result;
            return response;
        }

    }
}
