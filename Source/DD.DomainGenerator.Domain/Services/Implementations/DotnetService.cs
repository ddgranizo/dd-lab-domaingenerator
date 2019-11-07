using System;
using System.Collections.Generic;
using System.Text;

namespace DD.DomainGenerator.Services.Implementations
{
    public class DotnetService : IDotnetService
    {
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


    }
}
