using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace Xam.Views.NepaliDatePicker.Converters
{
    public class IntToDayNameConverter : IValueConverter
    {
        private static readonly string[] DayNames = new string[] {"Sun","Mon","Tue","Wed","Thu","Fri","Sat" };
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return null;

            if (value.GetType() != typeof(int))
                return null;

            return DayNames[(int)value - 1];
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
