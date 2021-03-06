﻿using System;
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

namespace UIClient.Views
{
    /// <summary>
    /// Interaction logic for InputTextBox.xaml
    /// </summary>
    public partial class InputTextBox : Window
    {
        public enum InputTextBoxResponse
        {
            Ok = 1,
        }

        public InputTextBoxResponse Response { get; set; }
        public string Description { get; set; } 
        public string Caption { get; }

        public string ReturnedText { get; set; }

        public InputTextBox(string description, string caption = null)
        {
            Description = description;
            Caption = caption;

            this.DataContext = this;
            InitializeComponent();
            TextBox.Focus();
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Response = InputTextBoxResponse.Ok;
            ReturnedText = TextBox.Text;
            this.Close();
        }
    }
}
