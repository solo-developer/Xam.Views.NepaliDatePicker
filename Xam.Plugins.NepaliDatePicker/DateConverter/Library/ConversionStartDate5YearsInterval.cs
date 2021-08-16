using System;
using System.Collections.Generic;
using System.Text;

namespace DateConverter.Core.Library
{
    public class ConversionStartDate5YearsInterval : iConversionStartDateData
    {
        public Tuple<int[], int[], int> getClosestEnglishDateAndNepaliDate(DateTime english_date)
        {
            try
            {
                if (english_date.Year >= 2045)
                    throw new Exception("English date must be between 1944 and 2045.");

                else if (english_date.Year >= 2044)
                    return Tuple.Create(new int[] { 2044 }, new int[] { 2100, 09, 16 }, 1);

                else if (english_date.Year >= 2039)
                    return Tuple.Create(new int[] { 2039 }, new int[] { 2095, 09, 16 }, 1);

                else if (english_date.Year >= 2034)
                    return Tuple.Create(new int[] { 2034 }, new int[] { 2090, 09, 16 }, 1);

                else if (english_date.Year >= 2029)
                    return Tuple.Create(new int[] { 2029 }, new int[] { 2085, 09, 16 }, 1);

                else if (english_date.Year >= 2024)
                    return Tuple.Create(new int[] { 2024 }, new int[] { 2080, 09, 15 }, 1);

                else if (english_date.Year >= 2019)
                    return Tuple.Create(new int[] { 2019 }, new int[] { 2075, 09, 16 }, 2);

                else if (english_date.Year >= 2014)
                    return Tuple.Create(new int[] { 2014 }, new int[] { 2070, 09, 16 }, 3);

                else if (english_date.Year >= 2009)
                    return Tuple.Create(new int[] { 2009 }, new int[] { 2065, 09, 16 }, 4);

                else if (english_date.Year >= 2004)
                    return Tuple.Create(new int[] { 2004 }, new int[] { 2060, 09, 16 }, 4);

                else if (english_date.Year >= 1999)
                    return Tuple.Create(new int[] { 1999 }, new int[] { 2055, 09, 16 }, 5);

                else if (english_date.Year >= 1994)
                    return Tuple.Create(new int[] { 1994 }, new int[] { 2050, 09, 16 }, 6);

                else if (english_date.Year >= 1989)
                    return Tuple.Create(new int[] { 1989 }, new int[] { 2045, 09, 17 }, 0);

                else if (english_date.Year >= 1984)
                    return Tuple.Create(new int[] { 1984 }, new int[] { 2040, 09, 16 }, 0);

                else if (english_date.Year >= 1979)
                    return Tuple.Create(new int[] { 1979 }, new int[] { 2035, 09, 16 }, 1);

                else if (english_date.Year >= 1974)
                    return Tuple.Create(new int[] { 1974 }, new int[] { 2030, 09, 16 }, 2);


                else if (english_date.Year >= 1969)
                    return Tuple.Create(new int[] { 1969 }, new int[] { 2025, 09, 17 }, 3);

                else if (english_date.Year >= 1964)
                    return Tuple.Create(new int[] { 1964 }, new int[] { 2020, 09, 16 }, 3);

                else if (english_date.Year >= 1959)
                    return Tuple.Create(new int[] { 1959 }, new int[] { 2015, 09, 16 }, 4);

                else if (english_date.Year >= 1954)
                    return Tuple.Create(new int[] { 1954 }, new int[] { 2010, 09, 17 }, 5);

                else if (english_date.Year >= 1949)
                    return Tuple.Create(new int[] { 1949 }, new int[] { 2005, 09, 17 }, 6);

                else if (english_date.Year >= 1944)
                    return Tuple.Create(new int[] { 1944 }, new int[] { 2000, 09, 16 }, 6);
                else
                    throw new Exception("English date must be between 1944 and 2035.");
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// this function returns array of integer containing nepali year as first return value and integer containing english year,month and day respectively as second return value
        /// </summary>
        /// <param name="nep_year"></param>
        /// <returns></returns>
        public Tuple<int, int[]> getClosestEnglishDateAndNepaliDateToAD(int nep_year)
        {
            try
            {
                if (nep_year >= 2100)
                    throw new Exception("Nepali date must be between 2000 and 2100.");

                else if (nep_year >= 2095)
                    return Tuple.Create(2095, new int[] { 2038, 04, 13 });

                else if (nep_year >= 2090)
                    return Tuple.Create(2090, new int[] { 2033, 04, 13 });

                else if (nep_year >= 2085)
                    return Tuple.Create(2085, new int[] { 2028, 04, 12 });

                else if (nep_year >= 2080)
                    return Tuple.Create(2080, new int[] { 2023, 04, 13 });

                else if (nep_year >= 2075)
                    return Tuple.Create(2075, new int[] { 2018, 04, 13 });

                else if (nep_year >= 2070)
                    return Tuple.Create(2070, new int[] { 2013, 04, 13 });

                else if (nep_year >= 2065)
                    return Tuple.Create(2065, new int[] { 2008, 04, 12 });

                else if (nep_year >= 2060)
                    return Tuple.Create(2060, new int[] { 2003, 04, 13 });

                else if (nep_year >= 2055)
                    return Tuple.Create(2055, new int[] { 1998, 04, 13 });

                else if (nep_year >= 2050)
                    return Tuple.Create(2050, new int[] { 1993, 04, 12 });

                else if (nep_year >= 2045)
                    return Tuple.Create(2045, new int[] { 1988, 04, 12 });

                else if (nep_year >= 2040)
                    return Tuple.Create(2040, new int[] { 1983, 04, 13 });

                else if (nep_year >= 2035)
                    return Tuple.Create(2035, new int[] { 1978, 04, 13 });

                else if (nep_year >= 2030)
                    return Tuple.Create(2030, new int[] { 1973, 04, 12 });

                else if (nep_year >= 2025)
                    return Tuple.Create(2025, new int[] { 1968, 04, 12 });

                else if (nep_year >= 2020)
                    return Tuple.Create(2020, new int[] { 1963, 04, 13 });

                else if (nep_year >= 2015)
                    return Tuple.Create(2015, new int[] { 1958, 04, 12 });

                else if (nep_year >= 2010)
                    return Tuple.Create(2010, new int[] { 1953, 04, 12 });

                else if (nep_year >= 2005)
                    return Tuple.Create(2005, new int[] { 1948, 04, 12 });

                else if (nep_year >= 2000)
                    return Tuple.Create(2000, new int[] { 1943, 04, 13 });

                else
                    throw new Exception("Nepali Date must be between 2000 and 2100");
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
