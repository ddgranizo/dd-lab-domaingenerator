﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace UIClient.Events
{
    public class OnGenericFormConfirmedValuesEventArgs : RoutedEventArgs
    {
        public Dictionary<string, object> Data { get; set; }
    }
}
