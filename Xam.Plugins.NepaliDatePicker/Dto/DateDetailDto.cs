using Xam.Plugins.NepaliDatePicker.Enums;

namespace Xam.Plugins.NepaliDatePicker.Dto
{
    public class DateDetailDto
    {
        public int SelectedDate { get; set; }
        public int SelectedYear { get; set; }
        public int SelectedMonth { get; set; }

        public Language DisplayLanguage { get; set; }
    }
}
