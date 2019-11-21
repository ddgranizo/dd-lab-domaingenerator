using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using UIClient.Models.Base;
using static DD.DomainGenerator.Models.UseCase;

namespace UIClient.Models
{
    public class SchemaModel : BaseModel
    {
        public string Name { get { return GetValue<string>(); } set { SetValue(value); } }
        public bool HasUserRelationship { get { return GetValue<bool>(); } set { SetValue(value); } }
        public bool HasState { get { return GetValue<bool>(); } set { SetValue(value); } }
        public bool HasOwner { get { return GetValue<bool>(); } set { SetValue(value); } }
        public bool IsIntersection { get { return GetValue<bool>(); } set { SetValue(value); } }

        public List<SchemaPropertyModel> Properties { get { return GetValue<List<SchemaPropertyModel>>(); } set { SetValue(value); UpdateListToCollection(value, PropertiesCollection); } }
        public ObservableCollection<SchemaPropertyModel> PropertiesCollection { get; set; } = new ObservableCollection<SchemaPropertyModel>();

        public List<UseCaseModel> UseCases { get { return GetValue<List<UseCaseModel>>(); } set { SetValue(value, UpdatedUseCases); } }
        public ObservableCollection<UseCaseModel> BasicUseCasesCollection { get; set; } = new ObservableCollection<UseCaseModel>();
        public ObservableCollection<UseCaseModel> BusinessUseCasesCollection { get; set; } = new ObservableCollection<UseCaseModel>();

        public List<RepositoryModel> Repositories { get { return GetValue<List<RepositoryModel>>(); } set { SetValue(value); UpdateListToCollection(value, RepositoriesCollection); } }
        public ObservableCollection<RepositoryModel> RepositoriesCollection { get; set; } = new ObservableCollection<RepositoryModel>();

        public List<ModelModel> Models { get { return GetValue<List<ModelModel>>(); } set { SetValue(value); UpdateListToCollection(value, ModelsCollection); } }
        public ObservableCollection<ModelModel> ModelsCollection { get; set; } = new ObservableCollection<ModelModel>();



        private void UpdatedUseCases(List<UseCaseModel> useCases)
        {
            var basicTypes = new UseCaseTypes[]
            {
                UseCaseTypes.Create,
                UseCaseTypes.DeleteByPk,
                UseCaseTypes.DeleteByUn,
                UseCaseTypes.RetrieveByPk,
                UseCaseTypes.RetrieveByUn,
                UseCaseTypes.Update
            };
            var basicUseCases = useCases.Where(k =>  Array.IndexOf(basicTypes, k.Type) > -1).ToList();
            var businessUseCases = useCases.Where(k => Array.IndexOf(basicTypes, k.Type) == -1).ToList();
            UpdateListToCollection(basicUseCases, BasicUseCasesCollection);
            UpdateListToCollection(businessUseCases, BusinessUseCasesCollection);

        }
    }
}
