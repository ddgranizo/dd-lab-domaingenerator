using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DD.DomainGenerator.Models
{
    public class ArchitectureSetup
    {
        public enum ArchitectureNodesSetup
        {
            Monolith = 1,
            Microservices = 2,
        }

        public enum ArchitectureEnvironmentsSetup
        {
            OnlyProduction = 1,
            DevAndPro = 2,
            DevPreAndPro = 3,
            DevIntPreAndPro = 4,
        }

        public List<Environment> Environments { get; set; }
        public List<Service> Services { get; set; }
        public ArchitectureSetup()
        {
            Services = new List<Service>();
            Environments = new List<Environment>();
        }

        public void InitializeNodesSetup(
            ProjectState project,
            ArchitectureNodesSetup nodesSetup)
        {
            //var service = new Service();
            //if (nodesSetup == ArchitectureNodesSetup.Microservices)
            //{
            //    service.AddDomains(project.Domain.GetDomainsBelow().Where(k=>k.HasModel).ToList());
            //}
            //else if (nodesSetup == ArchitectureNodesSetup.Monolith)
            //{
            //    service.AddDomain(project.Domain);
            //}
        }

        public void InitializeEnvironmentsSetup(
            ArchitectureEnvironmentsSetup environmentsSetup)
        {
            if (environmentsSetup == ArchitectureEnvironmentsSetup.OnlyProduction)
            {
                Environments.Add(new Environment(Definitions.DefaultEnvironmentNames.Production.Name, Definitions.DefaultEnvironmentNames.Production.Shortname, 1));
            }
            else if (environmentsSetup == ArchitectureEnvironmentsSetup.DevAndPro)
            {
                Environments.Add(new Environment(Definitions.DefaultEnvironmentNames.Development.Name, Definitions.DefaultEnvironmentNames.Development.Shortname, 1));
                Environments.Add(new Environment(Definitions.DefaultEnvironmentNames.Production.Name, Definitions.DefaultEnvironmentNames.Production.Shortname, 2));
            }
            else if (environmentsSetup == ArchitectureEnvironmentsSetup.DevPreAndPro)
            {
                Environments.Add(new Environment(Definitions.DefaultEnvironmentNames.Development.Name, Definitions.DefaultEnvironmentNames.Development.Shortname, 1));
                Environments.Add(new Environment(Definitions.DefaultEnvironmentNames.Preproduction.Name, Definitions.DefaultEnvironmentNames.Preproduction.Shortname, 2));
                Environments.Add(new Environment(Definitions.DefaultEnvironmentNames.Production.Name, Definitions.DefaultEnvironmentNames.Production.Shortname, 3));
            }
            else if (environmentsSetup == ArchitectureEnvironmentsSetup.DevIntPreAndPro)
            {
                Environments.Add(new Environment(Definitions.DefaultEnvironmentNames.Development.Name, Definitions.DefaultEnvironmentNames.Development.Shortname, 1));
                Environments.Add(new Environment(Definitions.DefaultEnvironmentNames.Integration.Name, Definitions.DefaultEnvironmentNames.Integration.Shortname, 2));
                Environments.Add(new Environment(Definitions.DefaultEnvironmentNames.Preproduction.Name, Definitions.DefaultEnvironmentNames.Preproduction.Shortname, 3));
                Environments.Add(new Environment(Definitions.DefaultEnvironmentNames.Production.Name, Definitions.DefaultEnvironmentNames.Production.Shortname, 4));
            }
        }


        public static ArchitectureNodesSetup StringToArchitectureNodesSetupType(string type)
        {
            foreach (var item in Enum.GetValues(typeof(ArchitectureNodesSetup)))
            {
                var name = Enum.GetName(typeof(ArchitectureNodesSetup), item);
                if (name == type)
                {
                    return (ArchitectureNodesSetup)item;
                }
            }
            throw new Exception($"Can't find type named {type}");
        }

        public static List<string> GetArchitectureNodesSetupTypesList()
        {
            return Enum.GetNames(typeof(ArchitectureNodesSetup)).ToList();
        }

        public static ArchitectureEnvironmentsSetup StringToArchitectureEnvironmentsSetupType(string type)
        {
            foreach (var item in Enum.GetValues(typeof(ArchitectureEnvironmentsSetup)))
            {
                var name = Enum.GetName(typeof(ArchitectureEnvironmentsSetup), item);
                if (name == type)
                {
                    return (ArchitectureEnvironmentsSetup)item;
                }
            }
            throw new Exception($"Can't find type named {type}");
        }

        public static List<string> GetArchitectureEnvironmentsSetupTypesList()
        {
            return Enum.GetNames(typeof(ArchitectureEnvironmentsSetup)).ToList();
        }
    }
}
