using System;
using System.Collections.Generic;
using System.Text;

namespace Xam.Plugins.NepaliDatePicker.Library
{
    public class EnglishToNepaliNumber
    {
        private static string[] nepaliOnesNumbers = new string[] { "०", "१", "२", "३", "४", "५", "६", "७", "८", "९" };

        public static string convertToNepaliNumber(decimal number)
        {
            string nepaliNumber = "";

            string[] arrNumber = number.ToString().Split('.');

            long wholePart = long.Parse(arrNumber[0]);

            long[] numberChars = getIntArray(wholePart);

            foreach (int englishNumber in numberChars)
            {
                nepaliNumber += getEquivalentNepaliNumber(englishNumber);
            }

            nepaliNumber = appendNumbersAfterDecimal(arrNumber, nepaliNumber);

            return nepaliNumber;
        }

        private static string getEquivalentNepaliNumber(int numberChar)
        {
            return nepaliOnesNumbers[numberChar];
        }

        private static string appendNumbersAfterDecimal(string[] splitted_number_by_decimal, string nepaliNumber)
        {
            bool isNumberDecimal = splitted_number_by_decimal.Length == 2;
            if (!isNumberDecimal)
            {
                return nepaliNumber;
            }
            nepaliNumber += ".";

            int number_after_decimal = Convert.ToInt32(splitted_number_by_decimal[1]);

            long[] numberChars = getIntArray(number_after_decimal);

            foreach (int englishNumber in numberChars)
            {
                nepaliNumber += getEquivalentNepaliNumber(englishNumber);
            }
            return nepaliNumber;
        }

        private static long[] getIntArray(long num)
        {
            List<long> listOfInts = new List<long>();
            while (num > 0)
            {
                listOfInts.Add(num % 10);
                num = num / 10;
            }
            listOfInts.Reverse();
            return listOfInts.ToArray();
        }
    }
}
