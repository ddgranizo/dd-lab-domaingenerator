using DD.Lab.Wpf.Drm.Models.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace DD.Lab.Wpf.Drm.Services
{
    public interface IRetrieveAll
    {
        DataSet Execute(string entity);
    }
}
