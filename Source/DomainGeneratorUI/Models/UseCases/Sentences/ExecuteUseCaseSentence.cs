using DD.Lab.Wpf.Drm.Services;
using DD.Lab.Wpf.Drm.Services.Implementations;
using DD.Lab.Wpf.Models.Inputs;
using DomainGeneratorUI.Models.UseCases.Sentences.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DomainGeneratorUI.Models.UseCases.Sentences
{
    public class ExecuteUseCaseSentence : UseCaseSentence
    {
        public string SchemaName { get; set; }
        public string UseCaseName { get; set; }
        public EntityReferenceValue UseCaseId { get; set; }
        public IJsonParserService JsonParserService { get; set; }

        public ExecuteUseCaseSentence()
        {
            JsonParserService = new JsonParserService();
        }

        public ExecuteUseCaseSentence(UseCaseSentence baseSentence)
            : this()
        {
            Type = SentenceType.ExecuteUseCase;
            Values = baseSentence.Values;

            InputParameters = baseSentence.InputParameters;
            OutputParameters = baseSentence.OutputParameters;
            ReferencedInputParametersValues = baseSentence.ReferencedInputParametersValues;
            Name = baseSentence.Name;
            Description = baseSentence.Description;
            DisplayName = baseSentence.DisplayName;

            SchemaName = GetValue<string>(nameof(SchemaName));
            UseCaseName = GetValue<string>(nameof(UseCaseName));
            UseCaseId = GetValue<EntityReferenceValue>(nameof(UseCaseId));

        }

        public ExecuteUseCaseSentence(string schemaName,  UseCase useCase)
            : this()
        {

            Type = SentenceType.ExecuteUseCase;
            if (useCase == null)
            {
                throw new ArgumentNullException(nameof(useCase));
            }
            UseCaseId = new EntityReferenceValue(useCase.Id, UseCase.LogicalName, useCase.Name);
            SchemaName = schemaName;

            Values.Add(nameof(SchemaName), schemaName);
            Values.Add(nameof(UseCaseName), useCase.Name);
            Values.Add(nameof(UseCaseId), UseCaseId);

            ProcessData(useCase);
        }

        public void ProcessData(UseCase regardingUseCase)
        {
            Name = regardingUseCase.Name;
            DisplayName = $"Method {SchemaName}.{UseCaseName}.{regardingUseCase.Name}";

            var repositoryMethod = JsonParserService.ObjectifyWithTypes<UseCaseContent>
                    (regardingUseCase.Content);

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
