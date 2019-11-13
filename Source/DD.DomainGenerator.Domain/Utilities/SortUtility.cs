using DD.DomainGenerator.DeployActions.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace DD.DomainGenerator.Utilities
{
    public static class SortUtility
    {
        public static double CalculateDeployActionPosition(DeployActionUnit k)
        {
            return (int)k.StartFromPhase * 1e4 + k.StartFromLine * 1e2 + k.StartFromPosition;
        }
        
    }
}
