using DD.Lab.Wpf.Drm.Models;
using DD.Lab.Wpf.Models.Inputs;
using DomainGeneratorUI.Models.RepositoryMethods;
using DomainGeneratorUI.Models.UseCases.Sentences.Base;
using DomainGeneratorUI.Viewmodels.UseCases.Sentences.Base;
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
        public EntityReferenceValue RepositoryMethodId { get; set; }

        //public RepositoryMethod RegardingRepositoryMethod { get; set; }
        public ExecuteRepositoryMethodSentence()
        {

        }

        public ExecuteRepositoryMethodSentence(UseCaseSentence baseSentence)
        {
            Type = SentenceType.ExecuteRepositoryMethod;
            SchemaName = (string)baseSentence.Values[nameof(SchemaName)];
            RepositoryName = (string)baseSentence.Values[nameof(RepositoryName)];
            RepositoryMethodId = (EntityReferenceValue)baseSentence.Values[nameof(RepositoryMethodId)];

            InputParameters = baseSentence.InputParameters;
            OutputParameters = baseSentence.OutputParameters;
            ReferencedInputParametersValues = baseSentence.ReferencedInputParametersValues;
            //InputReferencedParameters = baseSentence.InputReferencedParameters;
            //OutputReferencedParameters = baseSentence.OutputReferencedParameters;
            Name = baseSentence.Name;
            Description = baseSentence.Description;
            DisplayName = baseSentence.DisplayName;
        }

        public ExecuteRepositoryMethodSentence(string schemaName, string repositoryName, RepositoryMethod repositoryMethod)
        {
            Type = SentenceType.ExecuteRepositoryMethod;
            if (repositoryMethod == null)
            {
                throw new ArgumentNullException(nameof(repositoryMethod));
            }
            RepositoryMethodId = new EntityReferenceValue(repositoryMethod.Id, RepositoryMethod.LogicalName, repositoryMethod.Name);
            RepositoryName = repositoryName;
            SchemaName = schemaName;

            Values.Add(nameof(SchemaName), schemaName);
            Values.Add(nameof(RepositoryName), repositoryName);
            Values.Add(nameof(RepositoryMethodId), RepositoryMethodId);

            ProcessData(repositoryMethod);
        }

        public void ProcessData(RepositoryMethod regardingRepositoryMethod)
        {
            Name = regardingRepositoryMethod.Name;
            Description = $"Method {SchemaName}.{RepositoryName}.{regardingRepositoryMethod.Name}";

            var repositoryMethod = JsonConvert.DeserializeObject<RepositoryMethodContent>(regardingRepositoryMethod.Content);

            

            foreach (var item in repositoryMethod.Parameteters
                                    .Where(k => k.Direction == Methods.MethodParameter.ParameterDirection.Input))
            {
                InputParameters.Add(item);
                //InputReferencedParameters.Add(new SentenceInputReferencedParameter()
                //{
                //    RegardingParameter = item,
                //});
            }

            foreach (var item in repositoryMethod.Parameteters
                                    .Where(k => k.Direction == Methods.MethodParameter.ParameterDirection.Output))
            {
                OutputParameters.Add(item);
                //OutputReferencedParameters.Add(new SentenceOutputReferencedParameter()
                //{
                //    RegardingParameter = item,
                //});
            }
        }


    }
}
