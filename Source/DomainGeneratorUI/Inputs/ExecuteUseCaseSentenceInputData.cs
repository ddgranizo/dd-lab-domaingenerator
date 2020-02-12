using DD.Lab.Wpf.Drm;
using DomainGeneratorUI.Models.Methods;
using DomainGeneratorUI.Viewmodels.Methods;
using DomainGeneratorUI.Viewmodels.UseCases.Sentences.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace DomainGeneratorUI.Inputs
{
    public class ExecuteUseCaseSentenceInputData
    {
        public UseCaseSentenceViewModel Sentence { get; set; }
        public GenericManager GenericManager { get; set; }
        public List<MethodParameterReferenceViewModel> ParentInputParameters { get; set; }
        public List<MethodParameterReferenceViewModel> ParentOutputParameters { get; set; }
    }
}
