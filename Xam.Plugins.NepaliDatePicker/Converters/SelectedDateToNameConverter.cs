using System;
using System.Globalization;
using Xamarin.Forms;

namespace Xam.Plugins.NepaliDatePicker.Converters
{
    public class SelectedDateToNameConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values == null)
                return string.Empty;
            if(values.Length != 2)
                return string.Empty;
            if(values[0] == null || values[1]==null)
                return string.Empty;

            var dateData = ((int year, int month, int day))values[0];
            int dayOfWeek = (int)values[1];

            var humanisedDayName = new IntToDayNameConverter().Convert(dayOfWeek, null, null, null);
            var humanisedMonthName = new IntToMonthNameConverter().Convert(dateData.month, null, null, null);
            return $"{humanisedDayName}, {humanisedMonthName} {dateData.day}";
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
