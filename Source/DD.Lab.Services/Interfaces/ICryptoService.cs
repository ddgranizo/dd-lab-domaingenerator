using System;
using System.Collections.Generic;
using System.Text;

namespace DD.Lab.Services.System.Interfaces
{
    public interface ICryptoService
    {
        string Decrypt(string str);
        string Encrypt(string str);
    }
}
