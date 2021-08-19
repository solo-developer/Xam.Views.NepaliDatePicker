using DateConverter.Core;
using DateConverter.Core.Library;
using System.Collections.ObjectModel;
using Xam.Plugins.NepaliDatePicker.Dto;
using Xamarin.Forms;
using Unity;
using Xam.Plugins.NepaliDatePicker.Enums;
using Xam.Plugins.NepaliDatePicker.Library;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.ObjectModel;
using System.Collections.Generic;
using System.Linq;

namespace Xam.Plugins.NepaliDatePicker.ViewModel
{
    public class DatePickerPopupViewModel : ViewModelBase
    {
        private readonly Color SelectedDateColor = Color.HotPink;
        private readonly iNepaliDateData _nepaliDateData;
        private readonly iDateConverter _dateConverter;

        public DatePickerPopupViewModel(DateDetailDto model)
        {
            this.SelectedDay = model.SelectedDate;
            this.CurrentCalendarMonth = model.SelectedMonth;
            this.CurrentCalendarYear = model.SelectedYear;
            this.DisplayLanguage = model.DisplayLanguage;
            IsEnglishLanguage = model.DisplayLanguage == Language.English;

            UserSelection = (model.SelectedYear, model.SelectedMonth, model.SelectedDate);
            var unityContainer = UnityFactory.getUnityContainer();

            this._nepaliDateData = unityContainer.Resolve<iNepaliDateData>();
            this._dateConverter = unityContainer.Resolve<iDateConverter>();
            SetStartAndEndMonthDetail();
            Years = new ObservableRangeCollection<YearDetail>();
            Task.Run(() => InitAvailableYears());
        }

        public string OKButtonText
        {
            get => DisplayLanguage == Enums.Language.English ? "OK" : "ठीक छ";
        }
        public string CancelButtonText
        {
            get => DisplayLanguage == Enums.Language.English ? "Cancel" : "रद्द गर्नुहोस्";
        }

        public bool IsEnglishLanguage
        {
            get => GetValue<bool>();
            set => SetValue(value);
        }
        public Language DisplayLanguage
        {
            get => GetValue<Language>();
            set => SetValue(value);
        }

        /// <summary>
        /// List of years that is available in calendar
        /// </summary>
        public ObservableRangeCollection<YearDetail> Years
        {
            get => GetValue<ObservableRangeCollection<YearDetail>>();
            set => SetValue(value);
        }

        private bool _isMonthNavigated = false;

        /// <summary>
        /// Actual value of date user has selected
        /// </summary>
        public (int year, int month, int day) UserSelection
        {
            get => GetValue<(int year, int month, int day)>();
            set => SetValue(value);
        }

        /// <summary>
        /// Current Calendar navigation year
        /// </summary>
        public int CurrentCalendarYear
        {
            get => GetValue<int>();
            set => SetValue(value);
        }

        /// <summary>
        /// Current Calendar navigated month
        /// </summary>
        public int CurrentCalendarMonth
        {
            get => GetValue<int>();
            set => SetValue(value);
        }


        public int SelectedDay
        {
            get => GetValue<int>();
            set => SetValue(value);
        }

        /// <summary>
        /// Last day of the calendar month
        /// </summary>
        public int LastDayOfMonth
        {
            get => GetValue<int>();
            set => SetValue(value);
        }

        /// <summary>
        /// Day number of first day of a month
        /// Value in a range of 1-7
        /// </summary>
        public int FirstDayOfWeek
        {
            get => GetValue<int>();
            set => SetValue(value);
        }


        /// <summary>
        /// Day number of selected date of a month
        /// Value in a range of 1-7
        /// </summary>
        public int SelectedDayOfWeek
        {
            get => GetValue<int>();
            set => SetValue(value);
        }

        public bool ShowYearList
        {
            get => GetValue<bool>();
            set => SetValue(value);
        }

        internal void SetStartAndEndMonthDetail()
        {
            bool isPageOpenedAtFirstWithoutDatePassed = CurrentCalendarYear == 0 || CurrentCalendarMonth == 0 || SelectedDay == 0;
            if (isPageOpenedAtFirstWithoutDatePassed)
            {
                var nepaliDateEquivalentOfToday = _dateConverter.ToBS(System.DateTime.Now, NepaliDate.DateFormats.yMd);
                LastDayOfMonth = nepaliDateEquivalentOfToday.npDaysInMonth;
                CurrentCalendarYear = nepaliDateEquivalentOfToday.npYear;
                CurrentCalendarMonth = nepaliDateEquivalentOfToday.npMonth;
                SelectedDay = nepaliDateEquivalentOfToday.npDay;
                SelectedDayOfWeek = nepaliDateEquivalentOfToday.dayNumber;
                FirstDayOfWeek = _dateConverter.ToAD($"{CurrentCalendarMonth}/01/{CurrentCalendarYear}").dayNumber;
                UserSelection = (CurrentCalendarYear, CurrentCalendarMonth, SelectedDay);
            }
            else if (CurrentCalendarYear != 0 && CurrentCalendarMonth != 0 && SelectedDay != 0 && !_isMonthNavigated)
            {
                var engEquivalentOfSelectedDate = _dateConverter.ToAD($"{CurrentCalendarMonth}/{SelectedDay}/{CurrentCalendarYear}");
                LastDayOfMonth = _nepaliDateData.getLastDayOfMonthNep(CurrentCalendarYear, CurrentCalendarMonth);
                SelectedDayOfWeek = engEquivalentOfSelectedDate.dayNumber;
                FirstDayOfWeek = _dateConverter.ToAD($"{CurrentCalendarMonth}/01/{CurrentCalendarYear}").dayNumber;
                UserSelection = (CurrentCalendarYear, CurrentCalendarMonth, SelectedDay);
            }
            else if (_isMonthNavigated)
            {
                string firstDayOfMonth = $"{CurrentCalendarMonth}/01/{CurrentCalendarYear}";
                var englishEquivalentDate = _dateConverter.ToAD(firstDayOfMonth);
                FirstDayOfWeek = englishEquivalentDate.dayNumber;
                LastDayOfMonth = _nepaliDateData.getLastDayOfMonthNep(CurrentCalendarYear, CurrentCalendarMonth);
            }
        }

        internal void SetMonthNavigation()
        {
            _isMonthNavigated = true;
        }
        internal void UnsetMonthNavigation()
        {
            _isMonthNavigated = false;
        }

        /// <summary>
        /// Publishes event to the subscriber Entry
        /// </summary>
        internal void PublishDate()
        {
            MessagingCenter.Send<DateDetailDto>(new DateDetailDto()
            {
                SelectedDate = UserSelection.day,
                SelectedMonth = UserSelection.month,
                SelectedYear = UserSelection.year
            }, "date_selected");
        }

        internal void NavigateToYear(int year)
        {
            CurrentCalendarYear = year;
            var totalDaysInMonth = _nepaliDateData.getLastDayOfMonthNep(year, CurrentCalendarMonth);

            if (totalDaysInMonth < SelectedDay)
                SelectedDay = totalDaysInMonth;
            UserSelection = (year, CurrentCalendarMonth, SelectedDay);
            UnsetMonthNavigation();
            ShowYearList = false;
            SetStartAndEndMonthDetail();
        }

        /// <summary>
        /// Sets available year from 2000-2100 with font size and color based on current calendar navigated year
        /// </summary>
        internal void InitAvailableYears()
        {
            this.Years.Clear();
            var availableYears = new List<YearDetail>();
            for (var i = 2000; i < 2100; i++)
            {
                var color = i == CurrentCalendarYear ? SelectedDateColor : Color.Black;
                var fontSize = i == CurrentCalendarYear ? (double)22 : (double)16;
                availableYears.Add(new YearDetail()
                {
                    Year = i,
                    Color = color,
                    TextSize = fontSize,
                    YearInNepaliFormat = DisplayLanguage == Language.English ? string.Empty : EnglishToNepaliNumber.ConvertToNepaliNumber(i)
                });
            };
            Years.AddRange(availableYears);
        }

        internal void NavigateToPreviousMonth()
        {
            if (CurrentCalendarMonth == 1)
            {
                CurrentCalendarMonth = 12;
                CurrentCalendarYear = CurrentCalendarYear - 1;
            }
            else
                CurrentCalendarMonth -= 1;
            SetMonthNavigation();
            SetStartAndEndMonthDetail();
        }

        internal void NavigateToNextMonth()
        {
            if (CurrentCalendarMonth == 12)
            {
                CurrentCalendarMonth = 1;
                CurrentCalendarYear += 1;
            }
            else
                CurrentCalendarMonth += 1;

            SetMonthNavigation();
            SetStartAndEndMonthDetail();
        }

        internal void OnYearListShown()
        {
            ShowYearList = true;
            InitAvailableYears();
        }
    }
}
