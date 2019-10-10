using DD.DomainGenerator.Actions.Base;
using DD.DomainGenerator.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using UIClient.Models.Base;
using UIClient.ViewModels;

namespace UIClient.Models
{
    public class WorkspaceModel : BaseModel
    {

        public MainViewModel Vm { get; }
        public WorkspaceModel(MainViewModel viewModel)
        {
            Vm = viewModel ?? throw new ArgumentNullException(nameof(viewModel));
        }

       

    }
}
