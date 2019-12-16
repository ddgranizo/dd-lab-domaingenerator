using DD.Lab.GenericUI.Core.Models.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace DD.Lab.GenericUI.Core.Services
{
    public interface IRetrieveAllAssociated
    {
        DataSetModel Execute(string firstEntity, Guid mainId, string intersectionEntity, string secondEntity);
    }
}
