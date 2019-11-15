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
        
        public string Path { get; set; }
        public List<AzurePipelineSetting> AzurePipelineSettings { get; set; }
        public List<GithubSetting> GithubSettings { get; set; }
        public List<Models.Environment> Environments { get; set; }
        public List<Domain> Domains { get; set; }

        public List<Setting> Settings { get; set; }

        public ProjectState()
        {
            AzurePipelineSettings = new List<AzurePipelineSetting>();
            GithubSettings = new List<GithubSetting>();
            Domains = new List<Domain>();
            Environments = new List<Models.Environment>();
            Settings = new List<Setting>();
        }


        public List<Schema> GetAllSchemas()
        {
            return Domains.SelectMany(k=>k.Schemas).ToList();
        }

        public List<Domain> GetAllDomains()
        {
            return Domains;
        }


        public Schema GetSchema(string name)
        {
            return GetAllSchemas().FirstOrDefault(k=>k.Name == name);
        }

        public Domain GetDomain(string name)
        {
            return Domains.FirstOrDefault(k => k.Name == name);
        }

    }
}
