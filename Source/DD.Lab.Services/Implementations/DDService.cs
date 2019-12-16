using DD.Lab.Services.System.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace DD.Lab.Services.System.Implementations
{
    

    public class DDService : IDDService
    {
        public string DDPath { get; set; }

        public DDService(IProcessService processService)
        {
            ProcessService = processService ?? throw new ArgumentNullException(nameof(processService));
        }

        public IProcessService ProcessService { get; }

        public void CloneRepository(string localPath, string gitRepositoryUrl)
        {
            CheckIfInitialized();
            var command = $"clone {gitRepositoryUrl}";
            var asd = ProcessService.RunCommand(command, DDPath, localPath);
        }

        public void Initialize(string ddPath)
        {
            if (string.IsNullOrEmpty(ddPath))
            {
                throw new ArgumentException("message", nameof(ddPath));
            }
            DDPath = ddPath;
        }

        private void CheckIfInitialized()
        {
            if (string.IsNullOrEmpty(DDPath))
            {
                throw new Exception("Initialize first DDClient with the path to the dd.exe in your local computer");
            }
        }
     
        public void Template(string localPath, string templatePath, string @namespace, string appName)
        {
            CheckIfInitialized();
            var namespaceKey = Definitions.ProjectTemplates.DomainProject.TemplateParameters.Namespace;
            var appKey = Definitions.ProjectTemplates.DomainProject.TemplateParameters.App;
            var command = $"template -p \"{templatePath}\" -d \"{localPath}\" -v \"{namespaceKey}={@namespace};{appKey}={appName}\"";
            ProcessService.RunCommand(command, DDPath);
        }
    }
}
