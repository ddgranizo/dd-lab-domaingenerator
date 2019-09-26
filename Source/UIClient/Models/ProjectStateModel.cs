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
        public DomainModel Domain { get { return GetValue<DomainModel>(); } set { SetValue(value); } }
        public string ReposPath { get { return GetValue<string>(); } set { SetValue(value); } }
        public List<AzurePipelineSettingModel> AzurePipelineSettings { get { return GetValue<List<AzurePipelineSettingModel>>(); } set { AzurePipelineSettingsCollection = SetCollection(value); } }
        public ObservableCollection<AzurePipelineSettingModel> AzurePipelineSettingsCollection { get; set; }
        public List<GithubSettingModel> GithubSettings { get { return GetValue<List<GithubSettingModel>>(); } set { GithubSettingsCollection = SetCollection(value); } }
        public ObservableCollection<GithubSettingModel> GithubSettingsCollection { get; set; }
        public ArchitectureSetupModel Architecture { get { return GetValue<ArchitectureSetupModel>(); } set { SetValue(value); } }
    }
}
