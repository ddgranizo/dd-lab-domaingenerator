using DD.Lab.GenericUI.Core;
using DD.Lab.GenericUI.Core.Models.Data;
using DD.Lab.GenericUI.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UIClientV2.Services
{
    public class RetrieveAllAssociatedService : IRetrieveAllAssociated
    {

        public RetrieveAllAssociatedService(StoredGenericValuesService genericValuesService)
        {
            GenericValuesService = genericValuesService ?? throw new ArgumentNullException(nameof(genericValuesService));
        }

        public StoredGenericValuesService GenericValuesService { get; }


        public DataSetModel Execute(string firstEntity, Guid mainId, string intersectionEntity, string secondEntity)
        {
            var firstAttributeInIntersection = Utilities.GetFirstIntersectionAttribute(firstEntity, secondEntity);
            var secondAttributeInIntersection = Utilities.GetSecondIntersectionAttribute(firstEntity, secondEntity);
            GenericValuesService.SetContextFile(intersectionEntity);
            var currentIntersectionValues = GenericValuesService.GetStoredData()
                    ?? new DataSetModel();
            foreach (var item in currentIntersectionValues.Values)
            {
                item.Values["Id"] = item.Id;
            }
            foreach (var item in currentIntersectionValues.Values)
            {
                item.Values[firstAttributeInIntersection] = Guid.Parse((string) item.Values[firstAttributeInIntersection]);
                item.Values[secondAttributeInIntersection] = Guid.Parse((string)item.Values[secondAttributeInIntersection]);
            }
            var filteredIntersectionValues = currentIntersectionValues.Values
                .Where(k => (Guid)k.Values[firstAttributeInIntersection] == mainId)
                .Select(k => (Guid)k.Values[secondAttributeInIntersection])
                .ToList();

            GenericValuesService.SetContextFile(secondEntity);

            var currentRelatedValues = GenericValuesService.GetStoredData()
                    ?? new DataSetModel();

            var relatedRecordValues = currentRelatedValues.Values
                    .Where(k => filteredIntersectionValues.IndexOf(k.Id) > -1)
                    .ToList();

            var returnInstance = new DataSetModel();
            returnInstance.Values = relatedRecordValues;
            return returnInstance;
        }
    }
}
