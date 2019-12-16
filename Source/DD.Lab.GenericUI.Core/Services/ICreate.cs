using System;
using System.Collections.Generic;
using System.Text;

namespace DD.Lab.GenericUI.Core.Services
{
    public interface ICreate
    {
        Guid Execute(string entity, Dictionary<string, object> values);
    }
}
