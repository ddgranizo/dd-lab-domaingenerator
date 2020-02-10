using AutoMapper;
using DD.Lab.Wpf.Drm;
using DomainGeneratorUI.Models.UseCases;
using DomainGeneratorUI.Viewmodels.Methods;
using DomainGeneratorUI.Viewmodels.UseCases;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace DomainGeneratorUI.Inputs
{
    public class UseCaseSentenceCollectionManagerInputData 
    {
        public List<MethodParameterReferenceViewModel> ParentInputParameters { get; set; }
        public List<MethodParameterReferenceViewModel> ParentOutputParameters { get; set; }
        public UseCaseSentenceCollectionViewModel SentenceCollection { get; set; }
        public GenericManager GenericManager { get; set; }
        public UseCaseContext UseCaseContext { get; set; }
        public IMapper Mapper { get; set; }
    }
}
