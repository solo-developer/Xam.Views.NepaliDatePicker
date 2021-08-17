using System;
using System.Globalization;
using Xam.Plugins.NepaliDatePicker.Enums;
using Xamarin.Forms;

namespace Xam.Plugins.NepaliDatePicker.Converters
{
    public class SelectedDateToNameConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values == null)
                return string.Empty;
            if(values.Length != 3)
                return string.Empty;
            if(values[0] == null || values[1]==null || values[2]==null)
                return string.Empty;

            var dateData = ((int year, int month, int day))values[0];
            int dayOfWeek = (int)values[1];

            Language displayLanguage = (Language)values[2];

            var humanisedDayName = new IntToDayNameConverter().Convert( new object[] { dayOfWeek ,displayLanguage}, null, null, null);
            var humanisedMonthName = new IntToMonthNameConverter().Convert(new object[] { dateData.month ,displayLanguage}, null, null, null);
            var dateInSelectedLanguage = new EnglishToNepaliNumberConverter().Convert(new object[] { dateData.day, displayLanguage }, null, null, null);
            return $"{humanisedDayName}, {humanisedMonthName} {dateInSelectedLanguage}";
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
