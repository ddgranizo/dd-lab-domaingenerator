using DD.Lab.Wpf.Drm.Events;
using DD.Lab.Wpf.Drm.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DD.Lab.Wpf.Drm
{

    public delegate void CreateRequestHandler(object sender, CreateRequestEventArgs eventArgs);
    public delegate void EntityHandler(object sender, EntityEventArgs eventArgs);

    public class GenericEventManager
    {
        public event CreateRequestHandler OnCreateRequested;
        public event EntityHandler OnCreatedEntity;
        public event EntityHandler OnSelectedEntity;
        public event EntityHandler OnUpdatedEntity;
        public event EntityHandler OnDeletedEntity;

        public void RaiseOnSelectedEntity(Entity entity, Guid id)
        {
            OnSelectedEntity?.Invoke(this, new EntityEventArgs(entity, id));
        }

        public void RaiseOnUpdatedEntity(Entity entity, Guid id, Dictionary<string, object> values)
        {
            OnUpdatedEntity?.Invoke(this, new EntityEventArgs(entity, id, values));
        }

        public void RaiseOnDeletedEntity(Entity entity, Guid id)
        {
            OnDeletedEntity?.Invoke(this, new EntityEventArgs(entity, id));
        }

        public void RaiseOnCreatedEntity(Entity entity, Guid id)
        {
            OnCreatedEntity?.Invoke(this, new EntityEventArgs(entity, id));
        }

        public void RaiseOnCreateRequested(Entity entity, Dictionary<string, object> initialValues)
        {
            OnCreateRequested?.Invoke(this, new CreateRequestEventArgs(entity, initialValues));
        }

        public void RaiseOnCreateRequested(Entity entity)
        {
            OnCreateRequested?.Invoke(this, new CreateRequestEventArgs(entity));
        }
    }
}
