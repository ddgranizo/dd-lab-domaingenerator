using DD.DomainGenerator.Models;
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
using UIClient.Models;
using UIClient.UserControls;
using UIClient.ViewModels.Base;

namespace UIClient.ViewModels
{
    public class SchemaControlViewModel : BaseViewModel
    {
        public SchemaModel Schema { get { return GetValue<SchemaModel>(); } set { SetValue(value, UdpatedSchemaModel); } }
        public DomainEventManager EventManager { get { return GetValue<DomainEventManager>(); } set { SetValue(value); } }

        //public bool ShowProperties { get { return GetValue<bool>(); } set { SetValue(value); } }
        //public bool ShowUseCases { get { return GetValue<bool>(); } set { SetValue(value); } }
        //public bool ShowViews { get { return GetValue<bool>(); } set { SetValue(value); } }


        public bool IsGeneralOpen { get { return GetValue<bool>(); } set { SetValue(value); } }
        public bool IsPropertiesOpen { get { return GetValue<bool>(); } set { SetValue(value); } }

        public bool IsUseCasesOpen { get { return GetValue<bool>(); } set { SetValue(value); } }
        public bool IsBasicUseCasesOpen { get { return GetValue<bool>(); } set { SetValue(value); } }
        public bool IsBusinessUseCasesOpen { get { return GetValue<bool>(); } set { SetValue(value); } }

        public bool IsRepositoriesOpen { get { return GetValue<bool>(); } set { SetValue(value); } }
        public bool IsModelsOpen { get { return GetValue<bool>(); } set { SetValue(value); } }


        private SchemaControlView _view;

        public SchemaControlViewModel()
        {

        }


        private void UdpatedSchemaModel(SchemaModel schemaModelModel)
        {

        }

        public void Initialize(SchemaControlView v)
        {
            _view = v;
        }
    }
}
