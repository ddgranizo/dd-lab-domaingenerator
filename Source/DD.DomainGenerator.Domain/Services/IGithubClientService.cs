using Octokit;
using System;
using System.Collections.Generic;
using System.Text;

namespace DD.DomainGenerator.Services
{
    public interface IGithubClientService
    {
        void InitializeClientWithToken(string token);
        Repository SearchRepository(string repoName);
        Repository CreateRepository(string repoName);
    }
}
