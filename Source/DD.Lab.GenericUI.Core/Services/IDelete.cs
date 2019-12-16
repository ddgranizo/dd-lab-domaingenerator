using System;
using System.Collections.Generic;
using System.Text;

namespace DD.Lab.GenericUI.Core.Services
{
    public interface IDelete
    {
        void Execute(string entity, Guid id);
    }
}
