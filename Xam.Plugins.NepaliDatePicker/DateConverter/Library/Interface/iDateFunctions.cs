using System;
using System.Collections.Generic;
using System.Text;

namespace DateConverter.Core.Library
{
    public interface iDateFunctions
    {
       
        bool IsLeapYear(int english_year);
        string GetNepaliMonth(int month);
        string GetEnglishMonth(int month);
        string GetDayOfWeek(int day);
        double GetUnixTimestamp(DateTime gDate);
        DateTime FormatUnixTime(double timestamp);
    }
}
