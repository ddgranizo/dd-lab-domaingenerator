using DD.Lab.Wpf.Commands;
using DD.Lab.Wpf.Commands.Base;
using DD.Lab.Wpf.ViewModels.Base;
using DomainGeneratorUI.Controls;
using DomainGeneratorUI.Inputs;
using DomainGeneratorUI.Models.UseCases;
using DomainGeneratorUI.Models.UseCases.Sentences;
using DomainGeneratorUI.Models.UseCases.Sentences.Base;
using DomainGeneratorUI.Viewmodels.Methods;
using DomainGeneratorUI.Viewmodels.UseCases;
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
using DD.Basic.Extensions;
using DomainGeneratorUI.Models.Methods;
using AutoMapper;

namespace DomainGeneratorUI.Viewmodels
{
    public class UseCaseSentenceCollectionManagerViewModel : BaseViewModel
    {
        public List<SentenceType> SentenceTypes { get { return GetValue<List<SentenceType>>(); } set { SetValue(value); UpdateListToCollection(value, SentenceTypesCollection); } }
        public ObservableCollection<SentenceType> SentenceTypesCollection { get; set; } = new ObservableCollection<SentenceType>();
        public SentenceType SelectedSentenceType { get { return GetValue<SentenceType>(); } set { SetValue(value); } }
        public UseCaseContext UseCaseContext { get; set; }
        public UseCaseSentenceCollectionViewModel SentenceCollection { get { return GetValue<UseCaseSentenceCollectionViewModel>(); } set { SetValue(value); } }
        public UseCaseSentenceCollectionManagerInputData UseCaseSentenceCollectionManagerInputData { get { return GetValue<UseCaseSentenceCollectionManagerInputData>(); } set { SetValue(value, UpdatedUseCaseSentenceCollectionManagerInputData); } }

        public IMapper Mapper { get; set; }
        private UseCaseSentenceCollectionManagerView _view;

        public UseCaseSentenceCollectionManagerViewModel()
        {
            SetSentenceTypes();
            InitializeCommands();
        }

        public List<MethodParameterReferenceViewModel> GetOutputParametersForSentence(UseCaseSentenceViewModel sentence)
        {
            var parameters = new List<MethodParameterReferenceViewModel>();
            parameters.AddRange(UseCaseSentenceCollectionManagerInputData.ParentOutputParameters);
            var allSentences = UseCaseSentenceCollectionManagerInputData
                    .SentenceCollection
                    .Sentences;

            var index = allSentences.IndexOf(sentence);

            for (int i = 0; i < index; i++)
            {
                var targetSentence = allSentences[i];
                parameters.AddRange(targetSentence.OutputParameters.Select(k => new MethodParameterReferenceViewModel(targetSentence, k)));
            }
            return parameters;
        }

        public void UpdatedUseCaseSentence(UseCaseSentenceViewModel sentence, UseCaseSentence newSentence)
        {
            if (IsItemInList(sentence))
            {
                var newList = new List<UseCaseSentenceViewModel>(UseCaseSentenceCollectionManagerInputData.SentenceCollection.Sentences);
                var index = IndexInList(sentence);
                var newViewModel = Mapper.Map<UseCaseSentenceViewModel>(newSentence);
                if (index > -1)
                {
                    newList[index] = newViewModel;
                }
                else
                {
                    newList.Add(newViewModel);
                }
                UseCaseSentenceCollectionManagerInputData.SentenceCollection.Sentences = newList;
                _view.RaiseUpdateUseCaseSentenceEvent(newViewModel);
            }
        }

        public void UpdatedUseCaseSentenceInputParameters(UseCaseSentenceViewModel sentence, List<MethodParameterReferenceValueViewModel> newSentence)
        {
            if (IsItemInList(sentence))
            {
                sentence.ReferencedInputParametersValues = newSentence;
                _view.RaiseUpdateUseCaseSentenceEvent(sentence);
            }
        }

        private bool IsItemInList(UseCaseSentenceViewModel sentence)
        {
            return UseCaseSentenceCollectionManagerInputData.SentenceCollection.Sentences.IndexOf(sentence) > -1;
        }

        private int IndexInList(UseCaseSentenceViewModel sentence)
        {
            return UseCaseSentenceCollectionManagerInputData.SentenceCollection.Sentences.IndexOf(sentence);
        }

        public void CopiedUseCaseSentence(UseCaseSentenceViewModel sentence)
        {
            if (IsItemInList(sentence))
            {
                _view.RaiseCopiedUseCaseSentenceEvent(sentence);
            }
        }

        public void PastedUseCaseSentence(UseCaseSentenceViewModel sentence)
        {
            if (IsItemInList(sentence))
            {
                var clonedSentence = UseCaseSentenceCollectionManagerInputData.GenericManager.ParserService.Clone(sentence);
                var listItems = new List<UseCaseSentenceViewModel>(SentenceCollection.Sentences);
                listItems.Add(clonedSentence);
                UseCaseSentenceCollectionManagerInputData.SentenceCollection.Sentences = listItems;
                _view.RaisePastedUseCaseSentenceEvent(sentence);
            }
        }

        public void DeletedUseCaseSentence(UseCaseSentenceViewModel sentence)
        {
            if (IsItemInList(sentence))
            {
                UseCaseSentenceCollectionManagerInputData.SentenceCollection.Sentences.Remove(sentence);
                _view.RaiseDeletedUseCaseSentenceEvent(sentence);
            }
        }

        public void MovedUpUseCaseSentence(UseCaseSentenceViewModel sentence)
        {
            if (IsItemInList(sentence))
            {
                var index = IndexInList(sentence);
                UseCaseSentenceCollectionManagerInputData.SentenceCollection.Sentences.RemoveAt(index);
                UseCaseSentenceCollectionManagerInputData.SentenceCollection.Sentences.Insert(index - 1, sentence);
                _view.RaiseMovedUpUseCaseSentenceEvent(sentence);
            }
        }

        public void MovedDownUseCaseSentence(UseCaseSentenceViewModel sentence)
        {
            if (IsItemInList(sentence))
            {
                var index = IndexInList(sentence);
                UseCaseSentenceCollectionManagerInputData.SentenceCollection.Sentences.RemoveAt(index);
                UseCaseSentenceCollectionManagerInputData.SentenceCollection.Sentences.Insert(index + 1, sentence);
                _view.RaiseMovedUpUseCaseSentenceEvent(sentence);
            }
        }

        public bool GetCanCopy(UseCaseSentenceViewModel sentence)
        {
            return true;
        }

        public bool GetCanPaste(UseCaseSentenceViewModel sentence)
        {
            return UseCaseContext != null && UseCaseContext.CopiedSentence != null;
        }

        public bool GetCanDelete(UseCaseSentenceViewModel sentence)
        {
            return true;
        }

        public bool GetCanMoveUp(UseCaseSentenceViewModel sentence)
        {
            return SentenceCollection?.Sentences.First() != sentence;
        }

        public bool GetCanMoveDown(UseCaseSentenceViewModel sentence)
        {
            return SentenceCollection?.Sentences.Last() != sentence;
        }

        private void UpdatedUseCaseSentenceCollectionManagerInputData(UseCaseSentenceCollectionManagerInputData data)
        {
            if (Mapper == null)
            {
                Mapper = data.Mapper;
            }
            SentenceCollection = data.SentenceCollection;
            UseCaseContext = data.UseCaseContext;
        }

        public List<MethodParameterReferenceViewModel> GetInputParametersForSentence(UseCaseSentenceViewModel sentence)
        {
            var parameters = new List<MethodParameterReferenceViewModel>();
            parameters.AddRange(UseCaseSentenceCollectionManagerInputData.ParentInputParameters);
            var allSentences = UseCaseSentenceCollectionManagerInputData
                    .SentenceCollection
                    .Sentences;

            var index = allSentences
                    .IndexOf(sentence);

            for (int i = 0; i < index; i++)
            {
                var targetSentence = allSentences[i];
                parameters.AddRange(targetSentence.OutputParameters.Select(k => new MethodParameterReferenceViewModel(targetSentence, k)));
            }
            return parameters;
        }


        private void SetSentenceTypes()
        {
            var sentences = new List<SentenceType>();
            foreach (UseCaseSentence.SentenceType item
                in (UseCaseSentence.SentenceType[])Enum.GetValues(typeof(UseCaseSentence.SentenceType)))
            {
                sentences.Add(new SentenceType() { Name = item.ToString(), Value = (int)item });
            }
            SentenceTypes = sentences;
        }


        public ICommand AddSentenceCommand { get; set; }
        private void InitializeCommands()
        {
            AddSentenceCommand = new RelayCommandHandled((input) =>
            {
                if (SelectedSentenceType.Value == (int)UseCaseSentence.SentenceType.ExecuteRepositoryMethod)
                {

                    var newSentence = new UseCaseSentenceViewModel() { Type = UseCaseSentence.SentenceType.ExecuteRepositoryMethod };
                    UseCaseSentenceCollectionManagerInputData.SentenceCollection.Sentences
                        .Add(newSentence);

                    _view.RaiseMovedUpUseCaseSentenceEvent(newSentence);
                    //var newInputData = new UseCaseSentenceCollectionManagerInputData()
                    //{
                    //    GenericManager = UseCaseSentenceCollectionManagerInputData.GenericManager,
                    //    ParentInputParameters = UseCaseSentenceCollectionManagerInputData.ParentInputParameters,
                    //    ParentOutputParameters = UseCaseSentenceCollectionManagerInputData.ParentOutputParameters,
                    //    SentenceCollection = new UseCaseSentenceCollectionViewModel()
                    //    {
                    //        Sentences = newCollectionSentence
                    //    },
                    //    Mapper = Mapper,
                    //};
                    //UseCaseSentenceCollectionManagerInputData = newInputData;
                }
            }, (input) =>
            {
                return SelectedSentenceType != null;
            });

            RegisterCommand(AddSentenceCommand);
        }

        public void Initialize(UseCaseSentenceCollectionManagerView v)
        {
            _view = v;
        }


    }
}
