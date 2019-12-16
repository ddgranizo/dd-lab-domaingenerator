using System;
using System.Collections.Generic;
using System.Text;
using UIClient.Models;
using UIClient.Models.Sentences;
using UIClient.Models.Sentences.Base;

namespace UIClient.Extensions
{
    public static class ExecutionSentenceBaseModelExtensions
    {
        public static ExecuteRepositoryMethodSentenceModel ToExecuteRepositoryMethodSentence(this ExecutionSentenceBaseModel execution)
        {
            var domain = GetInstance<DomainModel>(execution.Values, nameof(ExecuteRepositoryMethodSentenceModel.Domain));
            var schema = GetInstance<SchemaModel>(execution.Values, nameof(ExecuteRepositoryMethodSentenceModel.Schema));
            var repository = GetInstance<RepositoryModel>(execution.Values, nameof(ExecuteRepositoryMethodSentenceModel.Repository));
            var method = GetInstance<RepositoryMethodModel>(execution.Values, nameof(ExecuteRepositoryMethodSentenceModel.Method));
            return new ExecuteRepositoryMethodSentenceModel() { Domain = domain, Schema = schema, Repository = repository, Method = method };
        }

        private static T GetInstance<T>(Dictionary<string, object> values, string key)
        {
            if (values.ContainsKey(key))
            {
                return (T)values[key];
            }
            throw new Exception($"Invalid cast. Can't find '{key}' parameter");
        }
    }
}
