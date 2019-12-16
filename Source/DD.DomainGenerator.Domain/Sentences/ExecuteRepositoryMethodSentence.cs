using DD.DomainGenerator.Models;
using DD.DomainGenerator.Sentences.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DD.DomainGenerator.Sentences
{
    public class ExecuteRepositoryMethodSentence : ExecutionSentenceBase
    {
        public const string SentenceName = "Execute Repository Method";
        public const string SentenceDescription = "Call to a repository method";
        public const ExecutionSentenceType SentenceType = ExecutionSentenceType.ExecuteRepositoryMethod;

        public ExecuteRepositoryMethodSentence(
            Domain domain,
            Schema schema,
            Repository repository,
            RepositoryMethod method) 
            : base(SentenceName, SentenceDescription, SentenceType)
        {
            Domain = domain ?? throw new ArgumentNullException(nameof(domain));
            Schema = schema ?? throw new ArgumentNullException(nameof(schema));
            Repository = repository ?? throw new ArgumentNullException(nameof(repository));
            Method = method ?? throw new ArgumentNullException(nameof(method));

            AddValue(nameof(Domain), Domain);
            AddValue(nameof(Schema), Schema);
            AddValue(nameof(Repository), Repository);
            AddValue(nameof(Method), Method);

            AddInputContextParameter(method.InputParameters.ToArray());
            AddOutputContextParameter(Method.OutputParameters.First());
        }

        public Domain Domain { get; }
        public Schema Schema { get; }
        public Repository Repository { get; }
        public RepositoryMethod Method { get; }

    }
}
