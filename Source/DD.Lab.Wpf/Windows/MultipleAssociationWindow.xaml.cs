using DD.Lab.Wpf.Inputs.Events;
using DD.Lab.Wpf.Models.Inputs;
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
    public partial class MultipleAssociationWindow : Window
    {

        public enum MultipleAssociationResponse
        {
            OK = 1,
            KO = 0,
        }

        public MultipleAssociationResponse Response { get; set; }
        public string Description { get; set; }
        public string Caption { get; }
        public List<EntityReferenceValue> AvailableValues { get; set; }
        public List<EntityReferenceValue> InitialValues { get; set; }
        public List<EntityReferenceValue> SelectedValues { get; set; }

        public MultipleAssociationWindow(string description, string caption, List<EntityReferenceValue> availableValues, List<EntityReferenceValue> initialValues)
        {
            Description = description;
            Caption = caption;
            AvailableValues = availableValues ?? throw new ArgumentNullException(nameof(availableValues));
            InitialValues = initialValues;
            this.DataContext = this;
            InitializeComponent();
            SetCenteredWindows();
            SelectedValues = InitialValues;
        }

        private void SetCenteredWindows()
        {
            Application curApp = Application.Current;
            Window mainWindow = curApp.MainWindow;
            this.Left = mainWindow.Left + (mainWindow.Width - this.ActualWidth) / 2;
            this.Top = mainWindow.Top + (mainWindow.Height - this.ActualHeight) / 2;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Response = MultipleAssociationResponse.OK;
            this.Close();
        }

        private void MultipleAssociationControlView_ValueChanged(object sender, RoutedEventArgs e)
        {
            var myEvent = e as MultipleAssociationValueChangedEventArgs;
            SelectedValues = myEvent.Value;
        }
    }
}
