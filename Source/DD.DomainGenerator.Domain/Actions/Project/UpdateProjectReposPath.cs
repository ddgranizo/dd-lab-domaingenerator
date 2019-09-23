using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using DD.DomainGenerator.Actions.Base;
using DD.DomainGenerator.Extensions;
using DD.DomainGenerator.Models;
using DD.DomainGenerator.Services;
using DD.DomainGenerator.Utilities;

namespace DD.DomainGenerator.Actions.Project
{
    public class UpdateProjectReposPath : ActionBase
    {
        public const string ActionName = "UpdateProjectReposPath";

        public ActionParameterDefinition PathParameter { get; set; }
        public IFileService FileService { get; }

        public UpdateProjectReposPath(IFileService fileService) : base(ActionName)
        {
            PathParameter = new ActionParameterDefinition(
                "path", ActionParameterDefinition.TypeValue.String, "Path for the repos", "p");
            ActionParametersDefinition.Add(PathParameter);
            FileService = fileService ?? throw new ArgumentNullException(nameof(fileService));
        }

        public override bool CanExecute(ProjectState project, List<ActionParameter> parameters)
        {
            return IsParamOk(parameters, PathParameter);
        }

        public override void ExecuteStateChange(ProjectState project, List<ActionParameter> parameters)
        {
            var path = GetStringParameterValue(parameters, PathParameter).ToWordPascalCase();
            var absolutePath = FileService.GetAbsoluteCurrentPath(path);
            project.ReposPath = absolutePath;
        }
        
    }
}
