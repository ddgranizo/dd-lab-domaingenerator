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

        public ProjectEventArgs(ProjectState projectState)
        {
            ProjectState = projectState;
        }
    }
}
