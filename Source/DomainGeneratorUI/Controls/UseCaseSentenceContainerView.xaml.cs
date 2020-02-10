
using DD.Lab.Wpf.Drm;
using DomainGeneratorUI.Controls.Sentences;
using DomainGeneratorUI.Events;
using DomainGeneratorUI.Inputs;
using DomainGeneratorUI.Models.Methods;
using DomainGeneratorUI.Models.UseCases;
using DomainGeneratorUI.Models.UseCases.Sentences.Base;
using DomainGeneratorUI.Viewmodels;
using DomainGeneratorUI.Viewmodels.Methods;
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

    public partial class UseCaseSentenceContainerView : UserControl
    {

        public static readonly RoutedEvent UpdatedUseCaseEvent =
                    EventManager.RegisterRoutedEvent(nameof(UpdatedUseCase), RoutingStrategy.Bubble,
                    typeof(RoutedEventHandler), typeof(UseCaseSentenceContainerView));

        public static readonly RoutedEvent CopiedUseCaseEvent =
                    EventManager.RegisterRoutedEvent(nameof(CopiedUseCase), RoutingStrategy.Bubble,
                    typeof(RoutedEventHandler), typeof(UseCaseSentenceContainerView));

        public static readonly RoutedEvent PastedUseCaseEvent =
                    EventManager.RegisterRoutedEvent(nameof(PastedUseCase), RoutingStrategy.Bubble,
                    typeof(RoutedEventHandler), typeof(UseCaseSentenceContainerView));

        public static readonly RoutedEvent DeletedUseCaseEvent =
                    EventManager.RegisterRoutedEvent(nameof(DeletedUseCase), RoutingStrategy.Bubble,
                    typeof(RoutedEventHandler), typeof(UseCaseSentenceContainerView));

        public static readonly RoutedEvent MovedUpUseCaseEvent =
                   EventManager.RegisterRoutedEvent(nameof(MovedUpUseCase), RoutingStrategy.Bubble,
                   typeof(RoutedEventHandler), typeof(UseCaseSentenceContainerView));

        public static readonly RoutedEvent MovedDownUseCaseEvent =
                  EventManager.RegisterRoutedEvent(nameof(MovedDownUseCase), RoutingStrategy.Bubble,
                  typeof(RoutedEventHandler), typeof(UseCaseSentenceContainerView));

        public event RoutedEventHandler UpdatedUseCase
        {
            add { AddHandler(UpdatedUseCaseEvent, value); }
            remove { RemoveHandler(UpdatedUseCaseEvent, value); }
        }

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

        public void RaiseCopiedUseCaseSentenceEvent()
        {
            RaiseRouteEventWithCurrentSentence(CopiedUseCaseEvent);
        }

        public void RaisePastedUseCaseSentenceEvent()
        {
            RaiseRouteEventWithCurrentSentence(PastedUseCaseEvent);
        }

        public void RaiseDeletedUseCaseSentenceEvent()
        {
            RaiseRouteEventWithCurrentSentence(DeletedUseCaseEvent);
        }

        public void RaiseMovedUpUseCaseSentenceEvent()
        {
            RaiseRouteEventWithCurrentSentence(MovedUpUseCaseEvent);
        }

        public void RaiseMovedDownUseCaseSentenceEvent()
        {
            RaiseRouteEventWithCurrentSentence(MovedDownUseCaseEvent);
        }

        public void RaiseUpdateUseCaseSentenceEvent(UseCaseSentence useCaseSentence, UseCaseSentenceViewModel useCaseSentenceViewModel)
        {
            RoutedEventArgs args = new UpdatedUseCaseSentenceEventArgs()
            {
                UseCase = useCaseSentence,
                UseCaseViewModel = useCaseSentenceViewModel,
            };
            args.RoutedEvent = UpdatedUseCaseEvent;
            RaiseEvent(args);
        }

        private void RaiseRouteEventWithCurrentSentence(RoutedEvent routedEvent)
        {
            RoutedEventArgs args = new UpdatedUseCaseSentenceEventArgs()
            {
                UseCaseViewModel = Sentence,
            };
            args.RoutedEvent = routedEvent;
            RaiseEvent(args);
        }

        public void RaiseUpdateUseCaseSentenceEvent(UpdatedUseCaseSentenceEventArgs args)
        {
            args.RoutedEvent = UpdatedUseCaseEvent;
            RaiseEvent(args);
        }

        public List<MethodParameterReferenceViewModel> ParentInputParameters
        {
            get
            {
                return (List<MethodParameterReferenceViewModel>)GetValue(ParentInputParametersProperty);
            }
            set
            {
                SetValue(ParentInputParametersProperty, value);
            }
        }

        public List<MethodParameterReferenceViewModel> ParentOutputParameters
        {
            get
            {
                return (List<MethodParameterReferenceViewModel>)GetValue(ParentOutputParametersProperty);
            }
            set
            {
                SetValue(ParentOutputParametersProperty, value);
            }
        }

        public UseCaseSentenceViewModel Sentence
        {
            get
            {
                return (UseCaseSentenceViewModel)GetValue(SentenceProperty);
            }
            set
            {
                SetValue(SentenceProperty, value);
            }
        }

        public GenericManager GenericManager
        {
            get
            {
                return (GenericManager)GetValue(GenericManagerProperty);
            }
            set
            {
                SetValue(GenericManagerProperty, value);
            }
        }

        public bool CanMoveUp
        {
            get
            {
                return (bool)GetValue(GenericManagerProperty);
            }
            set
            {
                SetValue(GenericManagerProperty, value);
            }
        }

        public bool CanMoveDown
        {
            get
            {
                return (bool)GetValue(GenericManagerProperty);
            }
            set
            {
                SetValue(GenericManagerProperty, value);
            }
        }

        public bool CanCopy
        {
            get
            {
                return (bool)GetValue(GenericManagerProperty);
            }
            set
            {
                SetValue(GenericManagerProperty, value);
            }
        }

        public bool CanPaste
        {
            get
            {
                return (bool)GetValue(GenericManagerProperty);
            }
            set
            {
                SetValue(GenericManagerProperty, value);
            }
        }

        public bool CanDelete
        {
            get
            {
                return (bool)GetValue(GenericManagerProperty);
            }
            set
            {
                SetValue(GenericManagerProperty, value);
            }
        }

        public UseCaseContext UseCaseContext
        {
            get
            {
                return (UseCaseContext)GetValue(GenericManagerProperty);
            }
            set
            {
                SetValue(GenericManagerProperty, value);
            }
        }

        public static readonly DependencyProperty UseCaseContextProperty =
                      DependencyProperty.Register(
                          nameof(UseCaseContext),
                          typeof(UseCaseContext),
                          typeof(UseCaseSentenceContainerView), new FrameworkPropertyMetadata(new PropertyChangedCallback(OnPropsValueChangedHandler)));

        public static readonly DependencyProperty CanDeleteProperty =
                      DependencyProperty.Register(
                          nameof(CanDelete),
                          typeof(bool),
                          typeof(UseCaseSentenceContainerView), new FrameworkPropertyMetadata(new PropertyChangedCallback(OnPropsValueChangedHandler)));

        public static readonly DependencyProperty CanPasteProperty =
                      DependencyProperty.Register(
                          nameof(CanPaste),
                          typeof(bool),
                          typeof(UseCaseSentenceContainerView), new FrameworkPropertyMetadata(new PropertyChangedCallback(OnPropsValueChangedHandler)));

        public static readonly DependencyProperty CanCopyProperty =
                      DependencyProperty.Register(
                          nameof(CanCopy),
                          typeof(bool),
                          typeof(UseCaseSentenceContainerView), new FrameworkPropertyMetadata(new PropertyChangedCallback(OnPropsValueChangedHandler)));

        public static readonly DependencyProperty CanMoveDownProperty =
                      DependencyProperty.Register(
                          nameof(CanMoveDown),
                          typeof(bool),
                          typeof(UseCaseSentenceContainerView), new FrameworkPropertyMetadata(new PropertyChangedCallback(OnPropsValueChangedHandler)));

        public static readonly DependencyProperty CanMoveUpProperty =
                      DependencyProperty.Register(
                          nameof(CanMoveUp),
                          typeof(bool),
                          typeof(UseCaseSentenceContainerView), new FrameworkPropertyMetadata(new PropertyChangedCallback(OnPropsValueChangedHandler)));


        public static readonly DependencyProperty ParentInputParametersProperty =
                      DependencyProperty.Register(
                          nameof(ParentInputParameters),
                          typeof(List<MethodParameterReferenceViewModel>),
                          typeof(UseCaseSentenceContainerView), new FrameworkPropertyMetadata(new PropertyChangedCallback(OnPropsValueChangedHandler)));

        public static readonly DependencyProperty ParentOutputParametersProperty =
                      DependencyProperty.Register(
                          nameof(ParentOutputParameters),
                          typeof(List<MethodParameterReferenceViewModel>),
                          typeof(UseCaseSentenceContainerView), new FrameworkPropertyMetadata(new PropertyChangedCallback(OnPropsValueChangedHandler)));


        public static readonly DependencyProperty GenericManagerProperty =
                      DependencyProperty.Register(
                          nameof(GenericManager),
                          typeof(GenericManager),
                          typeof(UseCaseSentenceContainerView), new FrameworkPropertyMetadata(new PropertyChangedCallback(OnPropsValueChangedHandler)));

        public static readonly DependencyProperty SentenceProperty =
                      DependencyProperty.Register(
                          nameof(Sentence),
                          typeof(UseCaseSentenceViewModel),
                          typeof(UseCaseSentenceContainerView), new FrameworkPropertyMetadata(new PropertyChangedCallback(OnPropsValueChangedHandler)));

        public UseCaseSentenceContainerViewModel ViewModel { get; set; }

        public UseCaseSentenceContainerView()
        {
            InitializeComponent();
            ViewModel = Resources["ViewModel"] as UseCaseSentenceContainerViewModel;
            ViewModel.Initialize(this);
        }

        private static void OnPropsValueChangedHandler(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            UseCaseSentenceContainerView v = d as UseCaseSentenceContainerView;
            if (e.Property.Name == nameof(Sentence))
            {
                v.SetSentence((UseCaseSentenceViewModel)e.NewValue);
            }
            else if (e.Property.Name == nameof(GenericManager))
            {
                v.SetGenericManager((GenericManager)e.NewValue);
            }
            else if (e.Property.Name == nameof(ParentInputParameters))
            {
                v.SetParentInputParameters((List<MethodParameterReferenceViewModel>)e.NewValue);
            }
            else if (e.Property.Name == nameof(ParentOutputParameters))
            {
                v.SetParentOutputParameters((List<MethodParameterReferenceViewModel>)e.NewValue);
            }
            else if (e.Property.Name == nameof(CanCopy))
            {
                v.SetCanCopy((bool)e.NewValue);
            }
            else if (e.Property.Name == nameof(CanPaste))
            {
                v.SetCanPaste((bool)e.NewValue);
            }
            else if (e.Property.Name == nameof(CanMoveDown))
            {
                v.SetCanMoveDown((bool)e.NewValue);
            }
            else if (e.Property.Name == nameof(CanMoveUp))
            {
                v.SetCanMoveUp((bool)e.NewValue);
            }
            else if (e.Property.Name == nameof(CanDelete))
            {
                v.SetDelete((bool)e.NewValue);
            }
        }

        private void SetDelete(bool data)
        {
            ViewModel.CanDelete = data;
        }

        private void SetCanMoveUp(bool data)
        {
            ViewModel.CanMoveUp = data;
        }

        private void SetCanMoveDown(bool data)
        {
            ViewModel.CanMoveDown = data;
        }

        private void SetCanCopy(bool data)
        {
            ViewModel.CanCopy = data;
        }

        private void SetCanPaste(bool data)
        {
            ViewModel.CanPaste = data;
        }

        private void SetSentence(UseCaseSentenceViewModel data)
        {
            ViewModel.Sentence = data;
        }

        private void SetGenericManager(GenericManager data)
        {
            ViewModel.GenericManager = data;
        }

        private void SetParentInputParameters(List<MethodParameterReferenceViewModel> data)
        {
            ViewModel.ParentInputParameters = data;
        }

        private void SetParentOutputParameters(List<MethodParameterReferenceViewModel> data)
        {
            ViewModel.ParentOutputParameters = data;
        }

        public void AddExecuteRepositoryMethodSentence(
            GenericManager manager,
            UseCaseSentenceViewModel sentence,
            List<MethodParameterReferenceViewModel> parentInputParameters,
            List<MethodParameterReferenceViewModel> parentOutputParameters)
        {
            foreach (var item in SentenceGrid.Children.OfType<ExecuteRepositoryMethodSentenceView>())
            {
                item.UpdatedUseCase -= Instance_UpdatedUseCase;
            }
            SentenceGrid.Children.Clear();
            var instance = new ExecuteRepositoryMethodSentenceView()
            {
                ExecuteRepositoryMethodSentenceInputData = new ExecuteRepositoryMethodSentenceInputData()
                {
                    Sentence = sentence,
                    GenericManager = manager,
                    ParentInputParameters = parentInputParameters,
                    ParentOutputParameters = parentOutputParameters,
                }
            };
            instance.UpdatedUseCase += Instance_UpdatedUseCase;
            SentenceGrid.Children.Add(instance);
        }

        private void Instance_UpdatedUseCase(object sender, RoutedEventArgs e)
        {
            var data = e as UpdatedUseCaseSentenceEventArgs;

            if (data.UseCaseViewModel == Sentence)
            {
                RaiseUpdateUseCaseSentenceEvent(data);
            }
        }
    }
}
