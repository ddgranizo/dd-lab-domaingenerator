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
using DD.Lab.Wpf.Drm.Models;
using DomainGeneratorUI.Models;

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

            var model = new MetadataModel();
            model.AddEntity(new Project());
            model.AddEntity(new Domain());
            model.AddEntity(new Setting());
            model.AddEntity(new Models.Environment());
            model.AddEntity(new Schema());
            model.AddEntity(new Property());
            model.AddEntity(new Model());
            model.AddEntity(new Repository());
            model.AddEntity(new UseCase());
            model.AddEntity(new RepositoryMethod());
            model.AddEntity(new MethodParameter());

            model.AddRelationship(new Project(), new Domain());
            model.AddRelationship(new Project(), new Setting());
            model.AddRelationship(new Project(), new Models.Environment());
            model.AddRelationship(new Domain(), new Schema());
            model.AddRelationship(new Schema(), new Property());
            //model.AddRelationship(new Schema(), new Property(), nameof(Property.EntityReferenceSchemaId));
            model.AddRelationship(new Schema(), new Model());
            model.AddRelationship(new Schema(), new Repository());
            model.AddRelationship(new Schema(), new UseCase());
            model.AddRelationship(new Repository(), new RepositoryMethod());
            model.AddRelationship(new RepositoryMethod(), new MethodParameter());

            model.AddManyTwoManyRelationship(new Property(), new Model());
            model.AddManyTwoManyRelationship(new Property(), new Schema(), "ReferencedSchema");

           
            GenericManager.InitializeModel(model);

            GenericManager.CreateHandler = new CreateService(StoredDataModel);
            GenericManager.UpdateHandler = new UpdateService(StoredDataModel);
            GenericManager.DeleteHandler = new DeleteService(StoredDataModel, model, true);
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
