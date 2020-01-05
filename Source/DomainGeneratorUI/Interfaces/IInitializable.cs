using System;
using System.Collections.Generic;
using System.Text;

namespace DomainGeneratorUI.Interfaces
{
    public interface IInitializable<T>
    {
        T GetInitialInstance();
    }
}
