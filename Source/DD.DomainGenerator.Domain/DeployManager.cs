using DD.DomainGenerator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DD.DomainGenerator
{
    public class DeployManager
    {
        public enum Phases
        {
            EmptyProject = 1,
            AvailableBaseDomain = 10,
            AvailableDomains = 15,
            AvailableBaseInfrastructure = 20,
            AvailableInfrastructure = 25,
            AvailableSolutionFile = 30,
            AvailableGithubRepositories = 40,
            AvailableApis = 50,
            AvailableAzurePipeline = 60,
        }

        public Phases CurrentPhase { get; set; }

        public DeployManager()
        {

        }

    }
}
