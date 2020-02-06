using DD.Lab.Wpf.ViewModels.Base;
using DomainGeneratorUI.Viewmodels.Methods;
using System;
using System.Collections.Generic;
using System.Text;

namespace DomainGeneratorUI.Models.Methods
{
    public class MethodParameterReferenceValueViewModel: BaseViewModel
    {
        public MethodParameterReferenceViewModel RegardingReferenceMethodParameter { get { return GetValue<MethodParameterReferenceViewModel>(); } set { SetValue(value); } }
        public MethodParameterViewModel RegardingMethodParameter { get { return GetValue<MethodParameterViewModel>(); } set { SetValue(value); } }
        public MethodParameterReferenceValue.ValueType Type { get { return GetValue<MethodParameterReferenceValue.ValueType>(); } set { SetValue(value); } }
        public object ConstantValue { get { return GetValue<object>(); } set { SetValue(value); } }

    }
}
