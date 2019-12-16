using System;
using System.Collections.Generic;
using System.Text;

namespace DD.Lab.Wpf.Models.Inputs
{
    public interface IEntityReferenceSuggestionHandler
    {
        EntityReferenceValue[] GetFirstPageValues();
        EntityReferenceValue GetValue(Guid id);
        EntityReferenceValue[] SearchValues(string searchText);
    }
}
