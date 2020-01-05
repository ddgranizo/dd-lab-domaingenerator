using DD.Lab.Services.System.Implementations;
using DD.Lab.Services.System.Interfaces;
using DD.Lab.Wpf.Drm;
using DomainGeneratorUI.Services;
using System;
using System.Collections.Generic;
using System.Text;
using DomainGeneratorUI.Extensions;
using DomainGeneratorUI.Windows;
using DD.Lab.Wpf.ViewModels.Base;
using DD.Lab.Wpf.Drm.Models;
using DomainGeneratorUI.Models;
using DomainGeneratorUI.Models.UseCases;

namespace DomainGeneratorUI.Viewmodels
{
    public class EditUseCaseWindowViewmodel : BaseViewModel
    {

        public UseCaseContent Content { get { return GetValue<UseCaseContent>(); } set { SetValue(value); } }


        public EditUseCaseWindowViewmodel()
        {

        }

        private EditUseCaseWindow _view;

        public void Initialize(EditUseCaseWindow view)
        {
            _view = view;
        }
    }
}
