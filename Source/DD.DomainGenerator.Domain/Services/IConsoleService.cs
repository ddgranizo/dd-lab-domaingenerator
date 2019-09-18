using System;
using System.Collections.Generic;
using System.Text;

namespace DD.DomainGenerator.Services
{
    public interface IConsoleService
    {
        string ReadLine();

        void WriteLine(string text);
    }
}
