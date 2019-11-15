using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using UIClient.Models.Base;

namespace UIClient.Models
{
    public class ProjectStateModel : BaseModel
    {
        public string Name { get { return GetValue<string>(); } set { SetValue(value); } }
        public string Path { get { return GetValue<string>(); } set { SetValue(value); } }
        public string NameSpace { get { return GetValue<string>(); } set { SetValue(value); } }


        public List<SettingModel> Settings { get { return GetValue<List<SettingModel>>(); } set { SetValue(value); UpdateListToCollection(value, SettingsCollection); } }
        public ObservableCollection<SettingModel> SettingsCollection { get; set; } = new ObservableCollection<SettingModel>();
        
        public List<AzurePipelineSettingModel> AzurePipelineSettings { get { return GetValue<List<AzurePipelineSettingModel>>(); } set { SetValue(value); UpdateListToCollection(value, AzurePipelineSettingsCollection); } }
        public ObservableCollection<AzurePipelineSettingModel> AzurePipelineSettingsCollection { get; set; } = new ObservableCollection<AzurePipelineSettingModel>();

        public List<GithubSettingModel> GithubSettings { get { return GetValue<List<GithubSettingModel>>(); } set { SetValue(value); UpdateListToCollection(value, GithubSettingsCollection); } }
        public ObservableCollection<GithubSettingModel> GithubSettingsCollection { get; set; } = new ObservableCollection<GithubSettingModel>();

        public List<EnvironmentModel> Environments { get { return GetValue<List<EnvironmentModel>>(); } set { SetValue(value); UpdateListToCollection(value, EnvironmentsCollection); } }
        public ObservableCollection<EnvironmentModel> EnvironmentsCollection { get; set; } = new ObservableCollection<EnvironmentModel>();

        public List<DomainModel> Domains { get { return GetValue<List<DomainModel>>(); } set { SetValue(value); UpdateListToCollection(value, DomainsCollection); } }
        public ObservableCollection<DomainModel> DomainsCollection { get; set; } = new ObservableCollection<DomainModel>();



    }
}
