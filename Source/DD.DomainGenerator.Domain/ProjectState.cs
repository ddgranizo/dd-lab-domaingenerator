using DD.DomainGenerator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DD.DomainGenerator
{
    public class ProjectState
    {
        public string Name { get; set; }
        public List<ActionExecution> Actions { get; set; }
        public List<Domain> Domains { get; set; }
        public List<SchemaModel> Schemas { get; set; }
        public List<SchemaInDomain> SchemaInDomains { get; set; }
        public string ReposPath { get; set; }
        public List<AzurePipelineSetting> AzurePipelineSettings { get; set; }
        public List<GithubSetting> GithubSettings { get; set; }
        public ArchitectureSetup Architecture { get; set; }
        public ProjectState()
        {
            SchemaInDomains = new List<SchemaInDomain>();
            Domains = new List<Domain>();
            Actions = new List<ActionExecution>();
            AzurePipelineSettings = new List<AzurePipelineSetting>();
            GithubSettings = new List<GithubSetting>();
            Schemas = new List<SchemaModel>();
        }


        public List<SchemaModel> GetAllSchemas()
        {
            return Schemas;
        }

        public List<Domain> GetAllDomains()
        {
            return Domains;
        }

        public SchemaModel GetSchema(string name)
        {
            return Schemas.FirstOrDefault(k=>k.Name == name);
        }

        public Domain GetDomain(string name)
        {
            return Domains.FirstOrDefault(k => k.Name == name);
        }

    }
}
