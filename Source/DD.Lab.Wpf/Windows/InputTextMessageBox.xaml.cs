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
    public partial class InputTextMessageBox : Window
    {

        public enum InputTextBoxResponse
        {
            OK = 1,
            KO = 0,
        }

        public InputTextBoxResponse Response { get; set; }
        public string Description { get; set; }
        public string Caption { get; }
        public string ReturnedText { get; set; }

        
        public InputTextMessageBox(string description, string caption = null)
        {
            Description = description;
            Caption = caption;

            this.DataContext = this;
            InitializeComponent();
            TextBox.Focus();
        }

    
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Response = InputTextBoxResponse.OK;
            ReturnedText = TextBox.Text;
            this.Close();
        }

    }
}
