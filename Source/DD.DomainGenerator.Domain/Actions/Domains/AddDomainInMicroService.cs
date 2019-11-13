using DD.DomainGenerator.Actions.Base;
using DD.DomainGenerator.DeployActions.Base;
using DD.DomainGenerator.DeployActions.Domains;
using DD.DomainGenerator.Models;
using DD.DomainGenerator.Services;
using DD.DomainGenerator.Utilities;
using System;
using System.Collections.Generic;
using System.Text;
using static DD.DomainGenerator.Definitions.ActionsParametersDefinitions;

namespace DD.DomainGenerator.Actions.Domains
{

    public class AddDomainInMicroService : ActionBase
    {
        public const string ActionName = "AddDomainInMicroService";
        public ActionParameterDefinition DomainNameParameter { get; set; }
        public ActionParameterDefinition MicroServiceNameParameter { get; set; }
        public IFileService FileService { get; }
        public IDotnetService DotnetService { get; }

        public AddDomainInMicroService(IFileService fileService, IDotnetService dotnetService) : base(ActionName)
        {

            DomainNameParameter = new ActionParameterDefinition(
               AddDomainInMicroservice.DomainName, ActionParameterDefinition.TypeValue.String, "Domain name", "d", string.Empty)
            { IsDomainSuggestion = true };
            MicroServiceNameParameter = new ActionParameterDefinition(
                AddDomainInMicroservice.MicroserviceName, ActionParameterDefinition.TypeValue.String, "Micro service name", "m", string.Empty)
            { IsMicroServiceSuggestion = true };
            

            ActionParametersDefinition.Add(MicroServiceNameParameter);
            ActionParametersDefinition.Add(DomainNameParameter);
            FileService = fileService ?? throw new ArgumentNullException(nameof(fileService));
            DotnetService = dotnetService ?? throw new ArgumentNullException(nameof(dotnetService));
        }

        public override bool CanExecute(ProjectState project, List<ActionParameter> parameters)
        {
            return IsParamOk(parameters, MicroServiceNameParameter) && IsParamOk(parameters, DomainNameParameter);
        }

        public override void Execute(ProjectState project, List<ActionParameter> parameters)
        {
            var microServiceName = GetStringParameterValue(parameters, MicroServiceNameParameter);
            var microService = project.GetMicroService(microServiceName);
            if (microService == null)
            {
                throw new Exception($"Can't find any microService named '{microService}'");
            }
            var domainName = GetStringParameterValue(parameters, DomainNameParameter);
            var domain = project.GetDomain(domainName);
            if (domain == null)
            {
                throw new Exception($"Can't find any domain named '{domainName}'");
            }
            project.DomainInMicroservices.Add(new DomainInMicroService(domain, microService));
        }


        public override List<DeployActionUnit> GetDeployActionUnits(ActionExecution actionExecution)
        {
            return new List<DeployActionUnit>()
            {
                //new CreateDotnetProject(actionExecution, DotnetService, FileService),
            };
        }

    }
}
