using DD.Lab.Wpf.Converters;
using System;
using System.Collections.Generic;
using System.Text;
using static UIClientV2.Viewmodels.MainControlViewModel;

namespace UIClientV2.Converters
{
    public class MainControlViewTypeToCollapseConverter: EnumSelectorToCollapsedConverter<ViewType> { }
}
