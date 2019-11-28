using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static DD.DomainGenerator.Definitions;

namespace DD.DomainGenerator.Models
{
    public class UseCaseExecutionContext
    {

        public List<UseCaseExecutionContextParameter> ContextItems { get; set; }
        public UseCaseExecutionContext()
        {
            ContextItems = new List<UseCaseExecutionContextParameter>();
        }

        public UseCaseExecutionContext CleanContextItems()
        {
            ContextItems.Clear();
            return this;
        }

        public UseCaseExecutionContext AddContextParameter(UseCaseExecutionContextParameter parameter)
        {
            ContextItems.Add(parameter);
            return this;
        }

    }
}
