using DD.Lab.Wpf.Drm.Models.Data;
using DD.Lab.Wpf.Drm.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace DomainGeneratorUI.Services
{
    public class CreateService : ICreate
    {

        public CreateService(StoredGenericValuesService genericValuesService)
        {
            GenericValuesService = genericValuesService ?? throw new ArgumentNullException(nameof(genericValuesService));
        }

        public StoredGenericValuesService GenericValuesService { get; }

        public Guid Execute(string entity, Dictionary<string, object> values)
        {
            var newId = Guid.NewGuid();
            if (!values.ContainsKey("Id"))
            {
                values.Add("Id", newId);
            }
            else
            {
                values["Id"] = newId;
            }

            GenericValuesService.SetContextFile(entity);
            var currentValues = GenericValuesService.GetStoredData();
            currentValues.Values.Add(new DataRecord(newId, values));
            GenericValuesService.SaveStoredData(currentValues);
            return newId;
        }
    }
}
