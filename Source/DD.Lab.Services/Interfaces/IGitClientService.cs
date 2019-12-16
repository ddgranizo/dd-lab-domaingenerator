using System;
using System.Collections.Generic;
using System.Text;

namespace DD.Lab.Services.System.Interfaces
{
    public interface IGitClientService
    {
        void Initialize(string gitPath);
        void CloneRepository(string localPath, string gitRepositoryUrl);
        void PushRepository(string localPath);
        void CheckoutBranch(string localPath, string branchName);
        void PullRepository(string localPath);
        void CommitAllRepository(string localPath, string commitMessage);
        string GetCurrentBranchRepository(string localPath);
        bool ExistsRepositoryInFolder(string localPath);
    }
}
