using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static DD.DomainGenerator.Definitions;

namespace DD.DomainGenerator.Models
{
    public class Repository
    {
        public string Name { get; set; }
        public List<RepositoryMethod> RepositoryMethods { get; set; }
        public bool IsCustom { get; set; }
        public bool IsMain { get; set; }

        public Repository(string name, bool isCustom, bool isMain = false)
        {
            Name = name;
            IsCustom = isCustom;
            IsMain = isMain;
            RepositoryMethods = new List<RepositoryMethod>();
            AddRepositoryMethod(new RepositoryMethod(UseCase.UseCaseTypes.View, DefaultViewNames.All, false).AddDefaultOutputViewParameter());
        }

        public void AddRepositoryMethod(RepositoryMethod view)
        {
            if (RepositoryMethods.FirstOrDefault(k => k.Name == view.Name) != null)
            {
                throw new Exception("View name repeated in repository");
            }
            RepositoryMethods.Add(view);
        }

        public List<RepositoryMethod> GetAllRepositoriesMethods()
        {
            return RepositoryMethods
                .ToList();
        }

        public RepositoryMethod GetDefaultCreateRepositoryMethod()
        {
            return RepositoryMethods.First(k => k.Type == UseCase.UseCaseTypes.Create);
        }

        public RepositoryMethod GetDefaultUpdateRepositoryMethod()
        {
            return RepositoryMethods.First(k => k.Type == UseCase.UseCaseTypes.Update);
        }

        public RepositoryMethod GetDefaultDeleteByPkRepositoryMethod()
        {
            return RepositoryMethods.First(k => k.Type == UseCase.UseCaseTypes.DeleteByPk);
        }

        public RepositoryMethod GetDefaultRetrieveByPkRepositoryMethod()
        {
            return RepositoryMethods.First(k => k.Type == UseCase.UseCaseTypes.RetrieveByPk);
        }

        public RepositoryMethod GetDefaultRetrieveByUnRepositoryMethod()
        {
            return RepositoryMethods.First(k => k.Type == UseCase.UseCaseTypes.RetrieveByUn);
        }

        public RepositoryMethod GetViewRepositoyMethod(string methodName)
        {
            return RepositoryMethods.First(k => k.Type == UseCase.UseCaseTypes.View
                                            && k.Name == methodName);
        }
    }
}
