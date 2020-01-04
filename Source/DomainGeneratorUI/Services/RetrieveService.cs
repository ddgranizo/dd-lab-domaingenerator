using DD.Lab.Wpf.Drm.Models.Data;
using DD.Lab.Wpf.Drm.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DomainGeneratorUI.Services
{
    public class RetrieveService : IRetrieve
    {

        public RetrieveService(StoredGenericValuesService genericValuesService)
        {
            GenericValuesService = genericValuesService ?? throw new ArgumentNullException(nameof(genericValuesService));
        }

        public StoredGenericValuesService GenericValuesService { get; }

        public DataRecord Execute(string entity, Guid id)
        {
            GenericValuesService.SetContextFile(entity);
            var currentValues = GenericValuesService.GetStoredData();
            var currentRow = currentValues.Values.FirstOrDefault(k => k.Id == id);
            currentRow.Values["Id"] = currentRow.Id;
            if (currentRow == null)
            {
                throw new IndexOutOfRangeException();
            }
            return currentRow;
        }
    }
}
