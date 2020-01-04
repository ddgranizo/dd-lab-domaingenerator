using DD.Lab.Wpf.Drm.Models.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace DD.Lab.Wpf.Drm.Services
{
    public interface IRetrieve
    {
        DataRecord Execute(string entity, Guid id);
    }
}
