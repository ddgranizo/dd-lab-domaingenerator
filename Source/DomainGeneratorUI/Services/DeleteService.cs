using DD.Lab.Wpf.Drm.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DomainGeneratorUI.Services
{
    public class DeleteService : IDelete
    {

        public DeleteService(StoredGenericValuesService genericValuesService)
        {
            GenericValuesService = genericValuesService ?? throw new ArgumentNullException(nameof(genericValuesService));
        }

        public StoredGenericValuesService GenericValuesService { get; }

        public void Execute(string entity, Guid id)
        {
            GenericValuesService.SetContextFile(entity);
            var currentValues = GenericValuesService.GetStoredData();
            var currentRow = currentValues.Values.FirstOrDefault(k => k.Id == id);
            if (currentRow == null)
            {
                throw new IndexOutOfRangeException();
            }
            currentValues.Values.Remove(currentRow);
            GenericValuesService.SaveStoredData(currentValues);
        }
    }
}
