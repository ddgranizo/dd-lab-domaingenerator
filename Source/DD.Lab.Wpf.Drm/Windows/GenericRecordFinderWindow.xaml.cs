using DD.Lab.Wpf.Drm.Viewmodels.Windows;
using DD.Lab.Wpf.Inputs.Events;
using DD.Lab.Wpf.Models.Inputs;
using DD.Lab.Wpf.Viewmodels.Windows;
using DD.Lab.Wpf.Windows;
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

namespace DD.Lab.Wpf.Drm
{
    public partial class GenericRecordFinderWindow : Window
    {
        public WindowResponse Response { get; set; }
        public object ResponseValue { get; set; }

        private readonly GenericRecordFinderViewmodel _viewModel = null;

        public GenericRecordFinderWindow(GenericManager manager,
            string mainEntityLogicalName,
            Guid mainEntityId,
            string targetEntityLogicalName)
        {
            InitializeComponent();
            _viewModel = Resources["ViewModel"] as GenericRecordFinderViewmodel;
            _viewModel.Initialize(this, manager, mainEntityLogicalName, mainEntityId, targetEntityLogicalName);
            Response = WindowResponse.KO;
          
        }

        public void SetResponseValue(object value)
        {
            ResponseValue = value;
        }
    }
}
