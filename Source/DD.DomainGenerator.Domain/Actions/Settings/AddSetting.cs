using DD.DomainGenerator.Actions.Base;
using DD.DomainGenerator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DD.DomainGenerator.Actions.Settings
{

    public class AddSetting : ActionBase
    {
        public const string ActionName = "AddSetting";

        public ActionParameterDefinition NameParameter { get; set; }
        public ActionParameterDefinition ValueParameter { get; set; }

        public AddSetting() : base(ActionName)
        {
            NameParameter = new ActionParameterDefinition(
               "name", ActionParameterDefinition.TypeValue.String, "Setting name", "n", string.Empty)
            {
                InputSuggestions = Definitions.Settings.ToList()
            };
            ValueParameter = new ActionParameterDefinition(
                "value", ActionParameterDefinition.TypeValue.String, "Value for the setting", "v", string.Empty);
          
            ActionParametersDefinition.Add(NameParameter);
            ActionParametersDefinition.Add(ValueParameter);
        }

        public override bool CanExecute(ProjectState project, List<ActionParameter> parameters)
        {
            return IsParamOk(parameters, NameParameter)
                && IsParamOk(parameters, ValueParameter);
        }

        public override void ExecuteStateChange(ProjectState project, List<ActionParameter> parameters)
        {
            var name = GetStringParameterValue(parameters, NameParameter);
            var value = GetStringParameterValue(parameters, ValueParameter);
            var exists = project.Settings.FirstOrDefault(k => k.Name == name);
            if (exists != null)
            {
                throw new Exception($"Setting with name {name} already exists");
            }
            project.Settings.Add(new Setting(name, value));
        }
    }
}
