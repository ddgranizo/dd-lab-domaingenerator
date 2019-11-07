using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using static DD.DomainGenerator.DeployActions.Base.DeployActionUnit;

namespace UIClient.Events
{
    public class DeployActionUnitStateChangedEventArgs: RoutedEventArgs
    {
        public DeployState Data { get; set; }
    }
}
