﻿using System;
using System.Collections.Generic;
using System.Text;

namespace DD.Lab.Wpf.Drm.Services
{
    public interface IDisassociate
    {
        void Execute(string firstEntity, Guid firstId, string intersectionEntity, string secondEntity, Guid secondId);
    }
}
