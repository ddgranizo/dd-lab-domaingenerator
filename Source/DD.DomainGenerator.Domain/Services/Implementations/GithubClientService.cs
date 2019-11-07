using System;
using System.Collections.Generic;
using System.Text;
using Octokit;

namespace DD.DomainGenerator.Services.Implementations
{
    public class GithubClientService : IGithubClientService
    {
        public GithubManager Manager { get; set; }
        public GithubClientService()
        {
            
        }

        public void InitializeClientWithToken(string token)
        {
            Manager = new GithubManager(token);
        }

        public Repository CreateRepository(string repoName)
        {
            CheckIfInitialized();
            return Manager.CreateRepository(repoName);
        }

        public Repository SearchRepository(string repoName)
        {
            CheckIfInitialized();
            return Manager.SearchRepository(repoName);
        }

        private void CheckIfInitialized()
        {
            if (Manager == null)
            {
                throw new Exception("Initialize first a client with a token");
            }
        }
    }
}
