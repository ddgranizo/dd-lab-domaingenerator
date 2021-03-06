﻿using DD.Lab.GenericUI.Core;
using DD.Lab.GenericUI.Core.Models.Data;
using DD.Lab.GenericUI.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UIClientV2.Services
{
    public class DisassociateService : IDisassociate
    {

        public DisassociateService(StoredGenericValuesService genericValuesService)
        {
            GenericValuesService = genericValuesService ?? throw new ArgumentNullException(nameof(genericValuesService));
        }

        public StoredGenericValuesService GenericValuesService { get; }

        public void Execute(string firstEntity, Guid firstId, string intersectionEntity, string secondEntity, Guid secondId)
        {
            var firstAttributeInIntersection = Utilities.GetFirstIntersectionAttribute(firstEntity, secondEntity);
            var secondAttributeInIntersection = Utilities.GetSecondIntersectionAttribute(firstEntity, secondEntity);

            GenericValuesService.SetContextFile(intersectionEntity);
            var intersectionSet = GenericValuesService.GetStoredData();

            foreach (var item in intersectionSet.Values)
            {
                item.Values[firstAttributeInIntersection] = Guid.Parse((string)item.Values[firstAttributeInIntersection]);
                item.Values[secondAttributeInIntersection] = Guid.Parse((string)item.Values[secondAttributeInIntersection]);
            }

            var record = intersectionSet.Values.First(k =>
                k.Values.ContainsKey(firstAttributeInIntersection)
                && k.Values.ContainsKey(secondAttributeInIntersection)
                && (Guid)k.Values[firstAttributeInIntersection] == firstId
                && (Guid)k.Values[secondAttributeInIntersection] == secondId);
            intersectionSet.Values.Remove(record);
            GenericValuesService.SaveStoredData(intersectionSet);
        }
    }
}
