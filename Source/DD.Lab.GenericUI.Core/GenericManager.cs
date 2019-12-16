using DD.Lab.GenericUI.Core.Models;
using DD.Lab.GenericUI.Core.Models.Data;
using DD.Lab.GenericUI.Core.Models.Workflows;
using DD.Lab.GenericUI.Core.Services;
using DD.Lab.GenericUI.Core.Services.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DD.Lab.GenericUI.Core
{

    public class GenericManager
    {
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


        public void RegisterNewCreateWorkflow(string entity, int order, Action<WorkflowInputParameter> action)
        {
            CreateWorkflows.Add(new WorkflowDefinition(entity, order, action));
        }

        public void RegisterNewUpdateWorkflow(string entity, int order, Action<WorkflowInputParameter> action)
        {
            UpdateWorkflows.Add(new WorkflowDefinition(entity, order, action));
        }

        public void RegisterNewDeleteWorkflow(string entity, int order, Action<WorkflowInputParameter> action)
        {
            DeleteWorkflows.Add(new WorkflowDefinition(entity, order, action));
        }

        public void RegisterNewAssociatekflow(string entity, int order, Action<WorkflowInputParameter> action)
        {
            AssociateWorkflow.Add(new WorkflowDefinition(entity, order, action));
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
                    item.Action.Invoke(workflowInput);
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
                    item.Action.Invoke(workflowInput);
                }
            }
            
        }

        public DataRowModel Retrieve(string entity, Guid id)
        {
            return RetrieveHandler.Execute(entity, id);
        }

        public DataSetModel RetrieveAll(string entity)
        {
            return RetrieveAllHandler.Execute(entity);
        }

        public DataSetModel RetrieveAllAssociated(string firstEntity, Guid mainId, string intersectionEntity, string secondEntity)
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
                    item.Action.Invoke(workflowInput);
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
                    item.Action.Invoke(workflowInput);
                }
            }
            return id;
        }

    }
}
