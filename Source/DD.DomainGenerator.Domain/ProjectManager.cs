using DD.DomainGenerator.Actions;
using DD.DomainGenerator.Actions.Architecture;
using DD.DomainGenerator.Actions.AzurePipelines;
using DD.DomainGenerator.Actions.Base;
using DD.DomainGenerator.Actions.Domains;
using DD.DomainGenerator.Actions.Github;
using DD.DomainGenerator.Actions.Project;
using DD.DomainGenerator.Actions.Schemas;
using DD.DomainGenerator.Actions.Schemas.UseCases;
using DD.DomainGenerator.Events;
using DD.DomainGenerator.Extensions;
using DD.DomainGenerator.Models;
using DD.DomainGenerator.Services;
using DD.DomainGenerator.Services.Implementations;
using DD.DomainGenerator.Utilities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace DD.DomainGenerator
{

    public delegate void ProjectHandler(object sender, ProjectEventArgs args);
    public class ProjectManager
    {
        public event ProjectHandler OnProjectChanged;
        public event ErrorExecutionActionHandler OnActionError;
        public IRegistryService _registryService { get; set; }
        public ICryptoService _cryptoService { get; set; }
        public enum ProjectInfrastructureAction
        {
            Help = 0,
            SaveChanges = 1,
            OpenFile = 2,
            CommitChanges = 10,
            DiscardLastQueuedChange = 11,
            DiscardAllQueuedChanges = 12,
            ShowProjectState = 20,
            ShowVirtualProjectState = 21,
            NewProject = 89,
            ExitPromptMode = 99,
        }

        public ProjectState ProjectState { get; set; }
        public ProjectState VirtualProjectState { get; set; }
        public ActionManager ActionManager { get; }

        private readonly IFileService _fileService;

        public string LastFilePath { get; set; }

        public ReadLineAutocompleteHandler ReadLineSuggestionHandler { get; set; }
        public ProjectManager()
        {
            ProjectState = new ProjectState();
            VirtualProjectState = new ProjectState();
            _fileService = new FileService();
            _registryService = new RegistryService();
            _cryptoService = new CryptoService(_registryService);
            ActionManager = GetActionManager();

            ActionManager.OnQueueAction += ActionManager_OnQueuedAction;
            ActionManager.OnLog += ActionManager_OnLog;
            ActionManager.OnErrorExecution += ActionManager_OnErrorExecution;
            
        }

        private void ActionManager_OnErrorExecution(object sender, ErrorExecutionActionEventArgs args)
        {
            OnActionError?.Invoke(sender, args);
        }

        public void PromptMode()
        {
            ReadLineSuggestionHandler = new ReadLineAutocompleteHandler(ActionManager.Actions);
            ReadLine.AutoCompletionHandler = ReadLineSuggestionHandler;
            string input = string.Empty;
            ProjectInfrastructureAction lastAction = ProjectInfrastructureAction.Help;
            do
            {
                Console.WriteLine("Type action number or command name:");
                foreach (var value in Enum.GetValues(typeof(ProjectInfrastructureAction)))
                {
                    Console.WriteLine($" - {(int)value} - {value}");
                }
                input = ReadLine.Read();
                if (int.TryParse(input, out int selectedAction))
                {
                    lastAction = (ProjectInfrastructureAction)selectedAction;
                    if (lastAction != ProjectInfrastructureAction.ExitPromptMode)
                    {
                        ExecuteProjectAction(lastAction);
                    }
                }
                else
                {
                    var argsV2 = StringFormats.StringToParams(input);
                    try
                    {
                        var inputCommand = new InputRequest(argsV2);
                        ActionManager.QueueInputRequest(VirtualProjectState, inputCommand);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"ERROR: {ex.Message}");
                    }
                }
                CommitVirtualProjectChanges();
                UpdateDomainSuggestions();
            } while (lastAction != ProjectInfrastructureAction.ExitPromptMode);
        }


        private void UpdateDomainSuggestions()
        {
            ReadLineSuggestionHandler.UpdateDomains(GetCurrentDomainsList());
        }


        private List<string> GetCurrentDomainsList()
        {
            return VirtualProjectState.GetAllDomains().Select(k => k.Name).ToList();
        }

        private List<string> GetCurrentSchemaList()
        {
            return VirtualProjectState.GetAllSchemas().Select(k => k.Name).ToList();
        }

        private void ExecuteProjectAction(ProjectInfrastructureAction action)
        {
            if (action == ProjectInfrastructureAction.CommitChanges)
            {
                CommitProjectChanges();
            }
            else if (action == ProjectInfrastructureAction.Help)
            {
                Console.WriteLine(GetHelpString(ActionManager.Actions));
            }
            else if (action == ProjectInfrastructureAction.OpenFile)
            {
                Console.WriteLine("Json file path:");
                LastFilePath = Console.ReadLine();
                OpenFile(LastFilePath);
            }
            else if (action == ProjectInfrastructureAction.SaveChanges)
            {
                if (string.IsNullOrEmpty(LastFilePath))
                {
                    Console.WriteLine("Json file path:");
                    LastFilePath = Console.ReadLine();
                }
                SaveChanges(LastFilePath);
            }
            else if (action == ProjectInfrastructureAction.ShowProjectState)
            {
                string commitedState = Stringfy(ProjectState);
                Console.WriteLine("################################");
                Console.WriteLine("### -> Commited project state ->");
                Console.WriteLine("################################");
                Console.WriteLine(commitedState);
            }
            else if (action == ProjectInfrastructureAction.ShowVirtualProjectState)
            {
                string virtualState = Stringfy(VirtualProjectState);
                Console.WriteLine("################################");
                Console.WriteLine("### -> Virtual project state ->");
                Console.WriteLine("################################");
                Console.WriteLine(virtualState);
            }
            else if (action == ProjectInfrastructureAction.NewProject)
            {
                NewProject();
            }
            else if (action == ProjectInfrastructureAction.DiscardAllQueuedChanges)
            {
                DiscardQueuedActions();
            }
            else if (action == ProjectInfrastructureAction.DiscardLastQueuedChange)
            {
                DiscardLastQueuedAction();
            }
        }

        public void NewProject()
        {
            ProjectState = new ProjectState();
            LastFilePath = null;
            RaiseProjectStateChange();
        }

        public void OpenFile(string path)
        {
            var absolutePath = _fileService.GetAbsoluteCurrentPath(path);
            if (Path.GetExtension(absolutePath) != ".json")
            {
                throw new Exception("Invalid extension file. Select .json file");
            }
            var json = _fileService.OpenFile(absolutePath);
            ProjectState = Objectify(json);
            RaiseProjectStateChange();
        }
      
        public void SaveChanges(string path)
        {
            var absolutePath = _fileService.GetAbsoluteCurrentPath(path);
            var json = Stringfy(ProjectState);
            _fileService.SaveFile(absolutePath, json);
        }

        private  ActionManager GetActionManager()
        {
            var actionManager = new ActionManager(_cryptoService);

            actionManager.RegisterAction(new InitializeRootDomain());
            actionManager.RegisterAction(new UpdateProjectName());
            actionManager.RegisterAction(new AddDomain());
            actionManager.RegisterAction(new DeleteDomain());
            actionManager.RegisterAction(new DeleteSchema());
            actionManager.RegisterAction(new AddSchema());
            actionManager.RegisterAction(new ModifySchema());
            actionManager.RegisterAction(new AddSchemaProperty());
            actionManager.RegisterAction(new AddAzurePipelinesSetting());
            actionManager.RegisterAction(new DeleteAzurePipelinesSetting());
            actionManager.RegisterAction(new AddGithubSetting());
            actionManager.RegisterAction(new DeleteGithubSetting());
            actionManager.RegisterAction(new InitializeArchitectureSetup());
            actionManager.RegisterAction(new AddSchemaIntersection());
            actionManager.RegisterAction(new AddUseCase());
            actionManager.RegisterAction(new DeleteUseCase());
            actionManager.RegisterAction(new UpdateProjectReposPath(_fileService));
            actionManager.RegisterAction(new AddSchemaToDomain());

            return actionManager;
        }


        public void CommitProjectChanges()
        {
            ExecuteChanges(ProjectState, false);
        }

        public void CommitVirtualProjectChanges()
        {
            ExecuteChanges(VirtualProjectState, true);
        }

        public void DiscardQueuedActions()
        {
            ProjectState.Actions.RemoveAll(k => k.State == ActionExecution.ActionExecutionState.Queued);
            RaiseProjectStateChange();
        }

        public void DiscardLastQueuedAction()
        {
            var lastQueuedAction = ProjectState.Actions.Last(k => k.State == ActionExecution.ActionExecutionState.Queued);
            if (lastQueuedAction != null)
            {
                ProjectState.Actions.Remove(lastQueuedAction);
            }
            RaiseProjectStateChange();
        }

        private void ExecuteChanges(ProjectState projectState, bool isVirtual)
        {
            foreach (var action in projectState.Actions.Where(k => k.State == ActionExecution.ActionExecutionState.Queued))
            {
                if (!TryExecuteAction(projectState, isVirtual, action))
                {
                    return;
                }
            }
        }

        private bool TryExecuteAction(ProjectState projectState, bool isVirtual, ActionExecution actionExecution)
        {
            try
            {
                actionExecution.State = ActionExecution.ActionExecutionState.Executing;
                var action = ActionManager.Actions.First(k => k.Name == actionExecution.ActionName);
                ActionManager.ExecuteAction(projectState, action, actionExecution.Parameters.ToActionParametersList(), isVirtual, null);
                actionExecution.State = ActionExecution.ActionExecutionState.Executed;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error executing action '{actionExecution.ActionName}'. Execution queue stopped in this item. Throwed exception: {ex.Message}");
                actionExecution.State = ActionExecution.ActionExecutionState.Queued;
                return false;
            }
            return true;
        }


        private void ExecuteNextChange(ProjectState projectState, bool isVirtual)
        {
            //var action = projectState.Actions.FirstOrDefault(k => k.State == ActionExecution.ActionExecutionState.Queued);
            //if (action != null)
            //{
            //    action.State = ActionExecution.ActionExecutionState.Executing;
            //    ActionManager.ExecuteAction(projectState, action.Action, action.ActionParameters, isVirtual, null);
            //    action.State = ActionExecution.ActionExecutionState.Executed;
            //}
        }

        public void AddQueueAction(string actionName, Dictionary<string, object> parameters)
        {
            ProjectState.Actions.Add(new ActionExecution(actionName, parameters));
            RaiseProjectStateChange();
        }


        public void UpdateQueuedAction(ActionExecution action, Dictionary<string, object> values)
        {
            var myAction = ProjectState.Actions.FirstOrDefault(k => k.Id == action.Id);
            var myActionDefinition = ActionManager.Actions.FirstOrDefault(k => k.Name == myAction.ActionName);
            if (myAction.State != ActionExecution.ActionExecutionState.Queued)
            {
                throw new Exception($"Only actions in state Queued can be modified");
            }
            foreach (var item in values)
            {
                var parameter = myActionDefinition
                    .ActionParametersDefinition
                    .FirstOrDefault(k => k.Name == item.Key);
                if (parameter.Type == ActionParameterDefinition.TypeValue.Password
                    && !string.IsNullOrEmpty((string)item.Value)
                    || parameter.Type != ActionParameterDefinition.TypeValue.Password)
                {
                    myAction.Parameters[parameter.Name] = item.Value;
                }
            }
            RaiseProjectStateChange();
        }


        public void RemoveQueuedAction(ActionExecution action)
        {
            var myAction = ProjectState.Actions.FirstOrDefault(k => k.Id == action.Id);
            if (myAction == null)
            {
                throw new Exception($"Cannot find action with id {myAction.Id}");
            }
            ProjectState.Actions.Remove(myAction);
            RaiseProjectStateChange();
        }

        private void ActionManager_OnQueuedAction(object sender, ActionEventArgs args)
        {
            AddQueueAction(args.Action.Name, args.ActionParameters.ToDictionary(args.Action.ActionParametersDefinition));
        }

        private void RaiseProjectStateChange()
        {
            VirtualProjectState = Objectify(Stringfy(ProjectState));
            CommitVirtualProjectChanges();
            OnProjectChanged?.Invoke(this, new ProjectEventArgs(ProjectState));
        }

        private static void ActionManager_OnLog(object sender, LogEventArgs e)
        {
            Console.WriteLine(e.Log);
        }


        private static string GetHelpString(List<ActionBase> actions)
        {
            var data = new StringBuilder();
            Version assemblyVersion = Assembly.GetEntryAssembly().GetName().Version;
            data.AppendLine($"DD.DomainGenerator version {assemblyVersion.ToString()}");
            data.AppendLine(
                actions
                    .OrderBy(k => k.GetInvocationCommandName())
                    .ToDisplayList((item) =>
                    {
                        return item.GetInvocationCommandName();
                    },
                        "Available actions:",
                        "#"));
            return data.ToString();
        }


        private string Stringfy(ProjectState projectState)
        {
            var settings = new JsonSerializerSettings()
            {
                PreserveReferencesHandling = PreserveReferencesHandling.Objects,
            };
            return JsonConvert.SerializeObject(projectState, Formatting.Indented, settings);
        }

        private ProjectState Objectify(string json)
        {
            var settings = new JsonSerializerSettings()
            {
                PreserveReferencesHandling = PreserveReferencesHandling.Objects,
            };
            return JsonConvert.DeserializeObject<ProjectState>(json, settings);
        }
    }
}
