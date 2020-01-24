using DD.Lab.Wpf.Inputs.Events;
using DD.Lab.Wpf.Models.Inputs;
using DD.Lab.Wpf.Viewmodels.Windows;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace DD.Lab.Wpf.Windows
{

    public enum WindowResponse
    {
        OK = 1,
        KO = 2,
    }

    public partial class GenericInputFormWindow : Window
    {
        public WindowResponse Response { get; set; }
        public Dictionary<string,object> Values { get; set; }

        private readonly GenericInputFormWindowViewModel _viewModel = null;

        public GenericInputFormWindow(GenericFormModel model)
        {
            InitializeComponent();
            _viewModel = Resources["ViewModel"] as GenericInputFormWindowViewModel;
            _viewModel.Initialize(this, model);
            Response = WindowResponse.KO;

            
        }

        

        private void GenericFormControlView_ValueSetChanged(object sender, RoutedEventArgs e)
        {
            var myEvent = e as ValueSetChangedEventArgs;
            _viewModel.Values = myEvent.Data;
        }
    }
}
