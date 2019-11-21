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
        public List<ViewModel> Views { get { return GetValue<List<ViewModel>>(); } set { SetValue(value); UpdateListToCollection(value, ViewsCollection); } }
        public ObservableCollection<ViewModel> ViewsCollection { get; set; } = new ObservableCollection<ViewModel>();

        public bool  IsCustom { get { return GetValue<bool>(); } set { SetValue(value); } }

    }
}
