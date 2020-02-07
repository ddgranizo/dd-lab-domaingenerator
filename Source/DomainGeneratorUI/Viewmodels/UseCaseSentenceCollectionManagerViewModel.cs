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


namespace DomainGeneratorUI.Viewmodels
{
    public class UseCaseSentenceCollectionManagerViewModel : BaseViewModel
    {
        public List<SentenceType> SentenceTypes { get { return GetValue<List<SentenceType>>(); } set { SetValue(value); UpdateListToCollection(value, SentenceTypesCollection); } }
        public ObservableCollection<SentenceType> SentenceTypesCollection { get; set; } = new ObservableCollection<SentenceType>();
        public SentenceType SelectedSentenceType { get { return GetValue<SentenceType>(); } set { SetValue(value); } }

        public UseCaseSentenceCollectionViewModel SentenceCollection { get { return GetValue<UseCaseSentenceCollectionViewModel>(); } set { SetValue(value); } }
        public UseCaseSentenceCollectionManagerInputData UseCaseSentenceCollectionManagerInputData { get { return GetValue<UseCaseSentenceCollectionManagerInputData>(); } set { SetValue(value); } }

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

            var index = allSentences
                    .IndexOf(sentence);

            for (int i = 0; i < index; i++)
            {
                var targetSentence = allSentences[i];
                parameters.AddRange(targetSentence.OutputParameters.Select(k=>new MethodParameterReferenceViewModel(targetSentence, k)));
            }
            return parameters;
        }


        private void UpdatedUseCaseSentenceCollectionManagerInputData(UseCaseSentenceCollectionManagerInputData data)
        {
            SentenceCollection = data.SentenceCollection;
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
                    var newCollectionSentence = new List<UseCaseSentenceViewModel>();
                    //var newCollection = UseCaseSentenceCollectionManagerInputData.SentenceCollection;
                    foreach (var item in UseCaseSentenceCollectionManagerInputData.SentenceCollection.Sentences)
                    {
                        newCollectionSentence.Add(item);
                    }
                    newCollectionSentence.Add(new UseCaseSentenceViewModel() { Type = UseCaseSentence.SentenceType.ExecuteRepositoryMethod });
                    var newInputData = new UseCaseSentenceCollectionManagerInputData()
                    {
                        GenericManager = UseCaseSentenceCollectionManagerInputData.GenericManager,
                        ParentInputParameters = UseCaseSentenceCollectionManagerInputData.ParentInputParameters,
                        ParentOutputParameters = UseCaseSentenceCollectionManagerInputData.ParentOutputParameters,
                        SentenceCollection = new UseCaseSentenceCollectionViewModel()
                        {
                            Sentences = newCollectionSentence
                        }
                    };
                    UseCaseSentenceCollectionManagerInputData = newInputData;
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
