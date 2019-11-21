using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using UIClient.Events;
using UIClient.ViewModels;

namespace UIClient.Views
{
    public partial class Main : Window
    {
        public MainViewModel ViewModel { get; set; }
        public Main()
        {
            InitializeComponent();
            ViewModel = Resources["ViewModel"] as MainViewModel;
            ViewModel.Initialize(this);
        }

        private void MainGrid_Drop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            if (files.Length>0)
            {
                ViewModel.DraggedFiles(files);
            }
        }


        private void NewActionGenericInputControlView_ValueChanged(object sender, RoutedEventArgs e)
        {
            var myEvent = e as ValueChangedEventArgs;
            ViewModel.NewActionParameterValueChanged(myEvent.ParameterDefinition, myEvent.Data);
        }

    }
}
