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
        public List<View> Views { get; set; }
        public bool IsCustom { get; set; }
        public bool IsMain { get; set; }

        public Repository(string name, bool isCustom, bool isMain = false)
        {
            Name = name;
            IsCustom = isCustom;
            IsMain = isMain;
            Views = new List<View>();
            AddView(new View(DefaultViewNames.All, false));
        }

        public void AddView(View view)
        {
            if (Views.FirstOrDefault(k => k.Name == view.Name) != null)
            {
                throw new Exception("View name repeated in repository");
            }
            Views.Add(view);
        }
    }
}
