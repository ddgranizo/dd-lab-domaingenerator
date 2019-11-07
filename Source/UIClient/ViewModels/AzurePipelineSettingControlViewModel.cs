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
    public class AzurePipelineSettingControlViewModel : BaseViewModel
    {
        public AzurePipelineSettingModel AzurePipelineSetting { get { return GetValue<AzurePipelineSettingModel>(); } set { SetValue(value); RaisePropertyChange(nameof(Url)); } }
        public string Url { get { return AzurePipelineSetting?.OrganizationUri; } }
        private AzurePipelineSettingControlView _view;

        public AzurePipelineSettingControlViewModel()
        {

        }

        public void Initialize(AzurePipelineSettingControlView v)
        {
            _view = v;
        }
    }
}
