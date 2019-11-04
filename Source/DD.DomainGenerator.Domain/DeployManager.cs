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
            AvailableGithubRepositories = 10,
            AvailableApis = 20,
            AvailableAzurePipeline = 30,
        }

        public Phases CurrentPhase { get; set; }

        public DeployManager()
        {

        }

    }
}
