using System;
using System.Collections.Generic;
using System.Text;
using UIClient.Models.Sentences.Base;

namespace UIClient.Models.Sentences
{
    public class ExecuteRepositoryMethodSentenceModel : ExecutionSentenceBaseModel
    {
        public DomainModel Domain { get { return GetValue<DomainModel>(); } set { SetValue(value); } }
        public SchemaModel Schema { get { return GetValue<SchemaModel>(); } set { SetValue(value); } }
        public RepositoryModel Repository { get { return GetValue<RepositoryModel>(); } set { SetValue(value); } }
        public RepositoryMethodModel Method { get { return GetValue<RepositoryMethodModel>(); } set { SetValue(value); } }

    }
}
