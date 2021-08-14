using System;
using System.Collections.Generic;
using System.Text;

namespace DateConverter.Core
{
    public class NepaliDate
    {
        private string _formattedDate;

        private int _npDaysInMonth;

        /// <summary>
        /// DaysInMonth of Nepali date
        /// </summary>
        public int npDaysInMonth
        {
            get { return _npDaysInMonth; }
            set { _npDaysInMonth = value; }
        }

        private int _npYear;

        /// <summary>
        /// Numeric Year of Nepali date
        /// </summary>
        public int npYear
        {
            get { return _npYear; }
            set { _npYear = value; }
        }

        private int _npMonth;

        /// <summary>
        /// Numeric Month of Nepali date
        /// </summary>
        public int npMonth
        {
            get { return _npMonth; }
            set
            {
                if (value > 12)
                    throw new Exception("Month cannot be greater than 12.");
                _npMonth = value;
            }
        }

        private int _npDay;

        /// <summary>
        /// Numeric Day of Nepali date
        /// </summary>
        public int npDay
        {
            get { return _npDay; }
            set
            {
                if (value > 32)
                    throw new Exception("Nepali Date cannot be greater than 32.");
                _npDay = value;
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

        public void setFormattedDate(int year, int month, int day, DateFormats date_format)
        {
            string formatted_date = "";
            string new_m = month.ToString();
            string new_d = day.ToString();
            if (month < 10)
            {
                new_m = "0" + month.ToString();
            }

            if (day < 10)
            {
                new_d = "0" + day.ToString();
            }

            switch (date_format)
            {
                case DateFormats.mDy:
                    formatted_date = new_m.ToString() + "-" + new_d.ToString() + "-" + year.ToString();
                    break;
                case DateFormats.dMy:
                    formatted_date = new_d.ToString() + "-" + new_m.ToString() + "-" + year.ToString();
                    break;
                case DateFormats.yMd:
                    formatted_date = year.ToString() + "-" + new_m.ToString() + "-" + new_d.ToString();
                    break;
            }
            _formattedDate = formatted_date;
        }

        public string getFormattedDate()
        {
            if (_formattedDate != null)
                return _formattedDate;
            return npMonth + "-" + npDay + "-" + npYear;
        }
    }
}
