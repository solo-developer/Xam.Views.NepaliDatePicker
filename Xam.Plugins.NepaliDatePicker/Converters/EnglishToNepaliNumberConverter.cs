using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xam.Plugins.NepaliDatePicker.Enums;
using Xam.Plugins.NepaliDatePicker.Library;
using Xamarin.Forms;

namespace Xam.Plugins.NepaliDatePicker.Converters
{
    public class EnglishToNepaliNumberConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values == null)
                return string.Empty;
            if (values[0] == null || values[1] == null)
                return string.Empty;
            if ((Language)values[1] == Language.English)
                return values[0];
            return EnglishToNepaliNumber.convertToNepaliNumber((int)values[0]);
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
