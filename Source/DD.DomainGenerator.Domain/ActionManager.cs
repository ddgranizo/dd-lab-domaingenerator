using DD.DomainGenerator.Actions;
using DD.DomainGenerator.Actions.Base;
using DD.DomainGenerator.Events;
using DD.DomainGenerator.Models;
using DD.DomainGenerator.Services;
using DD.DomainGenerator.Services.Implementations;
using DD.DomainGenerator.Utilities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace DD.DomainGenerator
{

    public delegate void ErrorExecutionActionHandler(object sender, ErrorExecutionActionEventArgs args);
    public delegate void ActionHandler(object sender, ActionEventArgs args);
    public class ActionManager
    {
        public List<ActionBase> Actions { get; set; }
        public ICryptoService CryptoService { get; }

        public event OnLogHandler OnLog;
        public event ActionHandler OnQueueAction;
        public event ErrorExecutionActionHandler OnErrorExecution;
        public ActionManager(ICryptoService cryptoService)
        {
            Actions = new List<ActionBase>();
            CryptoService = cryptoService ?? throw new ArgumentNullException(nameof(cryptoService));
        }
        public ActionManager(List<ActionBase> actions)
        {
            Actions = actions
                ?? throw new ArgumentNullException(nameof(actions));
        }

        public void RegisterAction(ActionBase action)
        {
            Actions.Add(action);
        }



        public void QueueInputRequest(ProjectState projectDefinition, InputRequest inputRequest, List<string> consoleInputs = null)
        {
            List<ActionBase> actions = SearchActions(inputRequest);

            if (actions.Count == 0)
            {
                throw new Exception($"ActionNotFoundException: {inputRequest.ActionName}");
            }

            var action = actions[0];

            var actionParameters = new List<ActionParameter>();
            if (action.ActionParametersDefinition.Where(k => k.Name != "help").Count() == 1
                && inputRequest.InputParameters.Count == 1
                && inputRequest.InputParameters[0].IsOnlyOne)
            {
                var targetCommandParameter = action.ActionParametersDefinition.FirstOrDefault(k => k.Name != "help");
                var parameter = GetParsedActionParameter(
                    action, targetCommandParameter, inputRequest.InputParameters[0]);
                actionParameters.Add(parameter);
            }
            else
            {
                foreach (var parameterDefinition in action.ActionParametersDefinition)
                {
                    var itemInput = GetImputParameterFromRequest(inputRequest, parameterDefinition);
                    if (itemInput != null)
                    {
                        var parameter = GetParsedActionParameter(action, parameterDefinition, itemInput);
                        actionParameters.Add(parameter);
                    }
                }
            }

            if (action.CanExecute(projectDefinition, actionParameters) || action.IsHelpCommand(actionParameters))
            {
                QueueAction(projectDefinition, action, actionParameters, consoleInputs);
            }
            else
            {
                throw new Exception("InvalidParamsOrStateException. This action cannot be invoked with this inputs or with this project state. Execute 'actionname --help' for further information");
            }
        }

        private void QueueAction(ProjectState projectState, ActionBase action, List<ActionParameter> actionParameters, List<string> consoleInputs = null)
        {
            try
            {
                action.OnLog += Action_OnLog;
                if (action.CanExecute(projectState, actionParameters))
                {
                    OnQueueAction?.Invoke(this, new ActionEventArgs(action, actionParameters));
                }
                else
                {
                    action.ExecuteHelp();
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                action.OnLog -= Action_OnLog;
            }
        }

        public Dictionary<string, object> ExecuteAction(ProjectState projectState, ActionBase action, List<ActionParameter> actionParameters, bool executeVirtual = false, List<string> consoleInputs = null)
        {
            var outputParameters = new Dictionary<string, object>();
            try
            {
                action.OnLog += Action_OnLog;
                if (action.CanExecute(projectState, actionParameters))
                {
                    var timer = new Stopwatch(); timer.Start();
                    action.ConsoleService = new ConsoleService(consoleInputs);
                    outputParameters = action.PrepareExecution(projectState, actionParameters);
                    var time = StringFormats.MillisecondsToHumanTime(timer.ElapsedMilliseconds);
                }
                else
                {
                    action.ExecuteHelp();
                }
            }
            catch (Exception ex)
            {
                OnErrorExecution?.Invoke(this, new ErrorExecutionActionEventArgs(action, ex));
                throw;
            }
            finally
            {
                action.OnLog -= Action_OnLog;
            }
            return outputParameters;
        }


        private Dictionary<string, object> ActionParametersToDictionary(ActionBase action, List<ActionParameter> parameters)
        {
            var output = new Dictionary<string, object>();
            foreach (var item in parameters)
            {
                var paramDefinition = action.ActionParametersDefinition.FirstOrDefault(k => k.Name.ToLowerInvariant() == item.ParameterName.ToLowerInvariant());

                object value = null;
                if (paramDefinition.Type == ActionParameterDefinition.TypeValue.Boolean)
                {
                    value = item.ValueBool;
                }
                else if (paramDefinition.Type == ActionParameterDefinition.TypeValue.Decimal)
                {
                    value = item.ValueDecimal;
                }
                else if (paramDefinition.Type == ActionParameterDefinition.TypeValue.Integer)
                {
                    value = item.ValueInt;
                }
                else if (paramDefinition.Type == ActionParameterDefinition.TypeValue.String)
                {
                    value = item.ValueString;
                }
                output.Add(item.ParameterName, value);
            }
            return output;
        }

        private void Action_OnLog(object sender, Events.LogEventArgs e)
        {
            OnLog?.Invoke(sender, e);
        }

        private List<ActionBase> SearchActions(InputRequest inputRequest)
        {
            return Actions.Where(k => k.GetInvocationCommandName() == inputRequest.ActionName)
                                    .ToList();
        }

        private static InputParameter GetImputParameterFromRequest(InputRequest inputRequest, ActionParameterDefinition parameter)
        {
            var inputByName = inputRequest
                    .InputParameters
                    .FirstOrDefault(k => k.ParameterName == parameter.Name && !k.IsShortCut);
            if (inputByName != null)
            {
                return inputByName;
            }
            var inputByShortCut = inputRequest
                    .InputParameters
                    .FirstOrDefault(k => k.ParameterName == parameter.ShortCut && k.IsShortCut);
            return inputByShortCut;
        }



        private ActionParameter GetParsedActionParameter(ActionBase action, ActionParameterDefinition item, InputParameter itemInput)
        {
            var parameterName = item.Name;
            ActionParameter actionParameter = null;
            var rawString = itemInput.RawStringValue;
            if (item.Type == ActionParameterDefinition.TypeValue.String)
            {
                actionParameter = new ActionParameter(parameterName, rawString);
            }
            else if (item.Type == ActionParameterDefinition.TypeValue.Password)
            {
                actionParameter = new ActionParameter(parameterName, CryptoService.Encrypt(rawString));
            }
            else if (item.Type == ActionParameterDefinition.TypeValue.Boolean)
            {
                if (!itemInput.HasValue)
                {
                    actionParameter = new ActionParameter(parameterName, true);
                }
                else
                {
                    bool boolValue = StrToBool(rawString);
                    actionParameter = new ActionParameter(parameterName, boolValue);
                }
            }
            else if (item.Type == ActionParameterDefinition.TypeValue.Decimal)
            {
                if (!decimal.TryParse(rawString, out decimal d))
                {
                    throw new InvalidCastException(GetInvalidCastExceptionMessage(action, item, rawString, "decimal"));
                }
                actionParameter = new ActionParameter(parameterName, d);
            }
            else if (item.Type == ActionParameterDefinition.TypeValue.Integer)
            {
                if (!int.TryParse(rawString, out int d))
                {
                    throw new InvalidCastException(GetInvalidCastExceptionMessage(action, item, rawString, "integer"));
                }
                actionParameter = new ActionParameter(parameterName, d);
            }
            return actionParameter;
        }

        private static string GetInvalidCastExceptionMessage(ActionBase action, ActionParameterDefinition item, string rawString, string typeString)
        {
            return $"'{rawString}' cannot be converted to {typeString} for action {action.Name} and parameter ${item.Name}";
        }

        private static bool StrToBool(string value)
        {
            return Definitions
                    .AvailableTrueStrings
                    .ToList().IndexOf(value.ToLowerInvariant()) > -1;
        }
    }
}
