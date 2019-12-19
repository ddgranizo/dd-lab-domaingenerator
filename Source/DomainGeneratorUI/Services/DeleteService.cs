using DD.Lab.Wpf.Drm.Models;
using DD.Lab.Wpf.Drm.Services;
using DD.Lab.Wpf.Models.Inputs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DomainGeneratorUI.Services
{
    public class DeleteService : IDelete
    {

        public DeleteService(StoredGenericValuesService genericValuesService, MetadataModel metadataModel, bool deleteInCascade = true)
        {
            GenericValuesService = genericValuesService ?? throw new ArgumentNullException(nameof(genericValuesService));
            MetadataModel = metadataModel ?? throw new ArgumentNullException(nameof(metadataModel));
            DeleteInCascade = deleteInCascade;
        }

        public StoredGenericValuesService GenericValuesService { get; }
        public MetadataModel MetadataModel { get; }
        public bool DeleteInCascade { get; }

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

            if (DeleteInCascade)
            {
                foreach (var item in MetadataModel.Relationships.Where(k => k.MainEntity == entity && !k.IsManyToMany))
                {
                    GenericValuesService.SetContextFile(item.RelatedEntity);
                    var currentRelatedValues = GenericValuesService.GetStoredData();
                    var relatedRecords = currentRelatedValues
                            .Values
                            .Where(k => k.Values.ContainsKey(item.RelatedAttribute) && ((EntityReferenceValue)k.Values[item.RelatedAttribute]).Id == id);
                    foreach (var relatedRecord in relatedRecords)
                    {
                        Execute(item.RelatedEntity, relatedRecord.Id);
                    }
                }
            }

        }
    }
}
