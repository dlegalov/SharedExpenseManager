using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows;

namespace SharedExpenseManager.Converters
{
    public class VisibilityConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is bool)
            {
                return ((bool)value ? Visibility.Visible : Visibility.Collapsed);
            }
            if (value is bool?)
            {
                var nullable = (bool?)value;
                if (nullable == null)
                {
                    return Visibility.Hidden;
                }
                if (nullable == true)
                {
                    return Visibility.Visible;
                }
                return Visibility.Collapsed;
            }

            return Visibility.Visible; // Do nothing if some other parameter
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
