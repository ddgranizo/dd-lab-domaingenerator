using DD.Lab.GenericUI.Core.Models.Data;
using DD.Lab.GenericUI.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UIClientV2.Services
{
    public class RetrieveAllService : IRetrieveAll
    {

        public RetrieveAllService(StoredGenericValuesService genericValuesService)
        {
            GenericValuesService = genericValuesService ?? throw new ArgumentNullException(nameof(genericValuesService));
        }

        public StoredGenericValuesService GenericValuesService { get; }



        public DataSetModel Execute(string entity)
        {
            GenericValuesService.SetContextFile(entity);
            var currentValues = GenericValuesService.GetStoredData()
                    ?? new DataSetModel();
            foreach (var item in currentValues.Values)
            {
                item.Values["Id"] = item.Id;
            }
            return currentValues;
        }
    }
}
