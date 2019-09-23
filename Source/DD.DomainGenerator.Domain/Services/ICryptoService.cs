using System;
using System.Collections.Generic;
using System.Text;

namespace DD.DomainGenerator.Services
{
    public interface ICryptoService
    {
        string Decrypt(string str);
        string Encrypt(string str);
    }
}
