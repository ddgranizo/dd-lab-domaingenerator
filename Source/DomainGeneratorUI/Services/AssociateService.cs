using DD.Lab.Wpf.Drm;
using DD.Lab.Wpf.Drm.Models.Data;
using DD.Lab.Wpf.Drm.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DomainGeneratorUI.Services
{
    public class AssociateService : IAssociate
    {

        public AssociateService(StoredGenericValuesService genericValuesService)
        {
            GenericValuesService = genericValuesService ?? throw new ArgumentNullException(nameof(genericValuesService));
        }

        public StoredGenericValuesService GenericValuesService { get; }

        public Guid Execute(string firstEntity, Guid firstId, string intersectionEntity, string secondEntity, Guid secondId)
        {
            var newId = Guid.NewGuid();

            var firstAttributeInIntersection = Utilities.GetFirstIntersectionAttribute(firstEntity, secondEntity);
            var secondAttributeInIntersection = Utilities.GetSecondIntersectionAttribute(firstEntity, secondEntity);

            GenericValuesService.SetContextFile(intersectionEntity);
            var intersectionSet = GenericValuesService.GetStoredData();

            foreach (var item in intersectionSet.Values)
            {
                item.Values[firstAttributeInIntersection] = Guid.Parse((string)item.Values[firstAttributeInIntersection]);
                item.Values[secondAttributeInIntersection] = Guid.Parse((string)item.Values[secondAttributeInIntersection]);
            }

            var record = intersectionSet.Values.FirstOrDefault(k =>
               k.Values.ContainsKey(firstAttributeInIntersection)
               && k.Values.ContainsKey(secondAttributeInIntersection)
               && (Guid)k.Values[firstAttributeInIntersection] == firstId
               && (Guid)k.Values[secondAttributeInIntersection] == secondId);
            if (record != null)
            {
                throw new Exception("Records already associated");
            }

            GenericValuesService.SetContextFile(firstEntity);
            var firstEntitySet = GenericValuesService.GetStoredData();
            GenericValuesService.SetContextFile(secondEntity);
            var secondEntitySet = GenericValuesService.GetStoredData();

            var firstRecord = firstEntitySet.Values.First(k => k.Id == firstId);
            var secondRecord = secondEntitySet.Values.First(k => k.Id == secondId);


            GenericValuesService.SetContextFile(intersectionEntity);
            intersectionSet.Values.Add(new DataRecord(newId, new Dictionary<string, object>()
            {
                { firstAttributeInIntersection, firstId },
                { secondAttributeInIntersection, secondId}
            }));

            GenericValuesService.SaveStoredData(intersectionSet);
            return newId;
        }
    }
}
