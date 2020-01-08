
using DomainGeneratorUI.Inputs;
using DomainGeneratorUI.Viewmodels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace DomainGeneratorUI.Controls
{

    public partial class UseCaseSentenceCollectionManagerView : UserControl
    {

        public UseCaseSentenceCollectionManagerInputData UseCaseSentenceCollectionManagerInputData
        {
            get
            {
                return (UseCaseSentenceCollectionManagerInputData)GetValue(UseCaseSentenceCollectionManagerInputDataProperty);
            }
            set
            {
                SetValue(UseCaseSentenceCollectionManagerInputDataProperty, value);
            }
        }

		public static readonly DependencyProperty UseCaseSentenceCollectionManagerInputDataProperty =
                      DependencyProperty.Register(
                          nameof(UseCaseSentenceCollectionManagerInputData),
                          typeof(UseCaseSentenceCollectionManagerInputData),
                          typeof(UseCaseSentenceCollectionManagerView), new FrameworkPropertyMetadata(new PropertyChangedCallback(OnPropsValueChangedHandler)));


		private readonly UseCaseSentenceCollectionManagerViewModel _viewModel = null;

        public UseCaseSentenceCollectionManagerView()
        {
            InitializeComponent();
			_viewModel = Resources["ViewModel"] as UseCaseSentenceCollectionManagerViewModel;
			_viewModel.Initialize(this);
        }


        private static void OnPropsValueChangedHandler(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
			UseCaseSentenceCollectionManagerView v = d as UseCaseSentenceCollectionManagerView;

            if (e.Property.Name == nameof(UseCaseSentenceCollectionManagerInputData))
            {
                v.SetUseCaseSentenceCollectionManagerInputData((UseCaseSentenceCollectionManagerInputData)e.NewValue);
            }
        }

        private void SetUseCaseSentenceCollectionManagerInputData(UseCaseSentenceCollectionManagerInputData data)
        {
            _viewModel.UseCaseSentenceCollectionManagerInputData = data;
        }
		
    }
}
