
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

        public static readonly RoutedEvent CopiedUseCaseEvent =
                    EventManager.RegisterRoutedEvent(nameof(CopiedUseCase), RoutingStrategy.Bubble,
                    typeof(RoutedEventHandler), typeof(UseCaseSentenceCollectionManagerView));

        public static readonly RoutedEvent PastedUseCaseEvent =
                    EventManager.RegisterRoutedEvent(nameof(PastedUseCase), RoutingStrategy.Bubble,
                    typeof(RoutedEventHandler), typeof(UseCaseSentenceCollectionManagerView));

        public static readonly RoutedEvent DeletedUseCaseEvent =
                    EventManager.RegisterRoutedEvent(nameof(DeletedUseCase), RoutingStrategy.Bubble,
                    typeof(RoutedEventHandler), typeof(UseCaseSentenceCollectionManagerView));

        public static readonly RoutedEvent MovedUpUseCaseEvent =
                   EventManager.RegisterRoutedEvent(nameof(MovedUpUseCase), RoutingStrategy.Bubble,
                   typeof(RoutedEventHandler), typeof(UseCaseSentenceCollectionManagerView));

        public static readonly RoutedEvent MovedDownUseCaseEvent =
                  EventManager.RegisterRoutedEvent(nameof(MovedDownUseCase), RoutingStrategy.Bubble,
                  typeof(RoutedEventHandler), typeof(UseCaseSentenceCollectionManagerView));

        
        public event RoutedEventHandler CopiedUseCase
        {
            add { AddHandler(CopiedUseCaseEvent, value); }
            remove { RemoveHandler(CopiedUseCaseEvent, value); }
        }

        public event RoutedEventHandler PastedUseCase
        {
            add { AddHandler(PastedUseCaseEvent, value); }
            remove { RemoveHandler(PastedUseCaseEvent, value); }
        }

        public event RoutedEventHandler DeletedUseCase
        {
            add { AddHandler(DeletedUseCaseEvent, value); }
            remove { RemoveHandler(DeletedUseCaseEvent, value); }
        }

        public event RoutedEventHandler MovedUpUseCase
        {
            add { AddHandler(MovedUpUseCaseEvent, value); }
            remove { RemoveHandler(MovedUpUseCaseEvent, value); }
        }

        public event RoutedEventHandler MovedDownUseCase
        {
            add { AddHandler(MovedDownUseCaseEvent, value); }
            remove { RemoveHandler(MovedDownUseCaseEvent, value); }
        }

        public void RaiseCopiedUseCaseSentenceEvent(UseCaseSentenceViewModel sentenceViewModel)
        {
            RaiseRouteEventWithCurrentSentence(CopiedUseCaseEvent, sentenceViewModel);
        }

        public void RaisePastedUseCaseSentenceEvent(UseCaseSentenceViewModel sentenceViewModel)
        {
            RaiseRouteEventWithCurrentSentence(PastedUseCaseEvent, sentenceViewModel);
        }

        public void RaiseDeletedUseCaseSentenceEvent(UseCaseSentenceViewModel sentenceViewModel)
        {
            RaiseRouteEventWithCurrentSentence(DeletedUseCaseEvent, sentenceViewModel);
        }

        public void RaiseMovedUpUseCaseSentenceEvent(UseCaseSentenceViewModel sentenceViewModel)
        {
            RaiseRouteEventWithCurrentSentence(MovedUpUseCaseEvent, sentenceViewModel);
        }

        public void RaiseMovedDownUseCaseSentenceEvent(UseCaseSentenceViewModel sentenceViewModel)
        {
            RaiseRouteEventWithCurrentSentence(MovedDownUseCaseEvent, sentenceViewModel);
        }

        private void RaiseRouteEventWithCurrentSentence(RoutedEvent routedEvent, UseCaseSentenceViewModel sentenceViewModel)
        {
            RoutedEventArgs args = new UpdatedUseCaseSentenceEventArgs()
            {
                UseCaseViewModel = sentenceViewModel,
            };
            args.RoutedEvent = routedEvent;
            RaiseEvent(args);
        }


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

        private void UseCaseSentenceContainerView_CopiedUseCase(object sender, RoutedEventArgs e)
        {
            var data = e as UpdatedUseCaseSentenceEventArgs;
            if (data.Source is UseCaseSentenceContainerView 
                && ((UseCaseSentenceContainerView)data.Source).ViewModel.Sentence == data.UseCaseViewModel)
            {
                ViewModel.CopiedUseCaseSentence(data.UseCaseViewModel);
            }
        }

        private void UseCaseSentenceContainerView_PastedUseCase(object sender, RoutedEventArgs e)
        {
            var data = e as UpdatedUseCaseSentenceEventArgs;
            if (data.Source is UseCaseSentenceContainerView
                && ((UseCaseSentenceContainerView)data.Source).ViewModel.Sentence == data.UseCaseViewModel)
            {
                ViewModel.PastedUseCaseSentence(data.UseCaseViewModel);
            }
        }

        private void UseCaseSentenceContainerView_DeletedUseCase(object sender, RoutedEventArgs e)
        {
            var data = e as UpdatedUseCaseSentenceEventArgs;
            if (data.Source is UseCaseSentenceContainerView
                && ((UseCaseSentenceContainerView)data.Source).ViewModel.Sentence == data.UseCaseViewModel)
            {
                ViewModel.DeletedUseCaseSentence(data.UseCaseViewModel);
            }
        }

        private void UseCaseSentenceContainerView_MovedDownUseCase(object sender, RoutedEventArgs e)
        {
            var data = e as UpdatedUseCaseSentenceEventArgs;
            if (data.Source is UseCaseSentenceContainerView
                && ((UseCaseSentenceContainerView)data.Source).ViewModel.Sentence == data.UseCaseViewModel)
            {
                ViewModel.MovedDownUseCaseSentence(data.UseCaseViewModel);
            }
        }

        private void UseCaseSentenceContainerView_MovedUpUseCase(object sender, RoutedEventArgs e)
        {
            var data = e as UpdatedUseCaseSentenceEventArgs;
            if (data.Source is UseCaseSentenceContainerView
                && ((UseCaseSentenceContainerView)data.Source).ViewModel.Sentence == data.UseCaseViewModel)
            {
                ViewModel.MovedUpUseCaseSentence(data.UseCaseViewModel);
            }
        }
    }
}
