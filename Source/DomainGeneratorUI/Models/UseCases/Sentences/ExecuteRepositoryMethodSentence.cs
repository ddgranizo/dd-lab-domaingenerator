﻿using DD.Lab.Wpf.Drm.Models;
using DD.Lab.Wpf.Drm.Services;
using DD.Lab.Wpf.Drm.Services.Implementations;
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
        public IJsonParserService JsonParserService { get; set; }

        public ExecuteRepositoryMethodSentence()
        {
            JsonParserService = new JsonParserService();
        }

        public ExecuteRepositoryMethodSentence(UseCaseSentence baseSentence)
            :this()
        {
            Type = SentenceType.ExecuteRepositoryMethod;
            Values = baseSentence.Values;

            InputParameters = baseSentence.InputParameters;
            OutputParameters = baseSentence.OutputParameters;
            ReferencedInputParametersValues = baseSentence.ReferencedInputParametersValues;
            Name = baseSentence.Name;
            Description = baseSentence.Description;
            DisplayName = baseSentence.DisplayName;

            SchemaName = GetValue<string>(nameof(SchemaName));
            RepositoryName = GetValue<string>(nameof(RepositoryName));
            RepositoryMethodId = GetValue<EntityReferenceValue>(nameof(RepositoryMethodId));

        }

        public ExecuteRepositoryMethodSentence(string schemaName, string repositoryName, RepositoryMethod repositoryMethod)
            : this()
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
            DisplayName = $"Method {SchemaName}.{RepositoryName}.{regardingRepositoryMethod.Name}";

            var repositoryMethod = JsonParserService.ObjectifyWithTypes<RepositoryMethodContent>
                    (regardingRepositoryMethod.Content);

            foreach (var item in repositoryMethod.Parameters
                                    .Where(k => k.Direction == Methods.MethodParameter.ParameterDirection.Input))
            {
                InputParameters.Add(item);
            }

            foreach (var item in repositoryMethod.Parameters
                                    .Where(k => k.Direction == Methods.MethodParameter.ParameterDirection.Output))
            {
                OutputParameters.Add(item);
            }
        }


    }
}
