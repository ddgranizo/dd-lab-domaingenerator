using AutoMapper;
using DD.DomainGenerator;
using DD.DomainGenerator.Actions.Base;
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
using UIClient.Models;
using UIClient.Profiles;
using UIClient.ViewModels.Base;

namespace UIClient.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        public ProjectStateModel State { get { return GetValue<ProjectStateModel>(); } set { SetValue(value); } }
        public ProjectStateModel VirtualState { get { return GetValue<ProjectStateModel>(); } set { SetValue(value); } }
        public WorkspaceModel WorkSpace { get { return GetValue<WorkspaceModel>(); } set { SetValue(value); } }


        private readonly IMapper _mapper;
        private readonly Window _window;
        public ProjectManager ProjectManager { get; set; }

        public ICommand NewProjectCommand { get; set; }

        public MainViewModel(Window window)
        {
            _window = window;
            ProjectManager = new ProjectManager();
            WorkSpace = new WorkspaceModel(this);
            WorkSpace.NewActions = ProjectManager.ActionManager.Actions.OrderBy(k=>k.Name).ToList();
            _mapper = new Mapper(ConfigureMappingProfiles());
            InitializeCommands();
            RaiseStateChanged();
        }


        public void OnNewActionChanged(ActionBase action)
        {
            WorkSpace.NewActionParametersDefinitions = action.ActionParametersDefinition;
        }

        private void RaiseStateChanged()
        {
            State = _mapper.Map<ProjectStateModel>(ProjectManager.ProjectState);
            VirtualState = _mapper.Map<ProjectStateModel>(ProjectManager.VirtualProjectState);
        }

        private void InitializeCommands()
        {
            NewProjectCommand = new NewProjectCommand(this);
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
            });
        }
    }
}
