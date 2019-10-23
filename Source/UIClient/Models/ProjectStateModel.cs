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
        public List<ActionExecutionModel> Actions { get { return GetValue<List<ActionExecutionModel>>(); } set { SetValue(value); UpdateListToCollection(value, ActionsCollection); } }
        public ObservableCollection<ActionExecutionModel> ActionsCollection { get; set; } = new ObservableCollection<ActionExecutionModel>();

        public List<DomainModel> Domains { get { return GetValue<List<DomainModel>>(); } set { DomainsCollection = SetCollection(value); } }
        public ObservableCollection<DomainModel> DomainsCollection { get; set; } = new ObservableCollection<DomainModel>();
        
        public List<SchemaModelModel> Schemas { get { return GetValue<List<SchemaModelModel>>(); } set { SetValue(value); UpdateListToCollection(value, SchemasCollection); } }
        public ObservableCollection<SchemaModelModel> SchemasCollection { get; set; } = new ObservableCollection<SchemaModelModel>();

        public List<SchemaInDomainModel> SchemaInDomains { get { return GetValue<List<SchemaInDomainModel>>(); } set { SetValue(value); UpdateListToCollection(value, SchemaInDomainsCollection); } }
        public ObservableCollection<SchemaInDomainModel> SchemaInDomainsCollection { get; set; } = new ObservableCollection<SchemaInDomainModel>();

        public string ReposPath { get { return GetValue<string>(); } set { SetValue(value); } }
        public List<AzurePipelineSettingModel> AzurePipelineSettings { get { return GetValue<List<AzurePipelineSettingModel>>(); } set { SetValue(value); UpdateListToCollection(value, AzurePipelineSettingsCollection); } }
        
        public ObservableCollection<AzurePipelineSettingModel> AzurePipelineSettingsCollection { get; set; } = new ObservableCollection<AzurePipelineSettingModel>();
        public List<GithubSettingModel> GithubSettings { get { return GetValue<List<GithubSettingModel>>(); } set { SetValue(value); UpdateListToCollection(value, GithubSettingsCollection); } }

        public ObservableCollection<GithubSettingModel> GithubSettingsCollection { get; set; } = new ObservableCollection<GithubSettingModel>();
        public ArchitectureSetupModel Architecture { get { return GetValue<ArchitectureSetupModel>(); } set { SetValue(value); } }
    }
}
