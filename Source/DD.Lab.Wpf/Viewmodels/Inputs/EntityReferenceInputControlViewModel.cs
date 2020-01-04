using DD.Lab.Wpf.Commands.Base;
using DD.Lab.Wpf.Controls.Inputs;
using DD.Lab.Wpf.Models.Inputs;
using DD.Lab.Wpf.ViewModels.Base;
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


namespace DD.Lab.Wpf.Viewmodels.Inputs
{
    public class EntityReferenceInputControlViewModel : BaseViewModel
    {
        public WpfEventManager WpfEventManager { get { return GetValue<WpfEventManager>(); } set { SetValue(value); } }


        public EntityReferenceValue EntityReference { get { return GetValue<EntityReferenceValue>(); } set { SetValue(value, UpdatedEntityReference); } }

        public GenericFormInputModel InputModel { get { return GetValue<GenericFormInputModel>(); } set { SetValue(value, UpdatedInputModel); } }

        public List<EntityReferenceValue> EntityReferences { get { return GetValue<List<EntityReferenceValue>>(); } set { SetValue(value); UpdateListToCollection(value, EntityReferencesCollection); } }
        public ObservableCollection<EntityReferenceValue> EntityReferencesCollection { get; set; } = new ObservableCollection<EntityReferenceValue>();

        public EntityReferenceValue CurrentEntityReference { get { return GetValue<EntityReferenceValue>(); } set { SetValue(value, UpdatedCurrentEntityReference); } }

        private EntityReferenceInputControlView _view;


        public EntityReferenceInputControlViewModel()
        {
            InitializeCommands();
        }

        public ICommand DeleteEntityReferenceCommand { get; set; }
        public ICommand OpenRecordCommand { get; set; }
        private void InitializeCommands()
        {
            DeleteEntityReferenceCommand = new RelayCommand((data) =>
            {
                CurrentEntityReference = null;

            }, (data) =>
            {
                return CurrentEntityReference != null;
            });

            OpenRecordCommand = new RelayCommand((data) =>
            {
                WpfEventManager.RaiseOnInputClicked(this, CurrentEntityReference.LogicalName, CurrentEntityReference.Id);
            }, (data) =>
            {
                return CurrentEntityReference != null;
            });

            RegisterCommand(DeleteEntityReferenceCommand);
            RegisterCommand(OpenRecordCommand);
        }

        public void Initialize(EntityReferenceInputControlView v)
        {
            _view = v;
        }

        private void UpdatedCurrentEntityReference(EntityReferenceValue entityReference)
        {
            _view.RaiseValueChangedEvent(entityReference);
        }


        private void UpdatedEntityReference(EntityReferenceValue entityReference)
        {
            SetInitialValue();
        }


        private void UpdatedInputModel(GenericFormInputModel inputModel)
        {
            if (inputModel.Type == GenericFormInputModel.TypeValue.EntityReference)
            {
                EntityReferences = inputModel.EntityReferenceSuggestionHandler.GetFirstPageValues().ToList();
            }
            SetInitialValue();
        }


        private void SetInitialValue()
        {
            if (EntityReference != null && EntityReferences != null)
            {
                var current = EntityReferences.FirstOrDefault(k => k.Id == EntityReference.Id);
                if (current != null)
                {
                    CurrentEntityReference = current;
                }
                else
                {
                    _view.RaiseValueChangedEvent(null);
                }
            }
        }
    }
}
