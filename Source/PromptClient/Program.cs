using DD.DomainGenerator;
using DD.DomainGenerator.Actions;
using DD.DomainGenerator.Actions.Base;
using DD.DomainGenerator.Actions.Project;
using DD.DomainGenerator.Events;
using DD.DomainGenerator.Extensions;
using DD.DomainGenerator.Models;
using DD.DomainGenerator.Utilities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace PromptClient
{
    class Program
    {
        static void Main(string[] args)
        {
            var projectManager = new ProjectManager();
            projectManager.PromptMode();
        }
    }
}
