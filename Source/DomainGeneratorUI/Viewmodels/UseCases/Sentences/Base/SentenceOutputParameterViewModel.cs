using DD.Lab.Wpf.ViewModels.Base;
using DomainGeneratorUI.Viewmodels.Methods;
using System;
using System.Collections.Generic;
using System.Text;

namespace DomainGeneratorUI.Viewmodels.UseCases.Sentences.Base
{
    public class SentenceOutputParameterViewModel : BaseViewModel
    {
        public MethodParameterViewModel SourceParameter { get { return GetValue<MethodParameterViewModel>(); } set { SetValue(value); } }

    }
}
