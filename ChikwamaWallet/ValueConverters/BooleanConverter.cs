using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace ChikwamaWallet.ValueConverters
{
    class BooleanConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (!(value is Boolean))
                return value;
            return !((Boolean)value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (!(value is Boolean))
                return value;
            return !((Boolean)value);
        }
    }
}
