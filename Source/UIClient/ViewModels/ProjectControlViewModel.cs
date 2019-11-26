using DD.DomainGenerator.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Xml.Serialization;
using UIClient.Models;
using UIClient.UserControls;
using UIClient.ViewModels.Base;

namespace UIClient.ViewModels
{
    public class ProjectControlViewModel : BaseViewModel
    {

        public enum MainViewSelector
        {
            Tree = 10,
            UseCaseList = 20,
        }

        public MainViewSelector CurrentMainView { get { return GetValue<MainViewSelector>(); } set { SetValue(value); } }


        public DomainEventManager EventManager { get { return GetValue<DomainEventManager>(); } set { SetValue(value); } }

        public ProjectStateModel ProjectState { get { return GetValue<ProjectStateModel>(); } set { SetValue(value, UpdatedProjectState); } }

        public List<UseCaseListItemModel> UseCaseListItems { get { return GetValue<List<UseCaseListItemModel>>(); } set { SetValue(value, UpdatedUseCaseListItems); } }

        public List<UseCaseListItemModel> FilteredUseCaseListItems { get { return GetValue<List<UseCaseListItemModel>>(); } set { SetValue(value); UpdateListToCollection(value, FilteredUseCaseListItemsCollection); } }
        public ObservableCollection<UseCaseListItemModel> FilteredUseCaseListItemsCollection { get; set; } = new ObservableCollection<UseCaseListItemModel>();


        public bool IsGeneralOpen { get { return GetValue<bool>(); } set { SetValue(value); } }
        public bool IsDomainsOpen { get { return GetValue<bool>(); } set { SetValue(value); } }
        public bool IsSettingsOpen { get { return GetValue<bool>(); } set { SetValue(value); } }
        public bool IsAzurePipelineSettingsOpen { get { return GetValue<bool>(); } set { SetValue(value); } }
        public bool IsGithubSettingsOpen { get { return GetValue<bool>(); } set { SetValue(value); } }
        public bool IsEnvironmentsOpen { get { return GetValue<bool>(); } set { SetValue(value); } }

        public bool IsRadioTreeChecked { get { return GetValue<bool>(); } set { SetValue(value); } }
        public bool IsRadioUseCaseListChecked { get { return GetValue<bool>(); } set { SetValue(value); } }

        public string SearchText { get { return GetValue<string>(); } set { SetValue(value, UpdatedFilterText);  } }


        private ProjectControlView _view;

		public ProjectControlViewModel()
        {
			
        }

        public void Initialize(ProjectControlView v)
        {
			_view = v;
            IsRadioTreeChecked = true;
        }


        public void FilterUseCaseListItems(List<UseCaseListItemModel> allUseCases, string searchText)
        {
            FilteredUseCaseListItems =
                string.IsNullOrEmpty(searchText) 
                ? allUseCases
                : allUseCases
                    .Where(k => k.CompleteDisplayName.ToLower().Contains(searchText.ToLower()))
                    .ToList();
        }

        public void UpdatedFilterText(string searchText)
        {
            FilterUseCaseListItems(UseCaseListItems, searchText);
        }

        public void UpdatedUseCaseListItems(List<UseCaseListItemModel> items)
        {
            FilterUseCaseListItems(items, SearchText);
        }

        public void UpdatedProjectState(ProjectStateModel model)
        {
            UseCaseListItems = model.Domains
                .SelectMany(domain => domain.Schemas
                    .SelectMany(schema => schema.UseCases
                        .Select(useCase => new UseCaseListItemModel() { Domain = domain, Schema = schema, UseCase = useCase }))).ToList(); ;
        }

    }
}
