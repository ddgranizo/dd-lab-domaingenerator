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
        public string NameSpace { get; set; }
        public List<ActionExecution> Actions { get; set; }
        public List<Domain> Domains { get; set; }
        public List<SchemaModel> Schemas { get; set; }
        public List<SchemaInDomain> SchemaInDomains { get; set; }
        public string ProjectPath { get; set; }
        public List<AzurePipelineSetting> AzurePipelineSettings { get; set; }
        public List<GithubSetting> GithubSettings { get; set; }
        public List<Models.Environment> Environments { get; set; }
        public List<MicroService> Microservices { get; set; }
        public List<DomainInMicroService> DomainInMicroservices { get; set; }

        public List<Setting> Settings { get; set; }

        public ProjectState()
        {
            SchemaInDomains = new List<SchemaInDomain>();
            Domains = new List<Domain>();
            Actions = new List<ActionExecution>();
            AzurePipelineSettings = new List<AzurePipelineSetting>();
            GithubSettings = new List<GithubSetting>();
            Schemas = new List<SchemaModel>();
            Microservices = new List<MicroService>();
            DomainInMicroservices = new List<DomainInMicroService>();
            Environments = new List<Models.Environment>();
            Settings = new List<Setting>();
        }


        public List<SchemaModel> GetAllSchemas()
        {
            return Schemas;
        }

        public List<Domain> GetAllDomains()
        {
            return Domains;
        }

        public MicroService GetMicroService(string name)
        {
            return Microservices.FirstOrDefault(k => k.Name == name);
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
