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

        public ProjectStateModel State { get { return GetValue<ProjectStateModel>(); } set { SetValue(value); } }
        public ProjectStateModel VirtualState { get { return GetValue<ProjectStateModel>(); } set { SetValue(value); } }
        public ProjectStateModel CurrentState { get { return GetValue<ProjectStateModel>(); } set { SetValue(value); } }
        public bool IsActiveVirtualState { get { return GetValue<bool>(); } set { SetValue(value); RaisePropertyChange(nameof(IsActiveRealState)); } }
        public bool IsActiveRealState { get { return GetValue<bool>(); } set { SetValue(value); } }

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
        public ProjectManager ProjectManager { get; set; }

        public readonly IStoredDataService<RecentProjects> StoredRecentProjectsService;

        public MainViewModel()
        {
            IsActiveVirtualState = true;
            StoredRecentProjectsService = new StoredRecentProjectsService();
            SetRecentProjects();
            InitializeCommands();
            InitializePorjectManager();
            InitializeMapper();
            RaiseStateChanged();
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

        private void InitializePorjectManager()
        {
            ProjectManager = new ProjectManager();
            ProjectManager.OnProjectChanged += ProjectManager_OnProjectChanged;
            ProjectManager.OnActionError += ProjectManager_OnActionError;
            NewActions = ProjectManager.ActionManager.Actions
                .OrderBy(k => k.Name)
                .ToList();
        }

        public void CleanErrors()
        {
            Errors = new List<ErrorExecutionActionModel>();
        }

        private void ProjectManager_OnActionError(object sender, DD.DomainGenerator.Events.ErrorExecutionActionEventArgs args)
        {
            var currentErrors = Errors;
            currentErrors.Add(Mapper.Map<ErrorExecutionActionModel>(args));
            Errors = currentErrors;
        }

        private void ProjectManager_OnProjectChanged(object sender, DD.DomainGenerator.Events.ProjectEventArgs args)
        {
            RaiseStateChanged();
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
                    SelectedActionForModifyParametersDefinitionsValues = action.Parameters;
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
                    ? ProjectManager._cryptoService.Encrypt((string)newValue)
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
                    ? ProjectManager._cryptoService.Encrypt((string)newValue)
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
                return VirtualState.Domains.Select(k => k.Name).ToList();
            }
            else if (action.IsSchemaSuggestion)
            {
                return VirtualState.Schemas.Select(k => k.Name).ToList();
            }
            else if (action.IsMicroServiceSuggestion)
            {
                return VirtualState.MicroServices.Select(k => k.Name).ToList();
            }
            else if (action.IsEnvironmentSuggestion)
            {
                return VirtualState.Environments.Select(k => k.Name).ToList();
            }
            else
            {
                return action.InputSuggestions;
            }
        }

        public void RaiseStateChanged()
        {
            State = Mapper.Map<ProjectStateModel>(ProjectManager.ProjectState);
            VirtualState = Mapper.Map<ProjectStateModel>(ProjectManager.VirtualProjectState);
            CurrentState = IsActiveVirtualState
                ? VirtualState
                : State;
            DeployActions = Mapper.Map<List<DeployActionUnitModel>>(ProjectManager.DeployActions);
        }

        
        //private bool AreTheSameDeployAction(DeployActionUnit deployActionUnit1, DeployActionUnit deployActionUnit2)
        //{
        //    var parametersFirst = string.Join(",", deployActionUnit1.act)
        //}


        public ICommand NewProjectCommand { get; set; }
        public ICommand AddActionCommand { get; set; }
        public ICommand RemoveActionCommand { get; set; }
        public ICommand SaveChangesCommand { get; set; }
        public ICommand RemoveSelectedNewActionCommand { get; set; }
        public ICommand ModifyActionRequestCommand { get; set; }
        public ICommand ModifyActionConfirmCommand { get; set; }
        public ICommand OpenFileCommand { get; set; }
        public ICommand ExecuteDeployActionUnitCommand { get; set; }
        public ICommand ChangeCurrentRealVirtualStateCommand { get; set; }
        public ICommand SetActionStateExecutedCommand { get; set; }
        public ICommand CheckDeployActionUnitCommand { get; set; }

        private void InitializeCommands()
        {
            NewProjectCommand = new NewProjectCommand(this);
            AddActionCommand = new AddActionCommand(this);
            RemoveActionCommand = new RemoveActionCommand(this);
            SaveChangesCommand = new SaveChangesCommand(this);
            RemoveSelectedNewActionCommand = new RemoveSelectedNewActionCommand(this);
            ModifyActionRequestCommand = new ModifyActionRequestCommand(this);
            ModifyActionConfirmCommand = new ModifyActionConfirmCommand(this);
            OpenFileCommand = new OpenFileCommand(this);
            ExecuteDeployActionUnitCommand = new ExecuteDeployActionUnitCommand(this);
            ChangeCurrentRealVirtualStateCommand = new ChangeCurrentRealVirtualStateCommand(this);
            SetActionStateExecutedCommand = new SetActionQueuedCommand(this);
            CheckDeployActionUnitCommand = new CheckDeployActionUnitCommand(this);

            RegisterCommand(NewProjectCommand);
            RegisterCommand(AddActionCommand);
            RegisterCommand(RemoveActionCommand);
            RegisterCommand(SaveChangesCommand);
            RegisterCommand(RemoveSelectedNewActionCommand);
            RegisterCommand(ModifyActionRequestCommand);
            RegisterCommand(ModifyActionConfirmCommand);
            RegisterCommand(OpenFileCommand);
            RegisterCommand(ExecuteDeployActionUnitCommand);
            RegisterCommand(ChangeCurrentRealVirtualStateCommand);
            RegisterCommand(SetActionStateExecutedCommand);
            RegisterCommand(CheckDeployActionUnitCommand);


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
            ProjectManager.NewProject();
            LastFileLoaded = null;
        }

        public void OpenFile(string file)
        {
            ProjectManager.OpenFile(file);
            LastFileLoaded = file;
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
            });
        }


        
    }
}
