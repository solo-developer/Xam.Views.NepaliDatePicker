using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xam.Plugins.NepaliDatePicker.Enums;
using Xamarin.Forms;

namespace Xam.Plugins.NepaliDatePicker.Converters
{
    public class IntToDayNameConverter : IMultiValueConverter
    {
        private static readonly string[] _englishDayNames = new string[] { "Sun", "Mon", "Tue", "Wed", "Thu", "Fri", "Sat" };
        private static readonly string[] _nepaliDayNames = new string[] { "आइत", "सोम", "मंगल", "बुध", "बिहि", "शुक्र", "शनि" };


        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values == null)
                return string.Empty;
            if (values[0] == null || values[1] == null)
                return string.Empty;

            if ((Language)values[1] == Language.English)
            {
                return _englishDayNames[(int)values[0] - 1];
            }
            return _nepaliDayNames[(int)values[0] - 1];

        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
