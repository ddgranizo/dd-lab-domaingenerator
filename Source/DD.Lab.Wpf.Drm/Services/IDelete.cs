using System;
using System.Collections.Generic;
using System.Text;

namespace DD.Lab.Wpf.Drm.Services
{
    public interface IDelete
    {
        void Execute(string entity, Guid id);
    }
}
