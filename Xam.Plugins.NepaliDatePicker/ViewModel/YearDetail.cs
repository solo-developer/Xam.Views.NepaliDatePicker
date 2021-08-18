using System;

namespace Xam.Plugins.NepaliDatePicker.ViewModel
{
    public class YearDetail
    {
        public int Year { get; set; }

        /// <summary>
        /// Nepali number of equivalent year
        /// </summary>
        public string YearInNepaliFormat { get; set; }

        /// <summary>
        /// Text Color of Year label in View while navigating through Year
        /// </summary>
        public string Color { get; set; }

        /// <summary>
        /// font size of label in view (is bigger for selected year)
        /// </summary>
        public double TextSize { get; set; }
    }
}
