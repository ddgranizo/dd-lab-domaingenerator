using System;
using System.Collections.Generic;
using System.Text;

namespace DD.Lab.Wpf.Drm.Services
{
    public interface IUpdate
    {
        void Execute(string entity, Guid id, Dictionary<string, object> values);
    }
}
