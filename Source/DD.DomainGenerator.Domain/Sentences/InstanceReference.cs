using System;
using System.Collections.Generic;
using System.Text;

namespace DD.DomainGenerator.Sentences
{
    public class InstanceReference
    {
        public enum InstanceType
        {
            Project = 1,

            Domain = 10,

            Schema = 20,

            Property = 21,
            Model = 30,
            Repository = 40,
            RepositoryMethod = 41,

            UseCaseMethod = 50,

            Setting = 1000,
            AzurePipeLineSetting = 1001,
            GithubSetting = 1002,
            Environment = 1003,
        }


        public InstanceType Type { get; set; }
        public int MyProperty { get; set; }
    }
}
