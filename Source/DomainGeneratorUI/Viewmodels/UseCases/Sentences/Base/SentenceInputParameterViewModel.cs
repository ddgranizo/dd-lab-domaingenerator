using DD.Lab.Wpf.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Text;
using static DomainGeneratorUI.Models.UseCases.Sentences.Base.SentenceInputParameter;

namespace DomainGeneratorUI.Viewmodels.UseCases.Sentences.Base
{
    public class SentenceInputParameterViewModel : BaseViewModel
    {
        public SentenceSourceTpye Type { get { return GetValue<SentenceSourceTpye>(); } set { SetValue(value); } }
        public UseCaseSentenceViewModel RegardingSentence { get { return GetValue<UseCaseSentenceViewModel>(); } set { SetValue(value); } }
        public UseCaseSentenceViewModel RegardingSentenceOutputParameter { get { return GetValue<UseCaseSentenceViewModel>(); } set { SetValue(value); } }
        public UseCaseSentenceViewModel RegardingUseCaseParameter { get { return GetValue<UseCaseSentenceViewModel>(); } set { SetValue(value); } }

    }
}
