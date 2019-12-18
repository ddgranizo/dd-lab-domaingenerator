using DD.Lab.Wpf.Drm.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DD.Lab.Wpf.Drm.Events
{
    public class CreateRequestEventArgs : EventArgs
    {
        public Entity Entity { get; }
        public Dictionary<string, object> InitalValues { get; }

        public CreateRequestEventArgs(Entity entity, Dictionary<string, object> initalValues)
        {
            Entity = entity ?? throw new ArgumentNullException(nameof(entity));
            InitalValues = initalValues ?? throw new ArgumentNullException(nameof(initalValues));
        }

        public CreateRequestEventArgs(Entity entity)
            : this(entity, new Dictionary<string, object>())
        {
        }
    }
}
