using System;
using System.Collections.Generic;
using System.Text;

namespace DD.DomainGenerator.Models
{
    public class ActionParameter
    {
        public string ParameterName { get; set; }
        public bool ValueBool { get; set; }
        public int ValueInt { get; set; }
        public decimal ValueDecimal { get; set; }
        public string ValueString { get; set; }
        public Guid ValueGuid { get; set; }
        public object Value { get; set; }

        public ActionParameter()
        {

        }

        public ActionParameter(string parameterName)
        {
            ValueBool = true;
            ParameterName = parameterName ?? throw new ArgumentNullException(nameof(parameterName));
        }

        public ActionParameter(string parameterName, object value)
        {
            ParameterName = parameterName ?? throw new ArgumentNullException(nameof(parameterName));
            Value = value ?? throw new ArgumentNullException(nameof(value));
        }

        public ActionParameter(string parameterName, bool value)
        {
            ParameterName = parameterName ?? throw new ArgumentNullException(nameof(parameterName));
            ValueBool = value;
        }
        public ActionParameter(string parameterName, int value)
        {
            ParameterName = parameterName ?? throw new ArgumentNullException(nameof(parameterName));
            ValueInt = value;
        }

        public ActionParameter(string parameterName, string value)
        {
            ParameterName = parameterName ?? throw new ArgumentNullException(nameof(parameterName));
            ValueString = value ?? throw new ArgumentNullException(nameof(value));
        }

        public ActionParameter(string parameterName, Guid value)
        {
            ParameterName = parameterName ?? throw new ArgumentNullException(nameof(parameterName));
            ValueGuid = value;
        }

        public ActionParameter(string parameterName, decimal value)
        {
            ParameterName = parameterName ?? throw new ArgumentNullException(nameof(parameterName));
            ValueDecimal = value;
        }
    }
}
