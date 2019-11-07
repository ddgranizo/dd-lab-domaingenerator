using System;
using System.Collections.Generic;
using System.Text;

namespace DD.DomainGenerator.Services
{
    public interface IDotnetService
    {
        void Initialize(string dotnetPath);
        void CreateSolutionFile(string path, string solutionName);
    }
}
