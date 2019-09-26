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
using UIClient.ViewModels;

namespace UIClient.Views
{
    /// <summary>
    /// Interaction logic for Main.xaml
    /// </summary>
    public partial class Main : Window
    {
        public MainViewModel ViewModel { get; set; }
        public Main()
        {
            InitializeComponent();
            ViewModel = new MainViewModel(this);
            this.DataContext = ViewModel;
        }

        private void MainGrid_Drop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            if (files.Length>0)
            {
                ViewModel.DraggedFiles(files);
            }
        }
    }
}
