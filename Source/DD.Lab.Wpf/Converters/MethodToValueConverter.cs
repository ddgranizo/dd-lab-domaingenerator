using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Data;

namespace DD.Lab.Wpf.Converters
{
    public class MethodToValueConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            var context = values[0];
            var methodName = parameter as string;
            if (context == null || methodName == null)
            {
                return context;
            }
            var methodInfo = context.GetType().GetMethods().FirstOrDefault(k => k.Name == methodName);
            if (methodInfo == null)
            {
                return context;
            }

            return methodInfo.Invoke(context, values.Skip(1).ToArray());
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
