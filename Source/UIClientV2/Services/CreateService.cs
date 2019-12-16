using DD.Lab.GenericUI.Core.Models.Data;
using DD.Lab.GenericUI.Core.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace UIClientV2.Services
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
            currentValues.Values.Add(new DataRowModel(newId, values));
            GenericValuesService.SaveStoredData(currentValues);
            return newId;
        }
    }
}
