using DD.Lab.Wpf.ViewModels.Base;
using DomainGeneratorUI.Viewmodels.Methods;
using System;
using System.Collections.Generic;
using System.Text;

namespace DomainGeneratorUI.Viewmodels.UseCases.Sentences.Base
{
    public class SentenceOutputReferencedParameterViewModel : BaseViewModel
    {
        public MethodParameterViewModel RegardingParameter { get { return GetValue<MethodParameterViewModel>(); } set { SetValue(value); } }

    }
}
