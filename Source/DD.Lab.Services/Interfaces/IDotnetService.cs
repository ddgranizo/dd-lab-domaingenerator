using System;
using System.Collections.Generic;
using System.Text;

namespace DD.Lab.Services.System.Interfaces
{
    public interface IDotnetService
    {
        void Initialize(string dotnetPath);
        void CreateSolutionFile(string path, string solutionName);
        void AddProjectToSolutionFile(string solutionDirectoryPath, string solutionFile, string relativePathToProject);
    }
}
