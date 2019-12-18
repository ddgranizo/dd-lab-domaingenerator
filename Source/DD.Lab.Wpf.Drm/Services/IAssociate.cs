using System;
using System.Collections.Generic;
using System.Text;

namespace DD.Lab.Wpf.Drm.Services
{
    public interface IAssociate
    {
        Guid Execute(string firstEntity, Guid firstId, string intersectionEntity, string secondEntity, Guid secondId);
    }
}
