using DD.Lab.Wpf.Commands.Base;
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

        public bool CanMoveUp { get { return GetValue<bool>(); } set { SetValue(value); } }
        public bool CanMoveDown { get { return GetValue<bool>(); } set { SetValue(value); } }
        public bool CanCopy { get { return GetValue<bool>(); } set { SetValue(value); } }
        public bool CanPaste { get { return GetValue<bool>(); } set { SetValue(value); } }
        public bool CanDelete { get { return GetValue<bool>(); } set { SetValue(value); } }

        private UseCaseSentenceContainerView _view;

        public UseCaseSentenceContainerViewModel()
        {
            RegisterCommands();

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


        public ICommand CopyCommand { get; set; }
        public ICommand PasteCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        public ICommand MoveUpCommand { get; set; }
        public ICommand MoveDownCommand { get; set; }

        private void RegisterCommands()
        {
            CopyCommand = new RelayCommand((data) =>
            {
                _view.RaiseCopiedUseCaseSentenceEvent();
            },(data) => { return CanCopy; });

            PasteCommand = new RelayCommand((data) =>
            {
                _view.RaisePastedUseCaseSentenceEvent();
            }, (data) => { return CanPaste; });

            DeleteCommand = new RelayCommand((data) =>
            {
                _view.RaiseDeletedUseCaseSentenceEvent();
            }, (data) => { return CanDelete; });

            MoveUpCommand = new RelayCommand((data) =>
            {
                _view.RaiseMovedUpUseCaseSentenceEvent();
            }, (data) => { return CanMoveUp; });

            MoveDownCommand = new RelayCommand((data) =>
            {
                _view.RaiseMovedDownUseCaseSentenceEvent();
            }, (data) => { return CanMoveDown; });

            RegisterCommand(CopyCommand);
            RegisterCommand(PasteCommand);
            RegisterCommand(DeleteCommand);
            RegisterCommand(MoveUpCommand);
            RegisterCommand(MoveDownCommand);
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
