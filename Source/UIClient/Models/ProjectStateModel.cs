using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using UIClient.Models.Base;

namespace UIClient.Models
{
    public class ProjectStateModel : BaseModel
    {
        public string Name { get { return GetValue<string>(); } set { SetValue(value); } }
        public List<ActionExecutionModel> Actions { get { return GetValue<List<ActionExecutionModel>>(); } set { ActionsCollection = SetCollection(value); } }
        public ObservableCollection<ActionExecutionModel> ActionsCollection { get; set; }

        public List<DomainModel> Domains { get { return GetValue<List<DomainModel>>(); } set { DomainsCollection = SetCollection(value); } }
        public ObservableCollection<DomainModel> DomainsCollection { get; set; }
        
        public List<SchemaModelModel> Schemas { get { return GetValue<List<SchemaModelModel>>(); } set { SetValue(value); SchemasCollection = SetCollection(value); } }
        public ObservableCollection<SchemaModelModel> SchemasCollection { get; set; }

        public List<SchemaModelModel> SchemaInDomain { get { return GetValue<List<SchemaModelModel>>(); } set { SetValue(value); SchemaInDomainCollection = SetCollection(value); } }
        public ObservableCollection<SchemaModelModel> SchemaInDomainCollection { get; set; }

        public string ReposPath { get { return GetValue<string>(); } set { SetValue(value); } }
        public List<AzurePipelineSettingModel> AzurePipelineSettings { get { return GetValue<List<AzurePipelineSettingModel>>(); } set { AzurePipelineSettingsCollection = SetCollection(value); } }
        public ObservableCollection<AzurePipelineSettingModel> AzurePipelineSettingsCollection { get; set; }
        public List<GithubSettingModel> GithubSettings { get { return GetValue<List<GithubSettingModel>>(); } set { GithubSettingsCollection = SetCollection(value); } }
        public ObservableCollection<GithubSettingModel> GithubSettingsCollection { get; set; }
        public ArchitectureSetupModel Architecture { get { return GetValue<ArchitectureSetupModel>(); } set { SetValue(value); } }
    }
}
