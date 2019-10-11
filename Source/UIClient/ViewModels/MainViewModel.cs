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
using UIClient.Profiles;
using UIClient.ViewModels.Base;

namespace UIClient.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        public string LastFileLoaded { get; set; }

        public ProjectStateModel State { get { return GetValue<ProjectStateModel>(); } set { SetValue(value); } }
        public ProjectStateModel VirtualState { get { return GetValue<ProjectStateModel>(); } set { SetValue(value); } }
        public List<ActionBase> NewActions { get { return GetValue<List<ActionBase>>(); } set { SetValue(value); UpdateListToCollection(value, NewActionsCollection); } }
        public ObservableCollection<ActionBase> NewActionsCollection { get; set; } = new ObservableCollection<ActionBase>();
        public ActionBase SelectedNewAction { get { return GetValue<ActionBase>(); } set { SetValue(value, OnNewActionChanged); } }
        public List<ActionParameterDefinition> NewActionParametersDefinitions { get { return GetValue<List<ActionParameterDefinition>>(); } set { SetValue(value); UpdateListToCollection(value, NewActionParametersDefinitionsCollection); } }
        public ObservableCollection<ActionParameterDefinition> NewActionParametersDefinitionsCollection { get; set; } = new ObservableCollection<ActionParameterDefinition>();
        
        public Dictionary<string, object> NewActionParametersDefinitionsValues { get { return GetValue<Dictionary<string, object>>(); } set { SetValue(value); } }

        public ActionExecutionModel SelectedAction { get { return GetValue<ActionExecutionModel>(); } set { SetValue(value); } }
        public ActionExecutionModel SelectedActionForModify { get { return GetValue<ActionExecutionModel>(); } set { SetValue(value, ActionForModifyChanged); } }

        public List<ActionParameterDefinition> SelectedActionForModifyParametersDefinitions { get { return GetValue<List<ActionParameterDefinition>>(); } set { SetValue(value); UpdateListToCollection(value, SelectedActionForModifyParametersDefinitionsCollection); } }
        public ObservableCollection<ActionParameterDefinition> SelectedActionForModifyParametersDefinitionsCollection { get; set; } = new ObservableCollection<ActionParameterDefinition>();
        public Dictionary<string, object> SelectedActionForModifyParametersDefinitionsValues { get { return GetValue<Dictionary<string, object>>(); } set { SetValue(value); } }


        public IMapper Mapper { get; set; }
        private Window _window;
        public ProjectManager ProjectManager { get; set; }

        public MainViewModel()
        {
            InitializeCommands();
            InitializePorjectManager();
            InitializeMapper();
            RaiseStateChanged();
        }


        public void Initialize(Window window)
        {
            _window = window;
        }

        private void InitializePorjectManager()
        {
            ProjectManager = new ProjectManager();
            NewActions = ProjectManager.ActionManager.Actions
                .OrderBy(k => k.Name)
                .ToList();
        }

        private void InitializeMapper()
        {
            Mapper = new Mapper(ConfigureMappingProfiles());
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
            if (SelectedActionForModifyParametersDefinitionsValues.ContainsKey(parameter.Name))
            {
                SelectedActionForModifyParametersDefinitionsValues[parameter.Name] = newValue;
            }
            else
            {
                SelectedActionForModifyParametersDefinitionsValues.Add(parameter.Name, newValue);
            }
        }


        public void NewActionParameterValueChanged(ActionParameterDefinition parameter, object newValue)
        {
            if (NewActionParametersDefinitionsValues.ContainsKey(parameter.Name))
            {
                NewActionParametersDefinitionsValues[parameter.Name] = newValue;
            }
            else
            {
                NewActionParametersDefinitionsValues.Add(parameter.Name, newValue);
            }
        }

        public void OnNewActionChanged(ActionBase action)
        {
            if (action != null)
            {
                var itemsValues = new Dictionary<string, object>();
                foreach (var item in action.ActionParametersDefinition)
                {
                    object value = item.DefaultValue;
                    itemsValues.Add(item.Name, value);
                }
                NewActionParametersDefinitionsValues = itemsValues;

                NewActionParametersDefinitions =
                   action.ActionParametersDefinition
                   .Where(k => k.Name.ToLower() != "help")
                   .ToList();
            }
            else
            {
                NewActionParametersDefinitions = new List<ActionParameterDefinition>();
                NewActionParametersDefinitionsValues = new Dictionary<string, object>();
            }
        }

        public void RaiseStateChanged()
        {
            State = Mapper.Map<ProjectStateModel>(ProjectManager.ProjectState);
            VirtualState = Mapper.Map<ProjectStateModel>(ProjectManager.VirtualProjectState);
        }


        public ICommand NewProjectCommand { get; set; }
        public ICommand AddActionCommand { get; set; }
        public ICommand RemoveActionCommand { get; set; }
        public ICommand SaveChangesCommand { get; set; }
        public ICommand RemoveSelectedNewActionCommand { get; set; }
        public ICommand ModifyActionRequestCommand { get; set; }
        public ICommand ModifyActionConfirmCommand { get; set; }
        

        private void InitializeCommands()
        {
            NewProjectCommand = new NewProjectCommand(this);
            AddActionCommand = new AddActionCommand(this);
            RemoveActionCommand = new RemoveActionCommand(this);
            SaveChangesCommand = new SaveChangesCommand(this);
            RemoveSelectedNewActionCommand = new RemoveSelectedNewActionCommand(this);
            ModifyActionRequestCommand = new ModifyActionRequestCommand(this);
            ModifyActionConfirmCommand = new ModifyActionConfirmCommand(this);

            RegisterCommand(NewProjectCommand);
            RegisterCommand(AddActionCommand);
            RegisterCommand(RemoveActionCommand);
            RegisterCommand(SaveChangesCommand);
            RegisterCommand(RemoveSelectedNewActionCommand);
            RegisterCommand(ModifyActionRequestCommand);
            RegisterCommand(ModifyActionConfirmCommand);

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
                RaiseOkCancelDialog("Do you want to open this new project?", "Open project", () =>
                {
                    ProjectManager.OpenFile(file);
                    LastFileLoaded = file;
                    RaiseStateChanged();
                });
            }
            catch (Exception ex)
            {
                RaiseError(ex.Message);
            }
        }

        private MapperConfiguration ConfigureMappingProfiles()
        {
            return new MapperConfiguration(mc =>
            {
                mc.AddProfile(new ActionExecutionProfile());
                mc.AddProfile(new ArchitectureSetupProfile());
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
            });
        }
    }
}
