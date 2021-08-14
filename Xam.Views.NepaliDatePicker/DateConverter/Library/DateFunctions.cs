using System;
using System.Collections.Generic;
using System.Text;

namespace DateConverter.Core.Library
{
    public class DateFunctions : iDateFunctions
    {
        iNepaliDateData nepaliDateArray;
        
        public DateFunctions(iNepaliDateData _nepaliDateArray)
        {
            nepaliDateArray = _nepaliDateArray;
        }

        public DateTime FormatUnixTime(double timestamp)
        {
            DateTime startUnixTime = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return startUnixTime.AddSeconds(timestamp).ToUniversalTime();
        }

        public string GetDayOfWeek(int day)
        {
            switch ((day))
            {
                case 1:
                    return "Sunday";
                case 2:
                    return "Monday";
                case 3:
                    return "Tuesday";
                case 4:
                    return "Wednesday";
                case 5:
                    return "Thursday";
                case 6:
                    return "Friday";
                case 7:
                    return "Saturday";
                default:
                    return null;
            }
        }

        public string GetEnglishMonth(int month)
        {
            switch ((month))
            {
                case 1:
                    return "January";
                case 2:
                    return "February";
                case 3:
                    return "March";
                case 4:
                    return "April";
                case 5:
                    return "May";
                case 6:
                    return "June";
                case 7:
                    return "July";
                case 8:
                    return "August";
                case 9:
                    return "September";
                case 10:
                    return "October";
                case 11:
                    return "November";
                case 12:
                    return "December";
                default:
                    return null;
            }
        }

        public string GetNepaliMonth(int month)
        {
            switch ((month))
            {
                case 1:
                    return "Baisakh";
                case 2:
                    return "Jestha";
                case 3:
                    return "Ashad";
                case 4:
                    return "Shrawan";
                case 5:
                    return "Bhadra";
                case 6:
                    return "Ashwin";
                case 7:
                    return "Kartik";
                case 8:
                    return "Mangsir";
                case 9:
                    return "Poush";
                case 10:
                    return "Magh";
                case 11:
                    return "Falgun";
                case 12:
                    return "Chaitra";
                default:
                    return null;
            }
        }

        public double GetUnixTimestamp(DateTime gDate)
        {
            //Overloads
            //create Timespan by subtracting the value provided from the Unix Epoch
            TimeSpan span = (gDate - new DateTime(1970, 1, 1, 0, 0, 0, 0).ToUniversalTime());
            //return the total seconds (which is a UNIX timestamp)
            return span.TotalSeconds;
        }

        public bool IsLeapYear(int english_year)
        {
            //Calculates whether a english year is leap year or not
            if ((english_year % 100 == 0))
            {
                if ((english_year % 400 == 0))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                if ((english_year % 4 == 0))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
    }
}
