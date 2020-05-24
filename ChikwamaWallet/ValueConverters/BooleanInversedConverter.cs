
using System;
using System.Globalization;
using Xamarin.Forms;

namespace ChikwamaWallet.ValueConverters
{
    public class BooleanInversedConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (value is bool v && v == false);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

