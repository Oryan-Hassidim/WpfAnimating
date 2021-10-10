using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace WpfAnimating.ViewHelpers
{
    [ValueConversion(typeof(double), typeof(double), ParameterType = typeof(string))]
    public class MathConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) => 0 - double.Parse(value.ToString());
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => throw new NotImplementedException();
    }
}
