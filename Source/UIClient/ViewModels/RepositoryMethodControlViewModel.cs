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
using UIClient.Utilities;
using UIClient.ViewModels.Base;

namespace UIClient.ViewModels
{
    public class RepositoryMethodControlViewModel : BaseViewModel
    {
		public RepositoryMethodModel RepositoryMethod { get { return GetValue<RepositoryMethodModel>(); } set { SetValue(value, UpdatedSchemaView); } }
        public string InputParameterList { get { return GetValue<string>(); } set { SetValue(value); } }
        public DomainEventManager EventManager { get { return GetValue<DomainEventManager>(); } set { SetValue(value); } }
        public string OutputParameterList { get { return GetValue<string>(); } set { SetValue(value); } }

        private RepositoryMethodControlView _view;

        public bool IsGeneralOpen { get { return GetValue<bool>(); } set { SetValue(value); } }
        public bool IsAttributesOpen { get { return GetValue<bool>(); } set { SetValue(value); } }

        public RepositoryMethodControlViewModel()
        {
			
        }

        private void UpdatedSchemaView(RepositoryMethodModel schemaViewModel)
        {
            RaisePropertyChange(
                nameof(InputTypesDisplayName),
                nameof(OutputTypeDisplayName),
                nameof(DisplayName));
        }

        public void Initialize(RepositoryMethodControlView v)
        {
			_view = v;
        }

        public string DisplayName
        {
            get
            {
                return RepositoryMethod == null
                    ? string.Empty
                    : $"{RepositoryMethod.Name}";
            }
        }

        public string InputTypesDisplayName
        {
            get
            {
                return RepositoryMethod == null
                    ? string.Empty
                    : string.Join(",", RepositoryMethod.InputParameters.Select(k => $"{StringFormats.GetTypeDisplayName(k.Type)} {k.Name}"));
            }
        }

        public string OutputTypeDisplayName
        {
            get
            {
                return RepositoryMethod == null
                    ? string.Empty
                    : StringFormats.GetDataParametersDisplayName(RepositoryMethod.OutputParameters);
            }
        }
    }
}
