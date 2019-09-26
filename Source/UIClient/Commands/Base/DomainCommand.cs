using DD.DomainGenerator;
using DD.DomainGenerator.Actions.Base;
using DD.DomainGenerator.Extensions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace UIClient.Commands.Base
{
    //public class DomainCommand : ICommand
    //{
    //    public ProjectState ProjectState { get; }
    //    public ActionBase Action { get; set; }

    //    public DomainCommand(ProjectState projectState, ActionBase action)
    //    {
    //        ProjectState = projectState ?? throw new ArgumentNullException(nameof(projectState));
    //        Action = action ?? throw new ArgumentNullException(nameof(action));
    //    }

    //    public event EventHandler CanExecuteChanged;

    //    public bool CanExecute(object parameter)
    //    {
    //        Dictionary<string, object> inputs = GetInputParameters(parameter);
    //        return Action.CanExecute(ProjectState, inputs.ToActionParametersList());
    //    }


    //    public void Execute(object parameter)
    //    {
    //        Dictionary<string, object> inputs = GetInputParameters(parameter);
    //        Action.ExecuteStateChange(ProjectState, inputs.ToActionParametersList());
    //    }

    //    private static Dictionary<string, object> GetInputParameters(object parameter)
    //    {
    //        Dictionary<string, object> inputs = new Dictionary<string, object>();
    //        if (parameter != null && parameter is Dictionary<string, object>)
    //        {
    //            inputs = (Dictionary<string, object>)parameter;
    //        }

    //        return inputs;
    //    }
    //}
}
