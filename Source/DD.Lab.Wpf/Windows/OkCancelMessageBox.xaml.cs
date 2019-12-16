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

namespace DD.Lab.Wpf.Windows
{
    /// <summary>
    /// Interaction logic for InputTextMessageBox.xaml
    /// </summary>
    public partial class OkCancelMessageBox : Window
    {
        public enum InputTextBoxResponse
        {
            OK = 1,
            KO = 0,
        }

        public InputTextBoxResponse Response { get; set; }
        public string Description { get; set; }
        public string Caption { get; }



        public OkCancelMessageBox(string description, string caption = null)
        {
            Description = description;
            Caption = caption;

            this.DataContext = this;
            InitializeComponent();
            SetCenteredWindows();
        }

        private void SetCenteredWindows()
        {
            Application curApp = Application.Current;
            Window mainWindow = curApp.MainWindow;
            this.Left = mainWindow.Left + (mainWindow.Width - this.ActualWidth) / 2;
            this.Top = mainWindow.Top + (mainWindow.Height - this.ActualHeight) / 2;
        }


        private void Ok_Click(object sender, RoutedEventArgs e)
        {
            Response = InputTextBoxResponse.OK;
            this.Close();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Response = InputTextBoxResponse.KO;
            this.Close();
        }
    }
}
