using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Xml.Serialization;
using UIClient.UserControls;
using UIClient.ViewModels.Base;

namespace UIClient.ViewModels
{
    public class HierarchyItemControlViewModel : BaseViewModel
    {

		public string Text { get { return GetValue<string>(); } set { SetValue(value); } }

		public PackIconKind Icon { get { return GetValue<PackIconKind>(); } set { SetValue(value); } }

		public bool IsCollapsible { get { return GetValue<bool>(); } set { SetValue(value, IsCollapsibleUpdated); RaiseAllPropertiesChange(); RaisePropertyChange(nameof(ShowDoCollapse), nameof(ShowDoUncollapse)); } }

        public bool IsCollapsed { get { return GetValue<bool>(); } set { SetValue(value, CollapsedUpdated); RaisePropertyChange(nameof(ShowDoCollapse), nameof(ShowDoUncollapse)); } }

        public bool ShowDoCollapse { get { return IsCollapsible && !IsCollapsed; } }
        public bool ShowDoUncollapse { get { return IsCollapsible && IsCollapsed; } }

        private HierarchyItemControlView _view;

		public HierarchyItemControlViewModel()
        {
			
        }

        public void Initialize(HierarchyItemControlView v)
        {
			_view = v;

        }
        public void CollapsedUpdated(bool value)
        {
            if (IsCollapsible)
            {
                _view.RaiseCollapsedChangedEvent(value);
            }
        }

        public void IsCollapsibleUpdated(bool value)
        {
            IsCollapsed = false;
        }

    }
}
