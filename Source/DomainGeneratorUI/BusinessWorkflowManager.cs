using DD.Lab.Wpf.Drm;
using System;
using System.Collections.Generic;
using System.Text;

namespace DomainGeneratorUI
{
    public class BusinessWorkflowManager
    {
        public GenericManager GenericManager { get; set; }

        public BusinessWorkflowManager(GenericManager genericManager)
        {
            GenericManager = genericManager ?? throw new ArgumentNullException(nameof(genericManager));
            Initialize();
        }

        private void Initialize()
        {
            InitializeSchemaWorkflows();
        }

        private void InitializeSchemaWorkflows()
        {
            GenericManager.RegisterNewCreateWorkflow("Schema", (genericManager, input) =>
            {
                var schemaId = (Guid)input.Values["Id"];

            });
        }

    }
}
