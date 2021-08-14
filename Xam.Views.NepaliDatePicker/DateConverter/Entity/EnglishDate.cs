using System;

namespace DateConverter.Core
{
    public class EnglishDate
    {
        private DateTime? _formattedDate;

        private int _engDaysInMonth;

        /// <summary>
        /// DaysInMonth of english date
        /// </summary>
        public int engDaysInMonth
        {
            get { return _engDaysInMonth; }
            set { _engDaysInMonth = value; }
        }

        private int _engYear;

        /// <summary>
        /// Numeric Year of english date
        /// </summary>
        public int engYear
        {
            get { return _engYear; }
            set { _engYear = value; }
        }

        private int _engMonth;

        /// <summary>
        /// Numeric Month of english date
        /// </summary>
        public int engMonth
        {
            get { return _engMonth; }
            set
            {
                if (value > 12)
                    throw new Exception("Month cannot be greater than 12.");
                _engMonth = value;
            }
        }

        private int _engDay;

        /// <summary>
        /// Numeric Day of english date
        /// </summary>
        public int engDay
        {
            get { return _engDay; }
            set
            {
                if (value > 31)
                    throw new Exception("English Date cannot be greater than 31.");
                _engDay = value;
            }
        }


        private string _dayName;
        /// <summary>
        /// day name of a week
        /// </summary>
        public string dayName
        {
            get { return _dayName; }
            set
            {
                if (string.IsNullOrEmpty(value))
                    throw new Exception("Day name cannot be empty.");
                _dayName = value;
            }
        }

        private int _dayNumber;
        /// <summary>
        /// day as integer value
        /// </summary>
        public int dayNumber
        {
            get { return _dayNumber; }
            set
            {
                if (value < 1 || value > 7)
                    throw new Exception("Day number must range between 1 and 7.");
                _dayNumber = value;
            }
        }

        //string formats
        public enum DateFormats
        {
            mDy,
            dMy,
            yMd
        }

        public void setFormattedDate(int year, int month, int day, string date_format= "mDy")
        {

            _formattedDate = new DateTime(year, month, day);
        }


        public DateTime getFormattedDate()
        {
            if (_formattedDate != null)
                return Convert.ToDateTime(_formattedDate);
            if (engMonth == 0 || engYear == 0 || engDay == 0)
                throw new Exception("Date value is not set.Please set the date value first.");
            return new DateTime(engYear, engMonth, engDay);
        }
    }
}
