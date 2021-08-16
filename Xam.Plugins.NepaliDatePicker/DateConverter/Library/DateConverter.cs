using System;
using System.Collections.Generic;
using System.Text;

namespace DateConverter.Core.Library
{
    public sealed class DateConverter : iDateConverter
    {
        private readonly iConversionStartDateData conversionStartDateData;
        private readonly iDateFunctions dateFunctions;
        private readonly iNepaliDateData nepaliDateData;
        public DateConverter(iConversionStartDateData _conversionStartDateData, iDateFunctions _dateFunctions, iNepaliDateData _nepaliDateData)
        {
            conversionStartDateData = _conversionStartDateData;
            dateFunctions = _dateFunctions;
            nepaliDateData = _nepaliDateData;
        }
        public EnglishDate ToAD(string gDate)
        {

            int def_eyy = 0;
            int def_emm = 0;
            int def_edd = 0;
            int def_nyy = 0;
            int total_eDays = 0;
            int total_nDays = 0;
            int a = 0;
            int m = 0;
            int y = 0;
            int i = 0;
            int j = 0;
            int k = 0;

            //split nepali dates to get its year,month and day

            System.String[] userDateParts = gDate.Split(new[] { "/" }, StringSplitOptions.None);
            int mm = int.Parse(userDateParts[0]);
            int dd = int.Parse(userDateParts[1]);
            int yy = int.Parse(userDateParts[2]);

            //check if day and month in user input value is valid or not
            if (mm > 12)
                throw new Exception("Month value cannot be greater than 12.");
            if (nepaliDateData.getLastDayOfMonthNep(yy, mm) < dd)
                throw new Exception(dateFunctions.GetNepaliMonth(mm) + " of the year " + yy + " doesnot have " + dd + " days.");

            //get starting nepali and english date for conversion 
            //Initialize english date
            Tuple<int, int[]> initializationDates = conversionStartDateData.getClosestEnglishDateAndNepaliDateToAD(yy);

            int nepali_init_date = initializationDates.Item1;
            int[] english_init_date = initializationDates.Item2;
            //Initialize english date
            //def_eyy = 1943;
            //def_emm = 4;
            //def_edd = 13;

            def_eyy = english_init_date[0];
            def_emm = english_init_date[1];
            def_edd = english_init_date[2];

            //Equivalent nepali date
            // def_nyy = 2000;
            def_nyy = nepali_init_date;

            //Initializations
            total_eDays = 0;
            total_nDays = 0;
            a = 0;
            m = 0;
            i = 0;

            //  k = 0;
            k = nepali_init_date;


            int[] month = new int[] { 0, 31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };

            int[] lmonth = new int[] { 0, 31, 29, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };

            while ((i < (yy - def_nyy)))
            {
                j = 1;
                while ((j <= 12))
                {
                    total_nDays += nepaliDateData.getLastDayOfMonthNep(k, j);
                    j += 1;
                }

                i += 1;
                k += 1;
            }

            j = 1;
            while ((j < mm))
            {
                total_nDays += nepaliDateData.getLastDayOfMonthNep(k, j);
                j += 1;
            }
            total_nDays += dd;

            total_eDays = def_edd;
            m = def_emm;
            y = def_eyy;


            while ((!(total_nDays == 0)))
            {
                if ((dateFunctions.IsLeapYear(y)))
                {
                    a = lmonth[m];
                }
                else
                {
                    a = month[m];
                }

                total_eDays += 1;

                if ((total_eDays > a))
                {
                    m += 1;
                    total_eDays = 1;

                    if ((m > 12))
                    {
                        y += 1;
                        m = 1;
                    }
                }
                total_nDays -= 1;

            }
            EnglishDate eng_date = new EnglishDate();
            eng_date.engDay = total_eDays;
            eng_date.engDaysInMonth = a;
            eng_date.engMonth = m;
            eng_date.engYear = y;
            eng_date.dayName = new DateTime(y, m, total_eDays).DayOfWeek.ToString();
            //1 is added since we consider first day to be 1 while it is actually 0
            eng_date.dayNumber = (int)new DateTime(y, m, total_eDays).DayOfWeek + 1;
            eng_date.setFormattedDate(y, m, total_eDays);
            return eng_date;
        }

        public NepaliDate ToBS(DateTime gDate, NepaliDate.DateFormats date_format = NepaliDate.DateFormats.mDy)
        {
            gDate = gDate.Date;
            //Breaking given english date
            int yy = 0;
            int mm = 0;
            int dd = 0;
            yy = gDate.Year;
            mm = gDate.Month;
            dd = gDate.Day;

            //English month data
            int[] month = new int[] { 31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };

            int[] lmonth = new int[] { 31, 29, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };

            int def_eyy = 0;
            int def_nyy = 0;
            int def_nmm = 0;
            int def_ndd = 0;
            int total_eDays = 0;
            int total_nDays = 0;
            int a = 0;
            int m = 0;
            int y = 0;
            int i = 0;
            int j = 0;

            //Initialize english date
            Tuple<int[], int[], int> initializationDates = conversionStartDateData.getClosestEnglishDateAndNepaliDate(gDate);

            int[] english_init_date = initializationDates.Item1;
            int[] nepali_init_date = initializationDates.Item2;
            // def_eyy = 1944;
            def_eyy = english_init_date[0];

            //Equivalent nepali date
            def_nyy = nepali_init_date[0];
            def_nmm = nepali_init_date[1];
            def_ndd = nepali_init_date[2];

            //Initializations
            total_eDays = 0;
            total_nDays = 0;
            a = 0;
            m = 0;
            i = 0;
            j = 0;

            //count total number of english days using standard date functions

            total_eDays = Convert.ToInt32((gDate - new DateTime(def_eyy, 01, 01)).TotalDays) + 1;

            //below i is the starting nepali year, used in looping to loop through years above the specified year
            i = def_nyy;
            j = def_nmm;
            total_nDays = def_ndd;
            m = def_nmm;
            y = def_nyy;

            //Count nepali date from array
            while ((!(total_eDays == 0)))
            {
                a = nepaliDateData.getLastDayOfMonthNep(i, j);
                total_nDays += 1;

                if ((total_nDays > a))
                {
                    m += 1;
                    total_nDays = 1;
                    j += 1;
                }
                if ((m > 12))
                {
                    y += 1;
                    m = 1;
                }

                if ((j > 12))
                {
                    j = 1;
                    i += 1;
                }
                total_eDays -= 1;
            }

            NepaliDate nep_date = new NepaliDate();
            nep_date.npDay = total_nDays;
            nep_date.npDaysInMonth = a;
            nep_date.npMonth = m;
            nep_date.npYear = y;
            //1 is added since we are using Sunday as 1 although it is 0
            nep_date.dayNumber = (int)gDate.DayOfWeek + 1;
            nep_date.dayName = gDate.DayOfWeek.ToString();
            nep_date.setFormattedDate(y, m, total_nDays, date_format);
            return nep_date;
        }
    }
}
