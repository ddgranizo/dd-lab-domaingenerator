using System;
using System.Collections.Generic;
using System.Text;

namespace DD.Lab.Services.System.Interfaces
{
    public interface IConsoleService
    {
        string ReadLine();

        void WriteLine(string text);
    }
}
