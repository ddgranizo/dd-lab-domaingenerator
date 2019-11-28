using DD.DomainGenerator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UIClient.Models.Base;
using UIClient.Utilities;
using static DD.DomainGenerator.Definitions;

namespace UIClient.Models
{
    public class UseCaseListItemModel : BaseModel
    {
        public UseCaseModel UseCase { get { return GetValue<UseCaseModel>(); } set { SetValue(value); } }
        public DomainModel Domain { get { return GetValue<DomainModel>(); } set { SetValue(value); } }
        public SchemaModel Schema { get { return GetValue<SchemaModel>(); } set { SetValue(value); } }



        public string Namespace
        {
            get
            {
                return $"{Domain.Name}.{Schema.Name}";
            }
        }

        public string DisplayName
        {
            get
            {
                return $"{UseCase.Name}";
            }
        }

        public string CompleteDisplayName
        {
            get
            {
                return $"{Namespace} {OutputTypeDisplayName} {DisplayName} ({InputTypesDisplayName})";
            }
        }

        public string InputTypesDisplayName
        {
            get
            {
                return string.Join(",", UseCase.InputParameters.Select(k => $"{StringFormats.GetTypeDisplayName(k.Type, Schema.Name)} {k.Name}"));
            }
        }

        public string OutputTypeDisplayName
        {
            get
            {
                return StringFormats.GetDataParametersDisplayName(UseCase.OutputParameters, Schema.Name);
            }
        }
    }
}
