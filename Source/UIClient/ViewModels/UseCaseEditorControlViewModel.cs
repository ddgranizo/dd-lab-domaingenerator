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
using UIClient.Commands;
using UIClient.Models;
using UIClient.UserControls.Editors.UseCases;
using UIClient.ViewModels.Base;

namespace UIClient.ViewModels
{
    public class UseCaseEditorControlViewModel : BaseViewModel
    {
        public DomainEventManager EventManager { get { return GetValue<DomainEventManager>(); } set { SetValue(value); } }

        public UseCaseModel UseCase { get { return GetValue<UseCaseModel>(); } set { SetValue(value); } }
        
		private UseCaseEditorControlView _view;

        public ICommand AddInputParameterCommand { get; set; }
        public ICommand AddOutputParameterCommand { get; set; }
        public ICommand RemoveInputParameterCommand { get; set; }
        public ICommand RemoveOutputParameterCommand { get; set; }
        public ICommand ModifyInputParameterCommand { get; set; }
        public ICommand ModifyOutputParameterCommand { get; set; }
        public ICommand MoveUpInputParameterCommand { get; set; }
        public ICommand MoveUpOutputParameterCommand { get; set; }
        public ICommand MoveDownInputParameterCommand { get; set; }
        public ICommand MoveDownOutputParameterCommand { get; set; }


        public Guid GenericFormRequestId { get; set; }


        public UseCaseEditorControlViewModel()
        {
            AddInputParameterCommand = new AddUseCaseEditorParameterCommand(this, AddUseCaseEditorParameterCommand.ParameterDirection.Input);
            AddOutputParameterCommand = new AddUseCaseEditorParameterCommand(this, AddUseCaseEditorParameterCommand.ParameterDirection.Output);
            RemoveInputParameterCommand = new RemoveUseCaseEditorParameterCommand(this, RemoveUseCaseEditorParameterCommand.ParameterDirection.Input);
            RemoveOutputParameterCommand = new RemoveUseCaseEditorParameterCommand(this, RemoveUseCaseEditorParameterCommand.ParameterDirection.Output);
            ModifyInputParameterCommand = new ModifyUseCaseEditorParameterCommand(this, ModifyUseCaseEditorParameterCommand.ParameterDirection.Input);
            ModifyOutputParameterCommand = new ModifyUseCaseEditorParameterCommand(this, ModifyUseCaseEditorParameterCommand.ParameterDirection.Output);
            MoveUpInputParameterCommand = new MoveUpDownUseCaseEditorParameterCommand(this, MoveUpDownUseCaseEditorParameterCommand.ParameterDirection.Input, MoveUpDownUseCaseEditorParameterCommand.MovementDirection.Up);
            MoveUpOutputParameterCommand = new MoveUpDownUseCaseEditorParameterCommand(this, MoveUpDownUseCaseEditorParameterCommand.ParameterDirection.Output, MoveUpDownUseCaseEditorParameterCommand.MovementDirection.Up);
            MoveDownInputParameterCommand = new MoveUpDownUseCaseEditorParameterCommand(this, MoveUpDownUseCaseEditorParameterCommand.ParameterDirection.Input, MoveUpDownUseCaseEditorParameterCommand.MovementDirection.Down);
            MoveDownOutputParameterCommand = new MoveUpDownUseCaseEditorParameterCommand(this, MoveUpDownUseCaseEditorParameterCommand.ParameterDirection.Output, MoveUpDownUseCaseEditorParameterCommand.MovementDirection.Down);


            RegisterCommand(AddInputParameterCommand);
            RegisterCommand(AddOutputParameterCommand);
            RegisterCommand(RemoveInputParameterCommand);
            RegisterCommand(RemoveOutputParameterCommand);
            RegisterCommand(ModifyInputParameterCommand);
            RegisterCommand(ModifyOutputParameterCommand);
            RegisterCommand(MoveUpInputParameterCommand);
            RegisterCommand(MoveUpOutputParameterCommand);
            RegisterCommand(MoveDownInputParameterCommand);
            RegisterCommand(MoveDownOutputParameterCommand);
        }

        public void Initialize(UseCaseEditorControlView v)
        {
			_view = v;
        }

    }
}
