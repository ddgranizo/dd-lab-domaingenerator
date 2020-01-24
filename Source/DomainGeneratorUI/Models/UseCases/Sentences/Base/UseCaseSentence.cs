using System;
using System.Collections.Generic;
using System.Text;

namespace DomainGeneratorUI.Models.UseCases.Sentences.Base
{
    public class UseCaseSentence
    {
        public enum SentenceType
        {
            ExecuteRepositoryMethod = 10,
            ExecuteService = 20,
        }

        public SentenceType Type { get; set; }
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public string Description { get; set; }

        public List<SentenceInputParameter> InputParameters { get; set; }
        public List<SentenceOutputParameter> OutputParameters { get; set; }
        public Dictionary<string, object> Values { get; set; }
        public UseCaseSentence()
        {
            InputParameters = new List<SentenceInputParameter>();
            OutputParameters = new List<SentenceOutputParameter>();
            Values = new Dictionary<string, object>();
        }
    }
}
