using DD.Lab.Wpf.Commands;
using DD.Lab.Wpf.Commands.Base;
using DD.Lab.Wpf.Drm;
using DD.Lab.Wpf.Drm.Services;
using DD.Lab.Wpf.Drm.Services.Implementations;
using DD.Lab.Wpf.ViewModels.Base;
using DomainGeneratorUI.Controls.Sentences;
using DomainGeneratorUI.Extensions;
using DomainGeneratorUI.Inputs;
using DomainGeneratorUI.Models;
using DomainGeneratorUI.Models.UseCases.Sentences;
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


namespace DomainGeneratorUI.Viewmodels.Sentences
{
    public class ExecuteRepositoryMethodSentenceViewModel : BaseViewModel
    {

        public ExecuteRepositoryMethodSentenceInputData ExecuteRepositoryMethodSentenceInputData { get { return GetValue<ExecuteRepositoryMethodSentenceInputData>(); } set { SetValue(value, UpdatedExecuteRepositoryMethodSentenceInputData); } }
        public UseCaseSentenceViewModel BasicSentence { get { return GetValue<UseCaseSentenceViewModel>(); } set { SetValue(value, UpdatedSentence); } }
        public GenericManager GenericManager { get; set; }
        public ExecuteRepositoryMethodSentence Sentence { get; set; }
        private ExecuteRepositoryMethodSentenceView _view;

        public IJsonParserService JsonParserService { get; set; }

        public ExecuteRepositoryMethodSentenceViewModel()
        {
            JsonParserService = new JsonParserService();
            InitializeCommands();
        }

        public void Initialize(ExecuteRepositoryMethodSentenceView v)
        {
			_view = v;
        }

        public ICommand EditCommand { get; set; }
        private void InitializeCommands()
        {
            EditCommand = new RelayCommandHandled((input) => {

                var current = Sentence.RepositoryMethod;
                var finderRecord = new GenericRecordFinderWindow(GenericManager, Project.LogicalName, Guid.Empty, RepositoryMethod.LogicalName);
                finderRecord.ShowDialog();
                if (finderRecord.Response == DD.Lab.Wpf.Windows.WindowResponse.OK)
                {
                    
                }
            });

            RegisterCommand(EditCommand);
        }

        private void UpdatedExecuteRepositoryMethodSentenceInputData(ExecuteRepositoryMethodSentenceInputData data)
        {
            BasicSentence = data.Sentence;
            GenericManager = data.GenericManager;
        }

        private void UpdatedSentence(UseCaseSentenceViewModel data)
        {
            Sentence = BasicSentence.ToExecuteRepositoryMethod(JsonParserService);
        }
    }
}
