using DD.DomainGenerator.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DD.DomainGenerator.Sentences.Base
{
    public class ExecutionSentenceBase
    {
        public enum ExecutionSentenceType
        {
            ExecuteRepositoryMethod = 10,
            ExecuteService = 11,
        }

        public Dictionary<string, object> Values { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
        public ExecutionSentenceType Type { get; set; }

        public List<UseCaseExecutionContextParameter> InputContextParameters { get; set; }
        public List<UseCaseExecutionContextParameter> OutputContextParameters { get; set; }



        public ExecutionSentenceBase(string name, string description, ExecutionSentenceType type)
        {
            Name = name;
            Description = description;
            Type = type;
            InputContextParameters = new List<UseCaseExecutionContextParameter>();
            OutputContextParameters = new List<UseCaseExecutionContextParameter>();
            Values = new Dictionary<string, object>();
        }

        public void AddValue(string key, object value)
        {
            if (Values.ContainsKey(key))
            {
                Values[key] = value;
            }
            else
            {
                Values.Add(key, value);
            }
        }

        public void AddInputContextParameter(params DataParameter[] parameters)
        {
            foreach (var parameter in parameters)
            {
                InputContextParameters.Add(new UseCaseExecutionContextParameter(
                    UseCaseExecutionContextParameter.ParameterDirection.Input,
                    parameter,
                    this));
            }
        }

        public void AddOutputContextParameter(DataParameter parameter)
        {
            OutputContextParameters.Add(new UseCaseExecutionContextParameter(
                UseCaseExecutionContextParameter.ParameterDirection.Output,
                parameter,
                this));
        }

        public virtual bool CanExecuteSentenceWithAvailableParameters(List<UseCaseExecutionContextParameter> parameters)
        {
            throw new NotImplementedException();
        }
    }
}
