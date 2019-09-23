using DD.DomainGenerator.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DD.DomainGenerator
{
    public class ProjectState
    {
        public string Name { get; set; }
        public List<ActionExecution> Actions { get; set; }
        public Domain Domain { get; set; }
        public string ReposPath { get; set; }
        public List<AzurePipelineSetting> AzurePipelineSettings { get; set; }
        public List<GithubSetting> GithubSettings { get; set; }
        public ArchitectureSetup Architecture { get; set; }
        public ProjectState()
        {
            Actions = new List<ActionExecution>();
            AzurePipelineSettings = new List<AzurePipelineSetting>();
            GithubSettings = new List<GithubSetting>();
        }


    }
}
