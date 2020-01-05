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
using DomainGeneratorUI.Models.RepositoryMethods;

namespace DomainGeneratorUI.Viewmodels
{
    public class EditRepositoryMethodWindowViewmodel : BaseViewModel
    {

        public RepositoryMethodContent Content { get { return GetValue<RepositoryMethodContent>(); } set { SetValue(value); } }


        public EditRepositoryMethodWindowViewmodel()
        {

        }

        private EditRepositoryMethodWindow _view;

        public void Initialize(EditRepositoryMethodWindow view)
        {
            _view = view;
        }
    }
}
