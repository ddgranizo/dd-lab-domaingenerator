using DD.Lab.Wpf.Drm.Services;
using DD.Lab.Wpf.Models.Inputs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DomainGeneratorUI.Services
{
    public class EntityReferenceHandler : IEntityReferenceSuggestionHandler
    {
        public EntityReferenceHandler(string entityLogicalName, IRetrieveAll retrieveAllService, IRetrieve retrieveService)
        {
            if (string.IsNullOrEmpty(entityLogicalName))
            {
                throw new ArgumentException("message", nameof(entityLogicalName));
            }

            EntityLogicalName = entityLogicalName;
            RetrieveAllService = retrieveAllService ?? throw new ArgumentNullException(nameof(retrieveAllService));
            RetrieveService = retrieveService ?? throw new ArgumentNullException(nameof(retrieveService));
        }

        public string EntityLogicalName { get; }
        public IRetrieveAll RetrieveAllService { get; }
        public IRetrieve RetrieveService { get; }

        public EntityReferenceValue[] GetFirstPageValues()
        {
            return RetrieveAllService.Execute(EntityLogicalName).Values
                    .Select(k => new EntityReferenceValue() { Id = k.Id, DisplayName = GetRowDisplayName(k.Values) })
                    .OrderBy(k => k.DisplayName)
                    .ToArray();
        }

        public EntityReferenceValue GetValue(Guid id)
        {
            var record = RetrieveService.Execute(EntityLogicalName, id);
            return new EntityReferenceValue() { Id = record.Id, DisplayName = GetRowDisplayName(record.Values) };
        }

        public EntityReferenceValue[] SearchValues(string searchText)
        {
            return RetrieveAllService.Execute(EntityLogicalName).Values
                    .Where(k =>
                    {
                        if (string.IsNullOrEmpty(searchText))
                        {
                            return true;
                        }
                        var name = GetRowDisplayName(k.Values);
                        return string.IsNullOrEmpty(name) && name.ToLower().IndexOf(searchText.ToLower()) > -1;
                    })
                   .Select(k => new EntityReferenceValue() { Id = k.Id, DisplayName = GetRowDisplayName(k.Values) })
                   .OrderBy(k => k.DisplayName)
                   .ToArray();
        }

        public string GetRowDisplayName(Dictionary<string, object> values)
        {
            return values.ContainsKey("Name") ? (string)values["Name"] : "NO NAME";
        }
    }
}
