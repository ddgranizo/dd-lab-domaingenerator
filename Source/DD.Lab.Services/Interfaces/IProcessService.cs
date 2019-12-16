using System;
using System.Collections.Generic;
using System.Text;

namespace DD.Lab.Services.System.Interfaces
{
    public interface IProcessService
    {
        string RunCommand(string command, string filename = null, string workingDirectory = null);
        string RunCommand(string command, string filename = null, string workingDirectory = null, params string[] inputs);
    }
}
