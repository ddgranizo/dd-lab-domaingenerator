
using DomainGeneratorUI.Controls.Sentences;
using DomainGeneratorUI.Events;
using DomainGeneratorUI.Inputs;
using DomainGeneratorUI.Models.UseCases.Sentences.Base;
using DomainGeneratorUI.Viewmodels;
using DomainGeneratorUI.Viewmodels.UseCases.Sentences.Base;
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
        public static readonly RoutedEvent UpdatedUseCaseEvent =
                    EventManager.RegisterRoutedEvent(nameof(UpdatedUseCase), RoutingStrategy.Bubble,
                    typeof(RoutedEventHandler), typeof(UseCaseSentenceCollectionManagerView));

        public event RoutedEventHandler UpdatedUseCase
        {
            add { AddHandler(UpdatedUseCaseEvent, value); }
            remove { RemoveHandler(UpdatedUseCaseEvent, value); }
        }

        public void RaiseUpdateUseCaseSentenceEvent(UseCaseSentenceViewModel useCaseViewModel, UseCaseSentence useCaseSentence)
        {
            RoutedEventArgs args = new UpdatedUseCaseSentenceEventArgs()
            {
                UseCaseViewModel = useCaseViewModel,
                UseCase = useCaseSentence,
            };
            args.RoutedEvent = UpdatedUseCaseEvent;
            RaiseEvent(args);
        }

        public void RaiseUpdateUseCaseSentenceEvent(UpdatedUseCaseSentenceEventArgs args)
        {
            args.RoutedEvent = UpdatedUseCaseEvent;
            RaiseEvent(args);
        }


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

        public UseCaseSentenceCollectionManagerViewModel ViewModel { get; }

        public UseCaseSentenceCollectionManagerView()
        {
            InitializeComponent();
			ViewModel = Resources["ViewModel"] as UseCaseSentenceCollectionManagerViewModel;
            ViewModel.Initialize(this);
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
            ViewModel.UseCaseSentenceCollectionManagerInputData = data;
        }

        private void UseCaseSentenceManagerView_UpdatedUseCase(object sender, RoutedEventArgs e)
        {
            var data = e as UpdatedUseCaseSentenceEventArgs;
            RaiseUpdateUseCaseSentenceEvent(data);
        }
    }
}
