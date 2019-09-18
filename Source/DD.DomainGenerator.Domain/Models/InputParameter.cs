﻿using System;
using System.Collections.Generic;
using System.Text;

namespace DD.DomainGenerator.Models
{
    public class InputParameter
    {

        public bool HasValue { get; set; }
        public string RawStringValue { get; set; }
        public string ParameterName { get; }
        public bool IsShortCut { get; set; }

        public bool IsOnlyOne { get; set; }

        public InputParameter(string parameter, string rawStringValue, bool isShortCut = false)
        {
            if (string.IsNullOrEmpty(parameter))
            {
                throw new ArgumentException("message", nameof(parameter));
            }

            RawStringValue = rawStringValue;
            IsShortCut = isShortCut;
            ParameterName = parameter;
            HasValue = !string.IsNullOrEmpty(rawStringValue);
            IsOnlyOne = false;
        }

        public InputParameter(string rawStringValue)
        {
            RawStringValue = rawStringValue;
            IsOnlyOne = true;
            HasValue = !string.IsNullOrEmpty(rawStringValue);
        }

    }
}
