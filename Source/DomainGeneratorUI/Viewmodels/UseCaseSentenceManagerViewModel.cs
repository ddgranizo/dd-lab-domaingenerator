using DD.Lab.Wpf.Drm;
using DD.Lab.Wpf.ViewModels.Base;
using DomainGeneratorUI.Controls;
using DomainGeneratorUI.Inputs;
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
    public class UseCaseSentenceManagerViewModel : BaseViewModel
    {

        public UseCaseSentenceViewModel Sentence { get { return GetValue<UseCaseSentenceViewModel>(); } set { SetValue(value, UpdatedSentence); } }
        public GenericManager GenericManager { get { return GetValue<GenericManager>(); } set { SetValue(value); } }

        private UseCaseSentenceManagerView _view;

        public UseCaseSentenceManagerViewModel()
        {
            AddSetterPropertiesTrigger(new DD.Lab.Wpf.Models.PropertiesTrigger(
                () =>
                {
                    if (Sentence.Type == Models.UseCases.Sentences.Base.UseCaseSentence.SentenceType.ExecuteRepositoryMethod)
                    {
                        _view.AddExecuteRepositoryMethodSentence(GenericManager, Sentence);
                    }
                    else if (Sentence.Type == Models.UseCases.Sentences.Base.UseCaseSentence.SentenceType.ExecuteService)
                    {

                    }

                }, nameof(Sentence), nameof(GenericManager)));

        }

        public void Initialize(UseCaseSentenceManagerView v)
        {
            _view = v;
        }


        private void UpdatedSentence(UseCaseSentenceViewModel data)
        {
            
        }
    }
}
