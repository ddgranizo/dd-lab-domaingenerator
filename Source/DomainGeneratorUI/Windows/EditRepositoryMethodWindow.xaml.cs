﻿using DD.Lab.Wpf.Drm;
using DomainGeneratorUI.Events;
using DomainGeneratorUI.Interfaces;
using DomainGeneratorUI.Models.Methods;
using DomainGeneratorUI.Models.RepositoryMethods;
using DomainGeneratorUI.Models.UseCases;
using DomainGeneratorUI.Viewmodels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DomainGeneratorUI.Windows
{

    public enum EditorWindowResponse
    {
        OK = 1,
        KO = 2,
    }

    public partial class EditRepositoryMethodWindow : Window, IContentEditor<RepositoryMethodContent>
    {

        private readonly EditRepositoryMethodWindowViewModel _viewModel = null;

        public RepositoryMethodContent ResponseContent { get; set; }
        public EditorWindowResponse Response { get; set; }

        public EditRepositoryMethodWindow()
        {
            InitializeComponent();

            _viewModel = Resources["ViewModel"] as EditRepositoryMethodWindowViewModel;
            _viewModel.Initialize(this);
        }
        
        public RepositoryMethodContent GetContent()
        {
            return ResponseContent;
        }

        public EditorWindowResponse GetResponse()
        {
            return Response;
        }

        public void SetContext(GenericManager manager, RepositoryMethodContent instance)
        {
            _viewModel.GenericManager = manager;
            _viewModel.Content = instance;
        }

        private void InputParametersManagerControlView_OnModifiedList(object sender, RoutedEventArgs e)
        {
            var myEvent = e as OnModifiedMethodParameterListEventArgs;
            var currentInputs = _viewModel.ContentView.Parameters.Where(k => k.Direction == Models.Methods.MethodParameter.ParameterDirection.Input).ToList();
            foreach (var item in currentInputs)
            {
                _viewModel.ContentView.Parameters.Remove(item);
            }
            _viewModel.ContentView.Parameters.AddRange(myEvent.Data);
        }

        private void OutputParametersManagerControlView_OnModifiedList(object sender, RoutedEventArgs e)
        {
            var myEvent = e as OnModifiedMethodParameterListEventArgs;
            var currentInputs = _viewModel.ContentView.Parameters.Where(k => k.Direction == Models.Methods.MethodParameter.ParameterDirection.Output).ToList();
            foreach (var item in currentInputs)
            {
                _viewModel.ContentView.Parameters.Remove(item);
            }
            _viewModel.ContentView.Parameters.AddRange(myEvent.Data);
        }

       
    }
}
