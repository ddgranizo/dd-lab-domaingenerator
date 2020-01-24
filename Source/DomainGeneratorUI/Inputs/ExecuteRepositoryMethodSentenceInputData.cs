using DD.Lab.Wpf.Drm;
using DomainGeneratorUI.Viewmodels.UseCases.Sentences.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace DomainGeneratorUI.Inputs
{
    public class ExecuteRepositoryMethodSentenceInputData 
    {
        public UseCaseSentenceViewModel Sentence { get; set; }
        public GenericManager GenericManager { get; set; }
    }
}
