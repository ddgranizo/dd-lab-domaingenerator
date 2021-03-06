﻿using DD.Lab.Wpf.Drm.Models.Data;
using DD.Lab.Wpf.Drm.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DomainGeneratorUI.Services
{
    public class RetrieveAllService : IRetrieveAll
    {

        public RetrieveAllService(StoredGenericValuesService genericValuesService)
        {
            GenericValuesService = genericValuesService ?? throw new ArgumentNullException(nameof(genericValuesService));
        }

        public StoredGenericValuesService GenericValuesService { get; }

        public DataSet Execute(string entity)
        {
            GenericValuesService.SetContextFile(entity);
            var currentValues = GenericValuesService.GetStoredData()
                    ?? new DataSet();
            foreach (var item in currentValues.Values)
            {
                item.Values["Id"] = item.Id;
            }
            currentValues.EntityLogicalName = entity;
            return currentValues;
        }
    }
}
