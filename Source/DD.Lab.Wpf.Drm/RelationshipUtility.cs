﻿using System;
using System.Collections.Generic;
using System.Text;

namespace DD.Lab.Wpf.Drm
{
    public static class RelationshipUtility
    {
        public static string GetFirstIntersectionAttribute(string firstEntity, string secondEntity)
        {
            if (firstEntity == secondEntity)
            {
                return $"{firstEntity}First";
            }
            return $"{firstEntity}Id";
        }

        public static string GetSecondIntersectionAttribute(string firstEntity, string secondEntity)
        {
            if (firstEntity == secondEntity)
            {
                return $"{firstEntity}First";
            }
            return $"{secondEntity}Id";
        }

    }
}
