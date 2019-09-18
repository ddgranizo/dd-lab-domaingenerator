using DD.DomainGenerator.Events;
using DD.DomainGenerator.Extensions;
using DD.DomainGenerator.Models;
using DD.DomainGenerator.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DD.DomainGenerator.Actions.Base
{

    public delegate void OnLogHandler(object sender, LogEventArgs e);
    public  class ActionBase
    {
        public event OnLogHandler OnLog;

        public bool ActionModifyState { get; set; } = true;

        public string Name { get; set; }
        public string Description { get; set; }
        public List<ActionParameterDefinition> ActionParametersDefinition { get; set; }
        public IConsoleService ConsoleService { get; set; }

        public ActionBase(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentException("message", nameof(name));
            }
            Name = name;
            ActionParametersDefinition = new List<ActionParameterDefinition>();
            ImplementHelpCommand();
        }

        public void RegisterActionParameter(ActionParameterDefinition parameter)
        {
            this.ActionParametersDefinition.Add(parameter);
        }

        public void ExecuteHelp()
        {
            var data = new StringBuilder();
            data.AppendLine($"# Action Name: {Name}");
            data.AppendLine($"# Invocation Name: {GetInvocationCommandName()}");
            data.AppendLine($"# Description: {Description}");
            data.AppendLine(
                ActionParametersDefinition.ToDisplayList(
                    item => {
                        StringBuilder line = new StringBuilder();
                        line.Append($"--{item.Name}");
                        if (item.ShortCut != null)
                        {
                            line.Append($" [-{item.ShortCut}]");
                        }
                        line.Append($", Type value: {item.Type.ToString()}, Description: {item.Description}");
                        return line.ToString();
                    }, "Parameters:", ""));
            Log(data.ToString());
        }

        public void Log(string log)
        {
            OnLog?.Invoke(this, new LogEventArgs(log));
        }


        public string GetInvocationCommandName()
        {
            return Name.ToLowerInvariant();
        }

        public void ImplementHelpCommand()
        {
            var helpCommand = new ActionParameterDefinition("help", ActionParameterDefinition.TypeValue.Boolean, "Details about the command and it's available parameters", "h");
            ActionParametersDefinition.Add(helpCommand);
        }

        public bool IsHelpCommand(List<ActionParameter> parameters)
        {
            return parameters.Count == 1 && parameters[0].ParameterName == "help";
        }


        public bool IsParamOk(List<ActionParameter> parameters, ActionParameterDefinition parameter)
        {
            return IsParamOk(parameters, parameter.Name);
        }

        public bool IsParamOk(List<ActionParameter> parameters, string name)
        {
            var paremeterDefinition = ActionParametersDefinition
                .FirstOrDefault(k => k.Name == name);
            if (paremeterDefinition == null)
            {
                throw new KeyNotFoundException($"Parameter with name '{name}' doesnt exist in the definition of '{Name}'");
            }
            var parameter = parameters.FirstOrDefault(k => k.ParameterName == name);
            if (parameter != null)
            {
                if (paremeterDefinition.Type == ActionParameterDefinition.TypeValue.Boolean)
                {
                    return true;
                }
                else if (paremeterDefinition.Type == ActionParameterDefinition.TypeValue.Decimal)
                {
                    return true;
                }
                else if (paremeterDefinition.Type == ActionParameterDefinition.TypeValue.Integer)
                {
                    return true;
                }
                else if (paremeterDefinition.Type == ActionParameterDefinition.TypeValue.String)
                {
                    return !string.IsNullOrEmpty(parameter.ValueString);
                }
                else if (paremeterDefinition.Type == ActionParameterDefinition.TypeValue.Guid)
                {
                    return parameter.ValueGuid != Guid.Empty;
                }
            }
            return false;
        }


        public string GetStringParameterValue(List<ActionParameter> parameters, ActionParameterDefinition parameter, string defaultValue = "")
        {
            return GetStringParameterValue(parameters, parameter.Name, defaultValue);
        }

        public string GetStringParameterValue(List<ActionParameter> parameters, string name, string defaultValue = "")
        {
            return IsParamOk(parameters, name) ? parameters.FirstOrDefault(k => k.ParameterName == name).ValueString : defaultValue;
        }

        public bool GetBoolParameterValue(List<ActionParameter> parameters, ActionParameterDefinition parameter, bool defaultValue = false)
        {
            return GetBoolParameterValue(parameters, parameter.Name, defaultValue);
        }
        public bool GetBoolParameterValue(List<ActionParameter> parameters, string name, bool defaultValue = false)
        {
            return IsParamOk(parameters, name) ? parameters.FirstOrDefault(k => k.ParameterName == name).ValueBool : defaultValue;
        }

        public int GetIntParameterValue(List<ActionParameter> parameters, ActionParameterDefinition parameter, int defaultValue = 0)
        {
            return GetIntParameterValue(parameters, parameter.Name, defaultValue);
        }
        public int GetIntParameterValue(List<ActionParameter> parameters, string name, int defaultValue = 0)
        {
            return IsParamOk(parameters, name) ? parameters.FirstOrDefault(k => k.ParameterName == name).ValueInt : defaultValue;
        }

        public decimal GetDecimalParameterValue(List<ActionParameter> parameters, ActionParameterDefinition parameter, decimal defaultValue = 0m)
        {
            return GetDecimalParameterValue(parameters, parameter.Name, defaultValue);
        }
        public decimal GetDecimalParameterValue(List<ActionParameter> parameters, string name, decimal defaultValue = 0m)
        {
            return IsParamOk(parameters, name) ? parameters.FirstOrDefault(k => k.ParameterName == name).ValueDecimal : defaultValue;
        }

        public Guid GetGuidParameterValue(List<ActionParameter> parameters, ActionParameterDefinition parameter)
        {
            return GetGuidParameterValue(parameters, parameter.Name);
        }
        public Guid GetGuidParameterValue(List<ActionParameter> parameters, string name)
        {
            return IsParamOk(parameters, name) ? parameters.FirstOrDefault(k => k.ParameterName == name).ValueGuid : Guid.Empty;
        }


        public virtual bool CanExecute(ProjectState project, List<ActionParameter> parameters)
        {
            return true;
        }

        public virtual void ExecuteStateChange(ProjectState project, List<ActionParameter> parameters)
        {
            //Do nothing
        }

        public virtual void ExecuteThirdParties(ProjectState project, List<ActionParameter> parameters)
        {
            //Do nothing
        }
    }
}
