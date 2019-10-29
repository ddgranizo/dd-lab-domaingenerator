using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using DD.DomainGenerator.Actions.Base;
using DD.DomainGenerator.Extensions;
using DD.DomainGenerator.Models;
using DD.DomainGenerator.Services;
using DD.DomainGenerator.Utilities;

namespace DD.DomainGenerator.Actions.Environments
{
    public class AddEnvironment : ActionBase
    {
        public const string ActionName = "AddEnvironment";
        
        public ActionParameterDefinition NameParameter { get; set; }
        public ActionParameterDefinition ShortNameParameter { get; set; }
        public ActionParameterDefinition OrderParameter { get; set; }

        public AddEnvironment() : base(ActionName)
        {
            NameParameter = new ActionParameterDefinition(
                   "name", ActionParameterDefinition.TypeValue.String, "Evironment name", "n", string.Empty);
            ShortNameParameter = new ActionParameterDefinition(
                  "shortname", ActionParameterDefinition.TypeValue.String, "Shortname. Will be used for name the pipelines and branchs. In LowerCase", "s", string.Empty);
            OrderParameter = new ActionParameterDefinition(
                  "order", ActionParameterDefinition.TypeValue.Integer, "Order", "n", 1);

            ActionParametersDefinition.Add(NameParameter);
            ActionParametersDefinition.Add(ShortNameParameter);
            ActionParametersDefinition.Add(OrderParameter);
        }

        public override bool CanExecute(ProjectState project, List<ActionParameter> parameters)
        {
            return IsParamOk(parameters, NameParameter)
                && IsParamOk(parameters, ShortNameParameter)
                && IsParamOk(parameters, OrderParameter);
        }

        public override void ExecuteStateChange(ProjectState project, List<ActionParameter> parameters)
        {
            var name = GetStringParameterValue(parameters, NameParameter);
            var shortName = GetStringParameterValue(parameters, ShortNameParameter).ToLowerInvariant();
            var order = GetIntParameterValue(parameters, OrderParameter);

            var repeated = project.Environments
                .FirstOrDefault(k => k.Name == name
                || k.ShortName == shortName
                || k.Order == order);

            if (repeated != null)
            {
                throw new Exception("Environment name, shortname or order repeated");
            }
            project.Environments.Add(new Models.Environment(name, shortName, order));
        }
    }
}
