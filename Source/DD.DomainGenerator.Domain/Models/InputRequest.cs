using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DD.DomainGenerator.Models
{
    public class InputRequest
    {
        public string ActionName { get; set; }

        public List<InputParameter> InputParameters { get; set; }
        public InputRequest(params string[] args)
        {
            InputParameters = new List<InputParameter>();
            if (args.Length == 0)
            {
                throw new Exception($"NotArgumentsException"); 
            }
            var actionRawName = args[0].ToLowerInvariant();
            ActionName = actionRawName;

            var parametersArr = args.Skip(1).ToArray();
            if (parametersArr.ToList().Count > 0)
            {
                InputParameters = GetInputParameters(parametersArr);
            }
        }

        private List<InputParameter> GetInputParameters(string[] parameterArr)
        {
            var parameters = new List<InputParameter>();
            if (parameterArr.Length == 1 && !IsParamNameValid(parameterArr[0]))
            {
                parameters.Add(new InputParameter(parameterArr[0]));
            }
            else
            {
                bool nextIsValue = false;
                for (int i = 0; i < parameterArr.Length; i++)
                {
                    var parameter = parameterArr[i];
                    if (!nextIsValue)
                    {
                        bool isValidParam = IsParamNameValid(parameter);
                        bool isValidShortCut = isValidParam
                            ? false :
                            IsParamShortCutValid(parameter);
                        if (!isValidParam && !isValidShortCut)
                        {
                            throw new Exception($"InvalidParamNameException: {parameter}");
                        }
                        string paramValue = null;
                        string parameterTrimmed = isValidShortCut
                                ? parameter.Substring("-".Length)
                                : parameter.Substring("--".Length);
                        if (i < parameterArr.Length - 1
                                && !IsParamNameValid(parameterArr[i + 1])
                                && !IsParamShortCutValid(parameterArr[i + 1]))
                        {
                            paramValue = parameterArr[i + 1];
                            nextIsValue = true;
                        }
                        parameters.Add(new InputParameter(parameterTrimmed, paramValue, isValidShortCut));
                    }
                    else
                    {
                        nextIsValue = false;
                    }
                }
            }
            return parameters;
        }




        private bool IsParamShortCutValid(string parameter)
        {
            return !string.IsNullOrEmpty(parameter) && parameter.Length > 1 && parameter.Substring(0, 1) == "-";
        }

        private bool IsParamNameValid(string parameter)
        {
            return !string.IsNullOrEmpty(parameter) && parameter.Length > 2 && parameter.Substring(0, 2) == "--";
        }

       
    }
}
