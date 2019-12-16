using DD.Lab.Services.System.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace DD.Lab.Services.System.Implementations
{
    public class GitClientService : IGitClientService
    {
        public string GitPath { get; set; }

        public GitClientService(IProcessService processService)
        {
            ProcessService = processService ?? throw new ArgumentNullException(nameof(processService));
        }

        public IProcessService ProcessService { get; }

        public void CloneRepository(string localPath, string gitRepositoryUrl)
        {
            CheckIfInitialized();
            var command = $"clone {gitRepositoryUrl}";
            var asd = ProcessService.RunCommand(command, GitPath, localPath);
        }

        public void CommitAllRepository(string localPath, string commitMessage)
        {
            CheckIfInitialized();
            var command = $"commit -A -m \"{commitMessage}\"";
            ProcessService.RunCommand(command, GitPath, localPath);
        }

        public string GetCurrentBranchRepository(string localPath)
        {
            CheckIfInitialized();
            var command = $"symbolic-ref --short HEAD";
            return ProcessService.RunCommand(command, GitPath, localPath);
        }

        public void Initialize(string gitPath)
        {
            if (string.IsNullOrEmpty(gitPath))
            {
                throw new ArgumentException("message", nameof(gitPath));
            }
            GitPath = gitPath;
        }

        public void PullRepository(string localPath)
        {
            CheckIfInitialized();
            var command = $"pull";
            ProcessService.RunCommand(command, GitPath, localPath);
        }

        private void CheckIfInitialized()
        {
            if (string.IsNullOrEmpty(GitPath))
            {
                throw new Exception("Initialize first GitClient with the path to the git.exe in your local computer");
            }
        }

        public void PushRepository(string localPath)
        {
            CheckIfInitialized();
            var command = $"push";
            ProcessService.RunCommand(command, GitPath, localPath);
        }

        public void CheckoutBranch(string localPath, string branchName)
        {
            CheckIfInitialized();
            var command = $"checkout {branchName}";
            ProcessService.RunCommand(command, GitPath, localPath);
        }

        public bool ExistsRepositoryInFolder(string localPath)
        {
            CheckIfInitialized();
            var command = $"rev-parse --is-inside-work-tree";
            var response = ProcessService.RunCommand(command, GitPath, localPath);
            return response == "true";
        }
    }
}
