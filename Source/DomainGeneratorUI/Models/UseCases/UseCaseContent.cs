using DomainGeneratorUI.Interfaces;
using DomainGeneratorUI.Models.Methods;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace DomainGeneratorUI.Models.UseCases
{
    public class UseCaseContent : IInitializable<UseCaseContent>
    {
        public List<MethodParameter> Parameters { get; set; }
        public UseCaseSentenceCollection SentenceCollection { get; set; }

        public UseCaseContent()
        {
            Parameters = new List<MethodParameter>();
            SentenceCollection = new UseCaseSentenceCollection();
        }


        public UseCaseContent GetInitialInstance()
        {
            return new UseCaseContent();
        }
    }
}
