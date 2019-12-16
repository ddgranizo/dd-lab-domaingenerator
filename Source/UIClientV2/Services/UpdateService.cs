using DD.Lab.GenericUI.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UIClientV2.Services
{
    public class UpdateService : IUpdate
    {

        public UpdateService(StoredGenericValuesService genericValuesService)
        {
            GenericValuesService = genericValuesService ?? throw new ArgumentNullException(nameof(genericValuesService));
        }

        public StoredGenericValuesService GenericValuesService { get; }

   

        public void Execute(string entity, Guid id, Dictionary<string, object> values)
        {
            if (values is null)
            {
                throw new ArgumentNullException(nameof(values));
            }

            GenericValuesService.SetContextFile(entity);
            var currentValues = GenericValuesService.GetStoredData();
            var currentRow = currentValues.Values.FirstOrDefault(k => k.Id == id);
            if (currentRow == null)
            {
                throw new IndexOutOfRangeException();
            }
            currentRow.Values = values;
            GenericValuesService.SaveStoredData(currentValues);
        }
    }
}
