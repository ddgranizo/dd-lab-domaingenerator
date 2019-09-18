using DD.DomainGenerator.Actions.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DD.DomainGenerator.Utilities
{
    class ReadLineAutocompleteHandler : IAutoCompleteHandler
    {

        public List<ActionBase> Actions { get; set; }
        public ReadLineAutocompleteHandler(List<ActionBase> actions)
        {
            Actions = actions ?? throw new ArgumentNullException(nameof(actions));
        }

        public char[] Separators { get; set; } = new char[] { ' ' };
        public string[] GetSuggestions(string text, int index)
        {
            if (string.IsNullOrEmpty(text) || string.IsNullOrEmpty(text.Trim()))
            {
                return Actions
                    .Select(k => k.GetInvocationCommandName())
                    .ToArray();
            }
            if (index == 0)
            {
                return Actions
                    .Where(k => k.GetInvocationCommandName().IndexOf(text) > -1)
                    .Select(k => k.GetInvocationCommandName())
                    .ToArray();
            }

            var parametersInputed = text
                .Split(new char[] { ' ' })
                .Where(k => k.Length > 2 && k.Substring(0, 2) == "--")
                .Select(k => k.Trim());

            var actionName = text
                .Split(new char[] { ' ' })
                .FirstOrDefault();
            if (actionName!=null)
            {
                var action = Actions.FirstOrDefault(k => k.GetInvocationCommandName() == actionName.Trim());
                if (action != null)
                {
                    return action.ActionParametersDefinition
                        .Where(k => !parametersInputed.Any(l => l == k.GetInvokeName()))
                        .Select(k => k.GetInvokeName())
                        .ToArray();
                }
            }
            
            return new string[] { };
        }
    }
}
