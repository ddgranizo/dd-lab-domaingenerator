using DD.Lab.Services.System.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace DD.Lab.Services.System.Implementations
{
    public class DotnetService : IDotnetService
    {
        private const string SolutionExtension = ".sln";

        public DotnetService(IProcessService processService)
        {
            ProcessService = processService ?? throw new ArgumentNullException(nameof(processService));
        }
        public string DotnetPath { get; set; }
        public IProcessService ProcessService { get; }

        public void Initialize(string dotnetPath)
        {
            if (string.IsNullOrEmpty(dotnetPath))
            {
                throw new ArgumentException("message", nameof(dotnetPath));
            }
            DotnetPath = dotnetPath;
        }

        public void CreateSolutionFile(string path, string solutionName)
        {
            CheckIfInitialized();
            if (solutionName.Length > SolutionExtension.Length 
                && solutionName.ToLowerInvariant().EndsWith(SolutionExtension))
            {
                solutionName = solutionName.Substring(0, solutionName.Length - SolutionExtension.Length);
            }
            var command = $"new sln -n {solutionName}";
            ProcessService.RunCommand(command, DotnetPath, path);
        }

        private void CheckIfInitialized()
        {
            if (string.IsNullOrEmpty(DotnetPath))
            {
                throw new Exception("Initialize first DotNetExePath setting with the path to the dotnet.exe in your local computer");
            }
        }

        public void AddProjectToSolutionFile(string solutionDirectoryPath, string solutionFile, string relativePathToProject)
        {
            var command = $"sln {solutionFile} add {relativePathToProject}"; //Test with `characters` if has spaces
            ProcessService.RunCommand(command, DotnetPath, solutionDirectoryPath);
        }
    }
}
