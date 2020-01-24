using DD.Lab.Wpf.Drm.Models;
using DomainGeneratorUI.Models.RepositoryMethods;
using DomainGeneratorUI.Models.UseCases.Sentences.Base;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DomainGeneratorUI.Models.UseCases.Sentences
{
    public class ExecuteRepositoryMethodSentence : UseCaseSentence
    {
        public string SchemaName { get; set; }
        public string RepositoryName { get; set; }
        public RepositoryMethod RepositoryMethod { get; set; }

        //public RepositoryMethod RegardingRepositoryMethod { get; set; }

        public ExecuteRepositoryMethodSentence(string schemaName, string repositoryName, RepositoryMethod repositoryMethod)
        {
            Type = SentenceType.ExecuteRepositoryMethod;
            RepositoryMethod = repositoryMethod ?? throw new ArgumentNullException(nameof(repositoryMethod));
            RepositoryName = repositoryName;
            SchemaName = schemaName;

            Values.Add(nameof(SchemaName), schemaName);
            Values.Add(nameof(RepositoryName), repositoryName);
            Values.Add(nameof(RepositoryMethod), Entity.EntityToDictionary(repositoryMethod));

            ProcessData(repositoryMethod);
        }

        private void ProcessData(RepositoryMethod regardingRepositoryMethod)
        {
            Name = RepositoryMethod.Name;
            Description = $"Method {SchemaName}.{RepositoryName}.{regardingRepositoryMethod.Name}";

            var repositoryMethod = JsonConvert.DeserializeObject<RepositoryMethodContent>(regardingRepositoryMethod.Content);

            foreach (var item in repositoryMethod.Parameteters.Where(k => k.Direction == Methods.MethodParameter.ParameterDirection.Input))
            {
                InputParameters.Add(new SentenceInputParameter()
                {
                    Type = SentenceInputParameter.SentenceSourceTpye.UseCaseInput,
                    RegardingUseCaseParameter = item,
                });
            }

            foreach (var item in repositoryMethod.Parameteters.Where(k => k.Direction == Methods.MethodParameter.ParameterDirection.Output))
            {
                OutputParameters.Add(new SentenceOutputParameter()
                {
                    SourceParameter = item,
                });
            }
        }

        public ExecuteRepositoryMethodSentence()
        {
        }
    }
}
