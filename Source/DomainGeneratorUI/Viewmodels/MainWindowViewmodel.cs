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
using DomainGeneratorUI.Models.UseCases;
using DomainGeneratorUI.Interfaces;
using System.Windows;
using DomainGeneratorUI.Utilities;
using DomainGeneratorUI.Models.RepositoryMethods;

namespace DomainGeneratorUI.Viewmodels
{
    public class MainWindowViewModel : BaseViewModel
    {

        public GenericManager GenericManager { get; }
        public IFileService FileService { get; }
        public IJsonParserService JsonParserService { get; }
        public StoredMetadataSchemaService StoredMetadataModel { get; }
        public StoredGenericValuesService StoredDataModel { get; }
        public BusinessWorkflowManager BusinessWorkflowManager { get; set; }

        public MainWindowViewModel()
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


            model.AddRelationship(new Project(), new Domain());
            model.AddRelationship(new Project(), new Setting());
            model.AddRelationship(new Project(), new Models.Environment());
            model.AddRelationship(new Domain(), new Schema());
            model.AddRelationship(new Schema(), new Property());
            model.AddRelationship(new Schema(), new Model());
            model.AddRelationship(new Schema(), new Repository());
            model.AddRelationship(new Schema(), new UseCase());
            model.AddRelationship(new Repository(), new RepositoryMethod());

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

            GenericManager.OnCustomModuleContentEditRequested += GenericManager_OnCustomModuleContentEditRequested;

        }

        private void GenericManager_OnCustomModuleContentEditRequested(object sender, DD.Lab.Wpf.Events.WpfCustomModuleEventArgs args)
        {
            var contentJson = args.Content;
            string newContentJson = GetNewContentJson(args.ModuleName, contentJson);
            args.Content = newContentJson;
        }

        private string GetNewContentJson(string moduleName, string contentJson)
        {

            if (moduleName == Definitions.CustomModules.UseCaseContentModule)
            {
                return GetNewContent<UseCaseContent, EditUseCaseWindow>(contentJson);
            }
            else if (moduleName == Definitions.CustomModules.RepositoryMethodContentModule)
            {
                return GetNewContent<RepositoryMethodContent, EditRepositoryMethodWindow>(contentJson);
            }
            throw new NotImplementedException();
        }

        private string GetNewContent<TContent, TWindow>(string contentJson)
            where TContent : IInitializable<TContent>, new()
            where TWindow : Window, IContentEditor<TContent>, new()
        {

            var newContentJson = contentJson;
            bool editing = true;
            TContent content = default(TContent);
            if (!JsonUtility.IsValidJson<TContent>(contentJson))
            {
                editing = false;
                RaiseOkCancelDialog("Current content cannot be updated. The content is not a valid json schema. Do you want to generate a new empty json?",
                    "Invalid json",
                    () => { editing = true; content = JsonUtility.GetInitialInstance<TContent>(); });
            }
            else
            {
                content = JsonUtility.GetInstance<TContent>(contentJson);
            }

            if (editing)
            {
                var editContentWindow = new TWindow();
                editContentWindow.SetContext(GenericManager, content);
                editContentWindow.ShowDialog();
                if (editContentWindow.GetResponse() == EditorWindowResponse.OK)
                {
                    var newContent = editContentWindow.GetContent();
                    newContentJson = JsonUtility.Stringfy<TContent>(newContent);
                }
            }
            return newContentJson;
        }
      
        private MainWindow _view;

        public void Initialize(MainWindow view)
        {
            _view = view;
        }

    }
}
