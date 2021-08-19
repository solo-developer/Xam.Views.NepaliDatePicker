using System;
using System.Collections.Generic;

namespace Xam.Plugins.NepaliDatePicker.Library
{
    public class EnglishToNepaliNumber
    {
        private static string[] nepaliOnesNumbers = new string[] { "०", "१", "२", "३", "४", "५", "६", "७", "८", "९" };

        private static Dictionary<decimal, string> NumberDictionary = new Dictionary<decimal, string>();

        public static string ConvertToNepaliNumber(decimal number)
        {
            if (NumberDictionary.ContainsKey(number))
                return NumberDictionary[number];

            string nepaliNumber = string.Empty;

            string[] arrNumber = number.ToString().Split('.');

            long wholePart = long.Parse(arrNumber[0]);

            long[] numberChars = GetIntArray(wholePart);

            foreach (int englishNumber in numberChars)
            {
                nepaliNumber += GetEquivalentNepaliNumber(englishNumber);
            }

            nepaliNumber = AppendNumbersAfterDecimal(arrNumber, nepaliNumber);

            NumberDictionary.Add(number, nepaliNumber);
            return nepaliNumber;
        }

        private static string GetEquivalentNepaliNumber(int numberChar)
        {
            return nepaliOnesNumbers[numberChar];
        }

        private static string AppendNumbersAfterDecimal(string[] splitted_number_by_decimal, string nepaliNumber)
        {
            bool isNumberDecimal = splitted_number_by_decimal.Length == 2;
            if (!isNumberDecimal)
            {
                return nepaliNumber;
            }
            nepaliNumber += ".";

            int number_after_decimal = Convert.ToInt32(splitted_number_by_decimal[1]);

            long[] numberChars = GetIntArray(number_after_decimal);

            foreach (int englishNumber in numberChars)
            {
                nepaliNumber += GetEquivalentNepaliNumber(englishNumber);
            }
            return nepaliNumber;
        }

        private static long[] GetIntArray(long num)
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
