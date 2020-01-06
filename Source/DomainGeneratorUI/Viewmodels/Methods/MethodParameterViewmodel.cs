using DD.Lab.Wpf.Models.Inputs;
using DD.Lab.Wpf.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Text;
using static DomainGeneratorUI.Models.Methods.MethodParameter;

namespace DomainGeneratorUI.Viewmodels.Methods
{
    public class MethodParameterViewmodel : BaseViewModel
    {
        public string Name { get { return GetValue<string>(); } set { SetValue(value); } }
        public ParameterDirection Direction { get { return GetValue<ParameterDirection>(); } set { SetValue(value); } }
        public ParameterInputType Type { get { return GetValue<ParameterInputType>(); } set { SetValue(value); } }
        public ParameterInputType EnumerableType { get { return GetValue<ParameterInputType>(); } set { SetValue(value); } }
        public ParameterInputType DictionaryValueType { get { return GetValue<ParameterInputType>(); } set { SetValue(value); } }
        public ParameterInputType DictionaryKeyType { get { return GetValue<ParameterInputType>(); } set { SetValue(value); } }

        
        public MethodParameterViewmodel()
        {
        }

        public MethodParameterViewmodel(Dictionary<string,object> data)
        {
            UpdateDataFromDictionary(data);
        }

        public void UpdateDataFromDictionary(Dictionary<string, object> data)
        {
            Name = GetDictionaryValue<string>(data, nameof(Name));
            Direction = (ParameterDirection)GetDictionaryValue(data, nameof(Direction), new OptionSetValue(0)).Value;
            Type = (ParameterInputType)GetDictionaryValue(data, nameof(Type), new OptionSetValue(0)).Value;
            EnumerableType = (ParameterInputType)GetDictionaryValue(data, nameof(EnumerableType), new OptionSetValue(0)).Value;
            DictionaryKeyType= (ParameterInputType)GetDictionaryValue(data, nameof(DictionaryKeyType), new OptionSetValue(0)).Value;
            DictionaryValueType = (ParameterInputType)GetDictionaryValue(data, nameof(DictionaryValueType), new OptionSetValue(0)).Value;
        }
    }
}
