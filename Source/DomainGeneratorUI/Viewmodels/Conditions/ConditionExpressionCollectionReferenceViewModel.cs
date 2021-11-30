using DD.Lab.Wpf.ViewModels.Base;
using DomainGeneratorUI.Viewmodels.UseCases;
using System;
using System.Collections.Generic;
using System.Text;

namespace DomainGeneratorUI.Viewmodels.Conditions
{
    public class ConditionExpressionCollectionReferenceViewModel: BaseViewModel
    {
        public ConditionExpressionViewModel Expression { get { return GetValue<ConditionExpressionViewModel>(); } set { SetValue(value); } }
        public UseCaseSentenceCollectionViewModel Collection { get { return GetValue<UseCaseSentenceCollectionViewModel>(); } set { SetValue(value); } }
    }
}
