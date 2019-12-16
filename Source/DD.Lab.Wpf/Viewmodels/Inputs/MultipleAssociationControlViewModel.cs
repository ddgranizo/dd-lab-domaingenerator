using DD.Lab.Wpf.Controls.Inputs;
using DD.Lab.Wpf.Models.Inputs;
using DD.Lab.Wpf.ViewModels.Base;
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


namespace DD.Lab.Wpf.Viewmodels.Inputs
{
    public class MultipleAssociationControlViewModel : BaseViewModel
    {

        public List<EntityReferenceValue> AvailableValues { get { return GetValue<List<EntityReferenceValue>>(); } set { SetValue(value, UpdatedAvailableValues); } }

        public List<EntityReferenceValue> InitialValues { get { return GetValue<List<EntityReferenceValue>>(); } set { SetValue(value, UpdatedInitialValues); } }

        public List<AssociationEntityReferenceValue> AssociationState { get { return GetValue<List<AssociationEntityReferenceValue>>(); } set { SetValue(value); UpdateListToCollection(value, AssociationStateCollection); } }
        public ObservableCollection<AssociationEntityReferenceValue> AssociationStateCollection { get; set; } = new ObservableCollection<AssociationEntityReferenceValue>();


        
        private MultipleAssociationControlView _view;

		public MultipleAssociationControlViewModel()
        {
			
        }

        public void Initialize(MultipleAssociationControlView v)
        {
			_view = v;
        }

        public void UpdatedAvailableValues(List<EntityReferenceValue> values)
        {
            var allState = new List<AssociationEntityReferenceValue>();
            foreach (var item in values)
            {
                allState.Add(new AssociationEntityReferenceValue() { Value = item, IsSelected = false });
            }
            AssociationState = allState;
            UpdatedInitialValues(InitialValues);
        }

        public void UpdatedInitialValues(List<EntityReferenceValue> values)
        {
            if (values != null && AssociationState != null)
            {
                foreach (var item in values)
                {
                    var stateLine = AssociationState.First(k => k.Value.Id == item.Id);
                    stateLine.IsSelected = true;
                }
            }
        }

        public void UpdatedChecked()
        {
            _view.RaiseValueChangedEvent(AssociationState.Where(k=>k.IsSelected).Select(k => k.Value).ToList());
        }
    }
}
