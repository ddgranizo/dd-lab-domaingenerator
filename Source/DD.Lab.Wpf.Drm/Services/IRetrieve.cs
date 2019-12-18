using DD.Lab.Wpf.Drm.Models.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace DD.Lab.Wpf.Drm.Services
{
    public interface IRetrieve
    {
        DataRowModel Execute(string entity, Guid id);
    }
}
