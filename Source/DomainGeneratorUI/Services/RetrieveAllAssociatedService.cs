﻿using DD.Lab.Wpf.Drm;
using DD.Lab.Wpf.Drm.Models.Data;
using DD.Lab.Wpf.Drm.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DomainGeneratorUI.Services
{
    public class RetrieveAllAssociatedService : IRetrieveAllAssociated
    {

        public RetrieveAllAssociatedService(StoredGenericValuesService genericValuesService)
        {
            GenericValuesService = genericValuesService ?? throw new ArgumentNullException(nameof(genericValuesService));
        }

        public StoredGenericValuesService GenericValuesService { get; }


        public DataSet Execute(string firstEntity, Guid mainId, string intersectionEntity, string secondEntity)
        {
            var firstAttributeInIntersection = Utilities.GetFirstIntersectionAttribute(firstEntity, secondEntity);
            var secondAttributeInIntersection = Utilities.GetSecondIntersectionAttribute(firstEntity, secondEntity);
            GenericValuesService.SetContextFile(intersectionEntity);
            var currentIntersectionValues = GenericValuesService.GetStoredData()
                    ?? new DataSet();
            foreach (var item in currentIntersectionValues.Values)
            {
                item.Values["Id"] = item.Id;
            }
            foreach (var item in currentIntersectionValues.Values)
            {
                item.Values[firstAttributeInIntersection] = Guid.Parse((string)item.Values[firstAttributeInIntersection]);
                item.Values[secondAttributeInIntersection] = Guid.Parse((string)item.Values[secondAttributeInIntersection]);
            }
            var filteredIntersectionValues = currentIntersectionValues.Values
                .Where(k => (Guid)k.Values[firstAttributeInIntersection] == mainId)
                .Select(k => (Guid)k.Values[secondAttributeInIntersection])
                .ToList();

            GenericValuesService.SetContextFile(secondEntity);

            var currentRelatedValues = GenericValuesService.GetStoredData()
                    ?? new DataSet();

            var relatedRecordValues = currentRelatedValues.Values
                    .Where(k => filteredIntersectionValues.IndexOf(k.Id) > -1)
                    .ToList();

            var returnInstance = new DataSet();
            returnInstance.Values = relatedRecordValues;
            return returnInstance;
        }
    }
}
