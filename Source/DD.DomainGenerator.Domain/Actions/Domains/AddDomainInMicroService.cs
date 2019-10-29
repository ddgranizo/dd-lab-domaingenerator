using DD.DomainGenerator.Actions.Base;
using DD.DomainGenerator.Models;
using DD.DomainGenerator.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DD.DomainGenerator.Actions.Domains
{

    public class AddDomainInMicroService : ActionBase
    {
        public const string ActionName = "AddDomainInMicroService";
        public ActionParameterDefinition DomainNameParameter { get; set; }
        public ActionParameterDefinition MicroServiceNameParameter { get; set; }

        public AddDomainInMicroService() : base(ActionName)
        {

            DomainNameParameter = new ActionParameterDefinition(
               "domainname", ActionParameterDefinition.TypeValue.String, "Domain name", "d", string.Empty)
            { IsDomainSuggestion = true };
            MicroServiceNameParameter = new ActionParameterDefinition(
                "microservicename", ActionParameterDefinition.TypeValue.String, "Micro service name", "m", string.Empty)
            { IsMicroServiceSuggestion = true };
            

            ActionParametersDefinition.Add(MicroServiceNameParameter);
            ActionParametersDefinition.Add(DomainNameParameter);
        }

        public override bool CanExecute(ProjectState project, List<ActionParameter> parameters)
        {
            return IsParamOk(parameters, MicroServiceNameParameter) && IsParamOk(parameters, DomainNameParameter);
        }

        public override void ExecuteStateChange(ProjectState project, List<ActionParameter> parameters)
        {
            var microServiceName = GetStringParameterValue(parameters, MicroServiceNameParameter).ToWordPascalCase();
            var microService = project.GetMicroService(microServiceName);
            if (microService == null)
            {
                throw new Exception($"Can't find any microService named '{microService}'");
            }
            var domainName = GetStringParameterValue(parameters, DomainNameParameter).ToWordPascalCase();
            var domain = project.GetDomain(domainName);
            if (domain == null)
            {
                throw new Exception($"Can't find any domain named '{domainName}'");
            }
            project.DomainInMicroServices.Add(new DomainInMicroService(domain, microService));
        }

    }
}
