using System;
using System.Collections.Generic;
using System.Text;

namespace DD.DomainGenerator.Services
{
    public interface IProcessService
    {
        string RunCommand(string command, string filename = null, string workingDirectory = null);
    }
}
