using DD.Lab.GenericUI.Core.Models.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace DD.Lab.GenericUI.Core.Services
{
    public interface IRetrieve
    {
        DataRowModel Execute(string entity, Guid id);
    }
}
