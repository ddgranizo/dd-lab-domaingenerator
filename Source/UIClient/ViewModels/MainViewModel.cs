using AutoMapper;
using DD.DomainGenerator;
using DD.DomainGenerator.Actions.Base;
using DD.DomainGenerator.Models;
using DD.DomainGenerator.Services;
using DD.DomainGenerator.Services.Implementations;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;
using UIClient.Commands;
using UIClient.Commands.Base;
using UIClient.Events;
using UIClient.Models;
using UIClient.Models.Inputs;
using UIClient.Models.Stored;
using UIClient.Profiles;
using UIClient.Services;
using UIClient.Services.Implementations;
using UIClient.ViewModels.Base;

namespace UIClient.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        public enum DetailViewSelector
        {
            UseCase = 10,
        }

        public string LastFileLoaded { get; set; }
        public ProjectState ProjectState { get; set; }
        public bool IsActionDialogOpen { get { return GetValue<bool>(); } set { SetValue(value); } }
        public string MessageDialog { get { return GetValue<string>(); } set { SetValue(value); } }
        public bool IsDetailSectionVisible { get { return GetValue<bool>(); } set { SetValue(value); } }

        public ProjectStateModel ProjectStateModel { get { return GetValue<ProjectStateModel>(); } set { SetValue(value); } }

        public List<ActionBase> NewActions { get { return GetValue<List<ActionBase>>(); } set { SetValue(value); UpdateListToCollection(value, NewActionsCollection); } }
        public ObservableCollection<ActionBase> NewActionsCollection { get; set; } = new ObservableCollection<ActionBase>();

        public ActionBase SelectedNewAction { get { return GetValue<ActionBase>(); } set { SetValue(value, OnNewActionChanged); } }
        public List<ActionParameterDefinition> NewActionParametersDefinitions { get { return GetValue<List<ActionParameterDefinition>>(); } set { SetValue(value); UpdateListToCollection(value, NewActionParametersDefinitionsCollection); } }
        public ObservableCollection<ActionParameterDefinition> NewActionParametersDefinitionsCollection { get; set; } = new ObservableCollection<ActionParameterDefinition>();

        public Dictionary<string, object> NewActionParametersDefinitionsValues { get { return GetValue<Dictionary<string, object>>(); } set { SetValue(value); } }
        public Dictionary<string, List<string>> NewActionParametersSugestions { get { return GetValue<Dictionary<string, List<string>>>(); } set { SetValue(value); } }


        public List<string> RecentProjects { get { return GetValue<List<string>>(); } set { SetValue(value); UpdateListToCollection(value, RecentProjectsCollection); } }
        public ObservableCollection<string> RecentProjectsCollection { get; set; } = new ObservableCollection<string>();

        public DomainEventManager EventManager { get; set; }

        public UseCaseModel SelectedUseCase { get { return GetValue<UseCaseModel>(); } set { SetValue(value); } }


        public Guid GenericFormRequestId { get { return GetValue<Guid>(); } set { SetValue(value); } }
        public Dictionary<string, object> GenericFormRequestInitialValues { get { return GetValue<Dictionary<string, object>>(); } set { SetValue(value); } }
        public GenericFormModel GenericFormRequestModel { get { return GetValue<GenericFormModel>(); } set { SetValue(value); } }
        public bool IsGenericFormDialogOpen { get { return GetValue<bool>(); } set { SetValue(value); } }


        public DetailViewSelector CurrentDetailView { get { return GetValue<DetailViewSelector>(); } set { SetValue(value); } }

        public IMapper Mapper { get; set; }
        private Window _window;

        private readonly ProjectManager _projectManager;
        public readonly IStoredDataService<RecentProjects> StoredRecentProjectsService;
        private readonly ICryptoService _cryptoService;
        private readonly IRegistryService _registryService;
        private readonly IFileService _fileService;
        private readonly IJsonParserService _jsonParserService;

        public MainViewModel()
        {
            StoredRecentProjectsService = new StoredRecentProjectsService();
            _registryService = new RegistryService();
            _cryptoService = new CryptoService(_registryService);
            _fileService = new FileService();
            _jsonParserService = new JsonParserService();
            _projectManager = new ProjectManager();
            EventManager = new DomainEventManager();
            EventManager.OnSelectedUseCase += EventManager_OnSelectedUseCase;
            EventManager.OnGenericFormInputRequested += EventManager_OnGenericFormInputRequested;
            NewActions = _projectManager.ActionManager.Actions.OrderBy(k => k.Name).ToList();
            SetRecentProjects();
            InitializeCommands();
            InitializeMapper();
            NewProject();
        }

        private void EventManager_OnGenericFormInputRequested(object sender, GenericFormRequestEventArgs args)
        {
            GenericFormRequestId = args.RequestId;
            GenericFormRequestModel = args.FormModel;
            SetGenericFormDialog();
        }

        private void EventManager_OnSelectedUseCase(object sender, UseCaseEventArgs args)
        {
            SelectedUseCase = _jsonParserService.Objectify<UseCaseModel>(_jsonParserService.Stringfy<UseCaseModel>(args.UseCase));
            CurrentDetailView = DetailViewSelector.UseCase;
        }


        public void GenericFormValueConfirmed(Dictionary<string, object> values)
        {
            UnsetGenericFormDialog();
            EventManager.RaiseOnGenericFormInputResponsedEvent(GenericFormRequestId, true, values);
        }

        public void GenericFormValueCancelled()
        {
            UnsetGenericFormDialog();
            EventManager.RaiseOnGenericFormInputResponsedEvent(GenericFormRequestId, false, null);
        }

        public void SavedUseCaseFromEditor(UseCaseModel model)
        {
            
        }

        private void SetRecentProjects()
        {
            var allRecentProjects = StoredRecentProjectsService.GetStoredData()
                            .Paths;
            allRecentProjects.Reverse();
            RecentProjects = allRecentProjects.Count <= 10
                ? allRecentProjects
                : allRecentProjects.Take(10).ToList();
        }

        public void Initialize(Window window)
        {
            _window = window;
        }


        private void InitializeMapper()
        {
            Mapper = new Mapper(ConfigureMappingProfiles());
        }

        public void AddNewRecentFile(string path)
        {
            var currentProjectsStored = StoredRecentProjectsService.GetStoredData();
            var alreadyInList = currentProjectsStored.Paths.FirstOrDefault(k => k == path);
            if (alreadyInList != null)
            {
                currentProjectsStored.Paths.Remove(alreadyInList);
            }
            currentProjectsStored.Paths.Add(path);
            StoredRecentProjectsService.SaveStoredData(currentProjectsStored);
        }



        public void NewActionParameterValueChanged(ActionParameterDefinition parameter, object newValue)
        {
            var value = parameter.Type == ActionParameterDefinition.TypeValue.Password
                && !string.IsNullOrEmpty((string)newValue)
                    ? _cryptoService.Encrypt((string)newValue)
                    : newValue;
            if (NewActionParametersDefinitionsValues.ContainsKey(parameter.Name))
            {
                NewActionParametersDefinitionsValues[parameter.Name] = value;
            }
            else
            {
                NewActionParametersDefinitionsValues.Add(parameter.Name, value);
            }

            UpdateDynamicSuggestions(parameter);
        }

        private void UpdateDynamicSuggestions(ActionParameterDefinition triggerParameter)
        {
            var currentSuggestions = NewActionParametersSugestions;
            var modified = false;
            foreach (var actionParameter in SelectedNewAction.ActionParametersDefinition)
            {
                if (actionParameter.InputSuggestionsHandler != null
                    && currentSuggestions.ContainsKey(actionParameter.Name)
                    && actionParameter.Name != triggerParameter.Name)
                {
                    var suggestions = actionParameter.InputSuggestionsHandler.Invoke(ProjectState, NewActionParametersDefinitionsValues);
                    currentSuggestions[actionParameter.Name] = suggestions;
                    modified = true;
                }
            }
            if (modified)
            {
                NewActionParametersSugestions = new Dictionary<string, List<string>>();
                NewActionParametersSugestions = currentSuggestions;
            }
        }

        public void OnNewActionChanged(ActionBase action)
        {
            if (action != null)
            {
                var itemsValues = new Dictionary<string, object>();
                var itemsSugestions = new Dictionary<string, List<string>>();

                foreach (var item in action.ActionParametersDefinition)
                {
                    object value = item.DefaultValue;
                    List<string> sugestions = GetActionParameterSugestions(item, ProjectState, itemsValues);
                    itemsValues.Add(item.Name, value);
                    itemsSugestions.Add(item.Name, sugestions);
                }
                NewActionParametersDefinitionsValues = itemsValues;
                NewActionParametersSugestions = itemsSugestions;

                NewActionParametersDefinitions =
                   action.ActionParametersDefinition
                   .Where(k => k.Name.ToLower() != "help")
                   .ToList();
            }
            else
            {
                NewActionParametersDefinitions = new List<ActionParameterDefinition>();
                NewActionParametersDefinitionsValues = new Dictionary<string, object>();
                NewActionParametersSugestions = new Dictionary<string, List<string>>();
            }
        }

        private List<string> GetActionParameterSugestions(ActionParameterDefinition action, ProjectState projectState, Dictionary<string, object> otherValues)
        {
            if (action.IsDomainSuggestion)
            {
                return ProjectState.Domains.Select(k => k.Name).ToList();
            }
            else if (action.IsSchemaSuggestion)
            {
                return ProjectState.Domains.SelectMany(k => k.Schemas).Select(k => k.Name).ToList();
            }
            else if (action.IsEnvironmentSuggestion)
            {
                return ProjectState.Environments.Select(k => k.Name).ToList();
            }
            else if (action.InputSuggestions != null && action.InputSuggestions.Count > 0)
            {
                return action.InputSuggestions;
            }
            else if (action.InputSuggestionsHandler != null)
            {
                return new List<string>();
            }
            return new List<string>();
        }

        public void RaiseStateChanged()
        {
            ProjectStateModel = Mapper.Map<ProjectStateModel>(ProjectState);
        }

        public ICommand NewProjectCommand { get; set; }
        public ICommand SaveChangesCommand { get; set; }
        public ICommand RemoveSelectedNewActionCommand { get; set; }
        public ICommand OpenFileCommand { get; set; }
        public ICommand ExecuteActionCommand { get; set; }
        public ICommand OpenAddActionDialogCommand { get; set; }
        public ICommand CloseAddActionDialogCommand { get; set; }
        private void InitializeCommands()
        {
            NewProjectCommand = new NewProjectCommand(this);
            SaveChangesCommand = new SaveChangesCommand(this);
            RemoveSelectedNewActionCommand = new RemoveSelectedNewActionCommand(this);
            OpenFileCommand = new OpenFileCommand(this);
            ExecuteActionCommand = new ExecuteActionCommand(this);
            OpenAddActionDialogCommand = new OpenAddActionDialogCommand(this);
            CloseAddActionDialogCommand = new CloseAddActionDialogCommand(this);

            RegisterCommand(NewProjectCommand);
            RegisterCommand(SaveChangesCommand);
            RegisterCommand(RemoveSelectedNewActionCommand);
            RegisterCommand(OpenFileCommand);
            RegisterCommand(ExecuteActionCommand);
            RegisterCommand(OpenAddActionDialogCommand);
            RegisterCommand(CloseAddActionDialogCommand);

            RaiseCanExecuteCommandChanged();
        }

        public void DraggedFiles(string[] files)
        {
            OpenFile(files);
        }

        private void OpenFile(string[] files)
        {
            try
            {
                if (files.Length > 1)
                {
                    throw new Exception("Select only one file");
                }
                var file = files[0];
                base.RaiseOkCancelDialog("Do you want to open this new project?", "Open project", () =>
                {
                    OpenFile(file);
                });
            }
            catch (Exception ex)
            {
                RaiseError(ex.Message);
            }
        }


        public void NewProject()
        {
            ProjectState = new ProjectState();
            LastFileLoaded = null;
            RaiseStateChanged();
        }

        public void OpenFile(string file)
        {
            var absolutePath = _fileService.GetAbsoluteCurrentPath(file);
            if (Path.GetExtension(absolutePath) != ".json")
            {
                throw new Exception("Invalid extension file. Select .json file");
            }
            var json = _fileService.OpenFile(absolutePath);
            LastFileLoaded = absolutePath;
            ProjectState = _jsonParserService.Objectify<ProjectState>(json);
            RaiseStateChanged();
        }

        public void SaveChanges(string path)
        {
            var absolutePath = _fileService.GetAbsoluteCurrentPath(path);
            var json = _jsonParserService.Stringfy(ProjectState);
            _fileService.SaveFile(absolutePath, json);
        }

        private MapperConfiguration ConfigureMappingProfiles()
        {
            return new MapperConfiguration(mc =>
            {
                mc.AddProfile(new AzurePipelineSettingProfile());
                mc.AddProfile(new DomainProfile());
                mc.AddProfile(new EnvironmentProfile());
                mc.AddProfile(new GithubSettingProfile());
                mc.AddProfile(new ProjectStateProfile());
                mc.AddProfile(new SchemaModelProfile());
                mc.AddProfile(new SchemaModelPropertyProfile());
           
                mc.AddProfile(new ActionBaseProfile());
                mc.AddProfile(new SettingProfile());
                mc.AddProfile(new SchemaViewProfile());
                mc.AddProfile(new ViewParameterProfile());
                mc.AddProfile(new RepositoryProfile());
                mc.AddProfile(new ModelProfile());
                
                mc.AddProfile(new DataParameterProfile());
                mc.AddProfile(new ExecutionSentenceBaseProfile());
                
                mc.AddProfile(new DataParameterValueProfile());

                mc.AddProfile(new UseCaseProfile());
                mc.AddProfile(new UseCaseExecutionProfile());
                mc.AddProfile(new UseCaseExecutionContextProfile());
                mc.AddProfile(new UseCaseLinkExecutionParameterProfile());
                mc.AddProfile(new UseCaseLinkInputExecutionParameterProfile());
                mc.AddProfile(new UseCaseLinkOutputExecutionParameterProfile());
                mc.AddProfile(new UseCaseExecutionContextParameterProfile());
                mc.AddProfile(new UseCaseExecutionSentenceProfile());

            });
        }

        public void SetGenericFormDialog()
        {
            IsGenericFormDialogOpen = true;
        }

        public void UnsetGenericFormDialog()
        {
            IsGenericFormDialogOpen = false;
        }


        public void SetActionDialog()
        {
            IsActionDialogOpen = true;
        }

        public void UnsetDialog()
        {
            IsActionDialogOpen = false;
        }


        public void OpenMenuForAction(string actionName)
        {
            var actionBase = NewActions.FirstOrDefault(k => k.Name == actionName)
                 ?? throw new Exception($"Can't find action named {actionName}");
            SelectedNewAction = actionBase;
            SetActionDialog();
        }
    }
}
