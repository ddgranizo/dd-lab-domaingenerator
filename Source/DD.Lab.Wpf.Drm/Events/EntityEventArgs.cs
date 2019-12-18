using DD.Lab.Wpf.Drm.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DD.Lab.Wpf.Drm.Events
{
    public class EntityEventArgs : EventArgs
    {
        public Entity Entity { get; }
        public Dictionary<string, object> Values { get; }
        public Guid Id { get; set; }
        public EntityEventArgs(Entity entity, Dictionary<string, object> values)
        {
            Entity = entity ?? throw new ArgumentNullException(nameof(entity));
            Values = values ?? throw new ArgumentNullException(nameof(values));
        }

        public EntityEventArgs(Entity entity, Guid id, Dictionary<string, object> values)
        {
            Id = id;
            Entity = entity ?? throw new ArgumentNullException(nameof(entity));
            Values = values ?? throw new ArgumentNullException(nameof(values));
        }

        public EntityEventArgs(Entity entity, Guid id)
        {
            Id = id;
            Entity = entity ?? throw new ArgumentNullException(nameof(entity));
        }

    }
}
