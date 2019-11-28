using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using UIClient.Models.Base;

namespace UIClient.Models
{
    public class RepositoryModel : BaseModel
    {
        public string Name { get { return GetValue<string>(); } set { SetValue(value); } }
        public List<RepositoryMethodModel> RepositoryMethods { get { return GetValue<List<RepositoryMethodModel>>(); } set { SetValue(value); UpdateListToCollection(value, RepositoryMethodsCollection); } }
        public ObservableCollection<RepositoryMethodModel> RepositoryMethodsCollection { get; set; } = new ObservableCollection<RepositoryMethodModel>();

        public bool  IsCustom { get { return GetValue<bool>(); } set { SetValue(value); } }

    }
}
