using DD.Lab.Wpf.Drm;
using DD.Lab.Wpf.Drm.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DomainGeneratorUI.Services
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
            var firstAttributeInIntersection = RelationshipUtility.GetFirstIntersectionAttribute(firstEntity, secondEntity);
            var secondAttributeInIntersection = RelationshipUtility.GetSecondIntersectionAttribute(firstEntity, secondEntity);

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
