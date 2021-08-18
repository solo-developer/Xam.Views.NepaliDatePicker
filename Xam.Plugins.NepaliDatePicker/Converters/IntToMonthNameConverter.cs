using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xam.Plugins.NepaliDatePicker.Enums;
using Xamarin.Forms;

namespace Xam.Plugins.NepaliDatePicker.Converters
{
    public class IntToMonthNameConverter : IMultiValueConverter
    {
        private static readonly string[] _monthNamesInEnglish = new string[] { "Baisakh", "Jestha", "Asadh", "Shrawan", "Bhadra", "Ashwin", "Kartik", "Mangsir", "Poush", "Magh", "Falgun", "Chaitra" };
        private static readonly string[] _monthNamesInNepali = new string[] { "वैशाख", "ज्येष्ठ", "आषाढ़", "श्रावण", "भदौ", "आश्विन", "कार्तिक", "मंसिर", "पौष", "माघ", "फागुन", "चैत्र" };
      
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values == null)
                return string.Empty;
            if (values[0] == null || values[1] == null)
                return string.Empty;

            if ((Language)values[1] == Language.English)
            {
                return _monthNamesInEnglish[(int)values[0] - 1];
            }
            return _monthNamesInNepali[(int)values[0] - 1];
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
