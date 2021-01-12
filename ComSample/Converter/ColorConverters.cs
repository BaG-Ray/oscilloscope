using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace ComSample.Converter
{
    public class ColorConverters : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null && bool.TryParse(value.ToString(), out bool result))
            {
                if (result)
                {
                    return new SolidColorBrush((Color)System.Windows.Media.ColorConverter.ConvertFromString("#2E8B57"));
                }
            }
            return new SolidColorBrush((Color)System.Windows.Media.ColorConverter.ConvertFromString("#FF6347"));
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
