﻿using DD.Lab.GenericUI.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using UIClientV2.Events;

namespace UIClientV2
{

    public delegate void CreateRequestHandler(object sender, CreateRequestEventArgs eventArgs);
    public delegate void EntityHandler(object sender, EntityEventArgs eventArgs);

    public class BusinessEventManager
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

        public void RaiseOnCreatedEntity(Entity entity, Dictionary<string, object> values)
        {
            OnCreatedEntity?.Invoke(this, new EntityEventArgs(entity, values));
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
