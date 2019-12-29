using DD.Lab.Wpf.Drm;
using DD.Lab.Wpf.Drm.Models;
using DD.Lab.Wpf.Models.Inputs;
using DomainGeneratorUI.Models;
using DomainGeneratorUI.Workflows.Schemas;
using System;
using System.Collections.Generic;
using System.Text;
using static DD.Lab.Wpf.Drm.Models.Attribute;

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
            GenericManager.RegisterNewCreateWorkflow(Schema.LogicalName, new CreateMainPropertiesWorkflow());
        }


    }
}
