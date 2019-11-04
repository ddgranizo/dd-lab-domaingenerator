using DD.DomainGenerator.Actions.Base;
using DD.DomainGenerator.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DD.DomainGenerator.Events
{
    public class ProjectEventArgs : EventArgs
    {
        public ProjectState ProjectState { get; set; }
        public List<DeployActionUnit> DeployActionUnits { get; }

        public ProjectEventArgs(ProjectState projectState, List<DeployActionUnit> deployActionUnits)
        {
            ProjectState = projectState;
            DeployActionUnits = deployActionUnits ?? throw new ArgumentNullException(nameof(deployActionUnits));
        }
    }
}
