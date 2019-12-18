using DD.Lab.Services.System.Implementations;
using DD.Lab.Services.System.Interfaces;
using DD.Lab.Wpf.Drm;
using DomainGeneratorUI.Services;
using System;
using System.Collections.Generic;
using System.Text;
using DomainGeneratorUI.Extensions;
using DomainGeneratorUI.Windows;
using DD.Lab.Wpf.ViewModels.Base;

namespace DomainGeneratorUI.Viewmodels
{
    public class MainWindowViewmodel : BaseViewModel
    {

        public GenericManager GenericManager { get; }
        public IFileService FileService { get; }
        public IJsonParserService JsonParserService { get; }
        public StoredMetadataSchemaService StoredMetadataModel { get; }
        public StoredGenericValuesService StoredDataModel { get; }
        public BusinessWorkflowManager BusinessWorkflowManager { get; set; }

        public MainWindowViewmodel()
        {

            GenericManager = new GenericManager();
            BusinessWorkflowManager = new BusinessWorkflowManager(GenericManager);
            FileService = new FileService();
            JsonParserService = new JsonParserService();
            StoredMetadataModel = new StoredMetadataSchemaService(JsonParserService, FileService);
            StoredDataModel = new StoredGenericValuesService(JsonParserService, FileService);
            
            var currentModel = StoredMetadataModel
                .GetStoredData()
                .CompleteModel();

            GenericManager.InitializeModel(currentModel);

            GenericManager.CreateHandler = new CreateService(StoredDataModel);
            GenericManager.UpdateHandler = new UpdateService(StoredDataModel);
            GenericManager.DeleteHandler = new DeleteService(StoredDataModel);
            GenericManager.RetrieveHandler = new RetrieveService(StoredDataModel);
            GenericManager.RetrieveAllHandler = new RetrieveAllService(StoredDataModel);
            GenericManager.AssociateHandler = new AssociateService(StoredDataModel);
            GenericManager.DisassociateHandler = new DisassociateService(StoredDataModel);
            GenericManager.RetrieveAllAssociatedHandler = new RetrieveAllAssociatedService(StoredDataModel);
        }

        private MainWindow _view;

        public void Initialize(MainWindow view)
        {
            _view = view;
        }

    }
}
