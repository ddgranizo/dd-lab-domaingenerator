using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows.Controls;
using System.Windows.Data;

namespace DD.Lab.Wpf.Converters
{
    public class IndexConverter : IValueConverter
    {
        public object Convert(object value, Type TargetType, object parameter, CultureInfo culture)
        {
            DataGridRow item = (DataGridRow)value;
            DataGrid dataGrid = ItemsControl.ItemsControlFromItemContainer(item) as DataGrid;
            int index = dataGrid.ItemContainerGenerator.IndexFromContainer(item);
            return ((int)(index) + 1).ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
