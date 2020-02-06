using DD.Lab.Wpf.Drm;
using DD.Lab.Wpf.ViewModels.Base;
using DomainGeneratorUI.Controls;
using DomainGeneratorUI.Inputs;
using DomainGeneratorUI.Models.Methods;
using DomainGeneratorUI.Viewmodels.Methods;
using DomainGeneratorUI.Viewmodels.UseCases.Sentences.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Xml.Serialization;

namespace DomainGeneratorUI.Viewmodels
{
    public class UseCaseSentenceContainerViewModel : BaseViewModel
    {

        public UseCaseSentenceViewModel Sentence { get { return GetValue<UseCaseSentenceViewModel>(); } set { SetValue(value, UpdatedSentence); } }
        public GenericManager GenericManager { get { return GetValue<GenericManager>(); } set { SetValue(value); } }
        public List<MethodParameterReferenceViewModel> ParentInputParameters { get { return GetValue<List<MethodParameterReferenceViewModel>>(); } set { SetValue(value, UpdatedParentInputParameters); } }
        public List<MethodParameterReferenceViewModel> ParentOutputParameters { get { return GetValue<List<MethodParameterReferenceViewModel>>(); } set { SetValue(value, UpdatedParentOutputParameters); } }

        private UseCaseSentenceContainerView _view;

        public UseCaseSentenceContainerViewModel()
        {
            AddSetterPropertiesTrigger(new DD.Lab.Wpf.Models.PropertiesTrigger(() =>
            {
                List<MethodParameterReferenceViewModel> childInputParameters = GetNewListWithInputParameters();
                List<MethodParameterReferenceViewModel> childOutputParameters = GetNewListWithOutputParameters();

                if (Sentence.Type == Models.UseCases.Sentences.Base.UseCaseSentence.SentenceType.ExecuteRepositoryMethod)
                {
                    _view.AddExecuteRepositoryMethodSentence(GenericManager, Sentence, childInputParameters, childOutputParameters);
                }
                else if (Sentence.Type == Models.UseCases.Sentences.Base.UseCaseSentence.SentenceType.ExecuteService)
                {

                }
                else
                {
                    //IfElse, Try, Foreach
                    AddCurrentChildInputParameters(childInputParameters);
                    AddCurrentChildOutputParameters(childOutputParameters);
                }
            }, nameof(Sentence), nameof(GenericManager), nameof(ParentInputParameters), nameof(ParentOutputParameters)));
        }

        private void UpdatedParentOutputParameters(List<MethodParameterReferenceViewModel> data)
        {

        }

        private void UpdatedParentInputParameters(List<MethodParameterReferenceViewModel> data)
        {

        }

        private void AddCurrentChildOutputParameters(List<MethodParameterReferenceViewModel> childOutputParameters)
        {
            foreach (var item in Sentence.OutputParameters)
            {
                childOutputParameters.Add(new MethodParameterReferenceViewModel(Sentence, item));
            }
        }

        private void AddCurrentChildInputParameters(List<MethodParameterReferenceViewModel> childInputParameters)
        {
            foreach (var item in Sentence.InputParameters)
            {
                childInputParameters.Add(new MethodParameterReferenceViewModel(Sentence, item));
            }
        }

        private List<MethodParameterReferenceViewModel> GetNewListWithOutputParameters()
        {
            var childOutputParameters = new List<MethodParameterReferenceViewModel>();
            foreach (var item in ParentOutputParameters)
            {
                childOutputParameters.Add(item);
            }

            return childOutputParameters;
        }

        private List<MethodParameterReferenceViewModel> GetNewListWithInputParameters()
        {
            var childInputParameters = new List<MethodParameterReferenceViewModel>();
            foreach (var item in ParentInputParameters)
            {
                childInputParameters.Add(item);
            }

            return childInputParameters;
        }

        public void Initialize(UseCaseSentenceContainerView v)
        {
            _view = v;
        }

        private void UpdatedSentence(UseCaseSentenceViewModel data)
        {

        }

        
    }
}
