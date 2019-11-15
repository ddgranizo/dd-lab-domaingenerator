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
using UIClient.Models;
using UIClient.Models.Stored;
using UIClient.Profiles;
using UIClient.Services;
using UIClient.Services.Implementations;
using UIClient.ViewModels.Base;

namespace UIClient.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
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

        public ActionExecutionModel SelectedAction { get { return GetValue<ActionExecutionModel>(); } set { SetValue(value); } }
        public ActionExecutionModel SelectedActionForModify { get { return GetValue<ActionExecutionModel>(); } set { SetValue(value, ActionForModifyChanged); } }

        public List<ActionParameterDefinition> SelectedActionForModifyParametersDefinitions { get { return GetValue<List<ActionParameterDefinition>>(); } set { SetValue(value); UpdateListToCollection(value, SelectedActionForModifyParametersDefinitionsCollection); } }
        public ObservableCollection<ActionParameterDefinition> SelectedActionForModifyParametersDefinitionsCollection { get; set; } = new ObservableCollection<ActionParameterDefinition>();
        public Dictionary<string, object> SelectedActionForModifyParametersDefinitionsValues { get { return GetValue<Dictionary<string, object>>(); } set { SetValue(value); } }

        public List<string> RecentProjects { get { return GetValue<List<string>>(); } set { SetValue(value); UpdateListToCollection(value, RecentProjectsCollection); } }
        public ObservableCollection<string> RecentProjectsCollection { get; set; } = new ObservableCollection<string>();

        public List<ErrorExecutionActionModel> Errors { get { return GetValue<List<ErrorExecutionActionModel>>(); } set { SetValue(value); UpdateListToCollection(value, ErrorsCollection); } }
        public ObservableCollection<ErrorExecutionActionModel> ErrorsCollection { get; set; } = new ObservableCollection<ErrorExecutionActionModel>();

        public List<DeployActionUnitModel> DeployActions { get { return GetValue<List<DeployActionUnitModel>>(); } set { SetValue(value); UpdateListToCollection(value, DeployActionsCollection); } }
        public ObservableCollection<DeployActionUnitModel> DeployActionsCollection { get; set; } = new ObservableCollection<DeployActionUnitModel>();

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
            NewActions = _projectManager.ActionManager.Actions;
            SetRecentProjects();
            InitializeCommands();
            InitializeMapper();
            NewProject();
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

        private void ActionForModifyChanged(ActionExecutionModel action)
        {
            if (action != null)
            {
                var baseAction = NewActions.FirstOrDefault(k => k.Name == action.ActionName);
                if (baseAction != null)
                {
                    SelectedActionForModifyParametersDefinitionsValues = action.OutputParameters;
                    SelectedActionForModifyParametersDefinitions =
                        baseAction.ActionParametersDefinition
                        .Where(k => k.Name.ToLower() != "help")
                        .ToList();

                    var itemsSugestions = new Dictionary<string, List<string>>();
                    foreach (var item in baseAction.ActionParametersDefinition)
                    {
                        List<string> sugestions = GetActionParameterSugestions(item);
                        itemsSugestions.Add(item.Name, sugestions);
                    }
                    NewActionParametersSugestions = itemsSugestions;
                }
            }
            else
            {
                SelectedActionForModifyParametersDefinitions = new List<ActionParameterDefinition>();
                SelectedActionForModifyParametersDefinitionsValues = new Dictionary<string, object>();
            }
        }

        public void ModifyActionParameterValueChanged(ActionParameterDefinition parameter, object newValue)
        {
            var value = parameter.Type == ActionParameterDefinition.TypeValue.Password
                && !string.IsNullOrEmpty((string)newValue)
                    ? _cryptoService.Encrypt((string)newValue)
                    : newValue;

            if (SelectedActionForModifyParametersDefinitionsValues.ContainsKey(parameter.Name))
            {
                SelectedActionForModifyParametersDefinitionsValues[parameter.Name] = value;
            }
            else
            {
                SelectedActionForModifyParametersDefinitionsValues.Add(parameter.Name, value);
            }
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
                    List<string> sugestions = GetActionParameterSugestions(item);
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


        private List<string> GetActionParameterSugestions(ActionParameterDefinition action)
        {
            if (action.IsDomainSuggestion)
            {
                return ProjectState.Domains.Select(k => k.Name).ToList();
            }
            else if (action.IsSchemaSuggestion)
            {
                //TODO: filter schemas for domain
                return ProjectState.Domains.SelectMany(k=>k.Schemas).Select(k => k.Name).ToList();
            }
            else if (action.IsEnvironmentSuggestion)
            {
                return ProjectState.Environments.Select(k => k.Name).ToList();
            }
            else
            {
                return action.InputSuggestions;
            }
        }

        public void RaiseStateChanged()
        {
            ProjectStateModel = Mapper.Map<ProjectStateModel>(ProjectState);
        }


        public ICommand NewProjectCommand { get; set; }
        public ICommand RemoveActionCommand { get; set; }
        public ICommand SaveChangesCommand { get; set; }
        public ICommand RemoveSelectedNewActionCommand { get; set; }
        public ICommand ModifyActionRequestCommand { get; set; }
        public ICommand OpenFileCommand { get; set; }
        public ICommand ExecuteDeployActionUnitCommand { get; set; }
        public ICommand ExecuteActionCommand { get; set; }
        public ICommand CheckDeployActionUnitCommand { get; set; }
        public ICommand CheckAndExecuteAboveAndThisDeployActionUnitCommand { get; set; }
        public ICommand OpenAddActionDialogCommand { get; set; }
        public ICommand CloseAddActionDialogCommand { get; set; }
        private void InitializeCommands()
        {
            NewProjectCommand = new NewProjectCommand(this);
            RemoveActionCommand = new RemoveActionCommand(this);
            SaveChangesCommand = new SaveChangesCommand(this);
            RemoveSelectedNewActionCommand = new RemoveSelectedNewActionCommand(this);
            ModifyActionRequestCommand = new ModifyActionRequestCommand(this);
            OpenFileCommand = new OpenFileCommand(this);
            ExecuteDeployActionUnitCommand = new ExecuteDeployActionUnitCommand(this);
            ExecuteActionCommand = new ExecuteActionCommand(this);
            CheckDeployActionUnitCommand = new CheckDeployActionUnitCommand(this);
            CheckAndExecuteAboveAndThisDeployActionUnitCommand = new CheckAndExecuteAboveAndThisDeployActionUnitCommand(this);
            OpenAddActionDialogCommand = new OpenAddActionDialogCommand(this);
            CloseAddActionDialogCommand = new CloseAddActionDialogCommand(this);

            RegisterCommand(NewProjectCommand);
            RegisterCommand(RemoveActionCommand);
            RegisterCommand(SaveChangesCommand);
            RegisterCommand(RemoveSelectedNewActionCommand);
            RegisterCommand(ModifyActionRequestCommand);
            RegisterCommand(OpenFileCommand);
            RegisterCommand(ExecuteDeployActionUnitCommand);
            RegisterCommand(ExecuteActionCommand);
            RegisterCommand(CheckDeployActionUnitCommand);
            RegisterCommand(CheckAndExecuteAboveAndThisDeployActionUnitCommand);
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
                mc.AddProfile(new ActionExecutionProfile());
                mc.AddProfile(new AzurePipelineSettingProfile());
                mc.AddProfile(new DomainProfile());
                mc.AddProfile(new EnvironmentProfile());
                mc.AddProfile(new GithubSettingProfile());
                mc.AddProfile(new ProjectStateProfile());
                mc.AddProfile(new SchemaModelProfile());
                mc.AddProfile(new SchemaModelPropertyProfile());
                mc.AddProfile(new ServiceProfile());
                mc.AddProfile(new UseCaseProfile());
                mc.AddProfile(new SchemaInDomainProfile());
                mc.AddProfile(new ErrorExecutionActionProfile());
                mc.AddProfile(new ActionBaseProfile());
                mc.AddProfile(new DomainInMicroServiceProfile());
                mc.AddProfile(new MicroServiceProfile());
                mc.AddProfile(new DeployActionUnitProfile());
                mc.AddProfile(new SettingProfile());
                mc.AddProfile(new SchemaViewProfile());
                mc.AddProfile(new ViewParameterProfile());
            });
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
