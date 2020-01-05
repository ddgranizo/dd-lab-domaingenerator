using DD.Lab.Wpf.Drm.Models;
using DD.Lab.Wpf.Drm.Models.Data;
using DD.Lab.Wpf.Drm.Models.Workflows;
using DD.Lab.Wpf.Drm.Services;
using DD.Lab.Wpf.Drm.Services.Implementations;
using DD.Lab.Wpf.Events;
using DD.Lab.Wpf.Models.Inputs;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DD.Lab.Wpf.Drm
{

    public delegate void CustomModuleHandler(object sender, WpfCustomModuleEventArgs args);
    public class GenericManager
    {
        public event CustomModuleHandler OnCustomModuleContentEditRequested;

        public MetadataModel Model { get; set; }
        public IJsonParserService ParserService { get; set; }

        public ICreate CreateHandler { get; set; }
        public IUpdate UpdateHandler { get; set; }
        public IDelete DeleteHandler { get; set; }
        public IRetrieve RetrieveHandler { get; set; }
        public IRetrieveAll RetrieveAllHandler { get; set; }
        public IAssociate AssociateHandler { get; set; }
        public IDisassociate DisassociateHandler { get; set; }
        public IRetrieveAllAssociated RetrieveAllAssociatedHandler { get; set; }

        public IEntityReferenceSuggestionHandler EntityReferenceSuggestionHandler { get; set; }


        public List<WorkflowDefinition> CreateWorkflows { get; set; }
        public List<WorkflowDefinition> UpdateWorkflows { get; set; }
        public List<WorkflowDefinition> DeleteWorkflows { get; set; }
        public List<WorkflowDefinition> AssociateWorkflow { get; set; }

        public GenericManager()
        {
            ParserService = new JsonParserService();
            Model = new MetadataModel();
            CreateWorkflows = new List<WorkflowDefinition>();
            UpdateWorkflows = new List<WorkflowDefinition>();
            DeleteWorkflows = new List<WorkflowDefinition>();
            AssociateWorkflow = new List<WorkflowDefinition>();
        }


        public void RaiseOnCustomModuleContentEditRequested(object sender, WpfCustomModuleEventArgs args)
        {
            OnCustomModuleContentEditRequested?.Invoke(sender, args);
        }

        public void RegisterNewCreateWorkflow(string entity, int order, IWorkflowAction action)
        {
            CreateWorkflows.Add(new WorkflowDefinition(entity, order, action));
        }

        public void RegisterNewUpdateWorkflow(string entity, int order, IWorkflowAction action)
        {
            UpdateWorkflows.Add(new WorkflowDefinition(entity, order, action));
        }

        public void RegisterNewDeleteWorkflow(string entity, int order, IWorkflowAction action)
        {
            DeleteWorkflows.Add(new WorkflowDefinition(entity, order, action));
        }

        public void RegisterNewAssociatekflow(string entity, int order, IWorkflowAction action)
        {
            AssociateWorkflow.Add(new WorkflowDefinition(entity, order, action));
        }

        public void RegisterNewCreateWorkflow(string entity, IWorkflowAction action)
        {
            CreateWorkflows.Add(new WorkflowDefinition(entity, action));
        }

        public void RegisterNewUpdateWorkflow(string entity, IWorkflowAction action)
        {
            UpdateWorkflows.Add(new WorkflowDefinition(entity, action));
        }

        public void RegisterNewDeleteWorkflow(string entity, IWorkflowAction action)
        {
            DeleteWorkflows.Add(new WorkflowDefinition(entity, action));
        }

        public void RegisterNewAssociatekflow(string entity, IWorkflowAction action)
        {
            AssociateWorkflow.Add(new WorkflowDefinition(entity, action));
        }


        public void LoadModelFromJson(string json)
        {
            Model = ParserService.Objectify<MetadataModel>(json);
        }

        public void InitializeModel(MetadataModel model)
        {
            Model = model ?? throw new ArgumentNullException(nameof(model));
        }

        public Guid Create(string entity, Dictionary<string, object> values)
        {
            var id = CreateHandler.Execute(entity, values);
            var workflowInput = new WorkflowInputParameter();
            workflowInput.Values.Add("Id", id);
            workflowInput.Values.Add("Values", values);
            foreach (var item in CreateWorkflows
                .Where(k => k.EntityLogicalName == entity)
                .OrderBy(k => k.Order))
            {
                if (item.Action != null)
                {
                    item.Action.Execute(this, workflowInput);
                }
            }
            return id;
        }

        public void Update(string entity, Guid id, Dictionary<string, object> values)
        {
            UpdateHandler.Execute(entity, id, values);
            var workflowInput = new WorkflowInputParameter();
            workflowInput.Values.Add("Id", id);
            workflowInput.Values.Add("Values", values);
            foreach (var item in UpdateWorkflows
                .Where(k => k.EntityLogicalName == entity)
                .OrderBy(k => k.Order))
            {
                if (item.Action != null)
                {
                    item.Action.Execute(this, workflowInput);
                }
            }

        }

        public DataRecord Retrieve(string entity, Guid id)
        {
            return RetrieveHandler.Execute(entity, id);
        }

        public DataSet RetrieveAll(string entity)
        {
            return RetrieveAllHandler.Execute(entity);
        }

        public DataSet RetrieveAllAssociated(string firstEntity, Guid mainId, string intersectionEntity, string secondEntity)
        {
            return RetrieveAllAssociatedHandler.Execute(firstEntity, mainId, intersectionEntity, secondEntity);
        }

        public void Delete(string entity, Guid id)
        {
            DeleteHandler.Execute(entity, id);
            var workflowInput = new WorkflowInputParameter();
            workflowInput.Values.Add("Id", id);
            foreach (var item in DeleteWorkflows
                .Where(k => k.EntityLogicalName == entity)
                .OrderBy(k => k.Order))
            {
                if (item.Action != null)
                {
                    item.Action.Execute(this, workflowInput);
                }
            }
        }

        public void Disassociate(string firstEntity, Guid firstId, string intersectionEntity, string secondEntity, Guid secondId)
        {
            DisassociateHandler.Execute(firstEntity, firstId, intersectionEntity, secondEntity, secondId);
        }

        public Guid Associate(string firstEntity, Guid firstId, string intersectionEntity, string secondEntity, Guid secondId)
        {
            var id = AssociateHandler.Execute(firstEntity, firstId, intersectionEntity, secondEntity, secondId);
            var workflowInput = new WorkflowInputParameter();
            workflowInput.Values.Add("Id", id);
            workflowInput.Values.Add("FirstEntity", firstEntity);
            workflowInput.Values.Add("FirstId", firstId);
            workflowInput.Values.Add("SecondEntity", secondEntity);
            workflowInput.Values.Add("SecondId", secondId);

            foreach (var item in AssociateWorkflow
                .Where(k => k.EntityLogicalName == intersectionEntity)
                .OrderBy(k => k.Order))
            {
                if (item.Action != null)
                {
                    item.Action.Execute(this, workflowInput);
                }
            }
            return id;
        }

    }
}
