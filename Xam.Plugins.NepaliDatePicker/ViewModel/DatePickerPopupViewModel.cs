using DateConverter.Core;
using DateConverter.Core.Library;
using System.Collections.ObjectModel;
using Xam.Plugins.NepaliDatePicker.Dto;
using Xamarin.Forms;
using Unity;
using System.Linq;
using Xam.Plugins.NepaliDatePicker.Enums;
using Xam.Plugins.NepaliDatePicker.Library;

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
            this.SelectedMonth = model.SelectedMonth;
            this.SelectedYear = model.SelectedYear;
            this.DisplayLanguage = model.DisplayLanguage;
            IsEnglishLanguage = model.DisplayLanguage == Language.English;

            InitialDate = (model.SelectedYear, model.SelectedMonth, model.SelectedDate);
            var unityContainer = UnityFactory.getUnityContainer();

            this._nepaliDateData = unityContainer.Resolve<iNepaliDateData>();
            this._dateConverter = unityContainer.Resolve<iDateConverter>();
            SetStartAndEndMonthDetail();
            Years = new ObservableCollection<AvailableYear>();
            InitAvailableYears();
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

        public ObservableCollection<AvailableYear> Years
        {
            get => GetValue<ObservableCollection<AvailableYear>>();
            set => SetValue(value);
        }

        private bool _isCalendarNavigationPerformed = false;


        public (int year, int month, int day) InitialDate
        {
            get => GetValue<(int year, int month, int day)>();
            set => SetValue(value);
        }


        public int SelectedYear
        {
            get => GetValue<int>();
            set => SetValue(value);
        }

        public int SelectedMonth
        {
            get => GetValue<int>();
            set => SetValue(value);
        }


        public int SelectedDay
        {
            get => GetValue<int>();
            set => SetValue(value);
        }


        public int LastDayOfMonth
        {
            get => GetValue<int>();
            set => SetValue(value);
        }

        public int FirstDayOfWeek
        {
            get => GetValue<int>();
            set => SetValue(value);
        }

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
            bool isPageOpenedAtFirstWithoutDatePassed = SelectedYear == 0 || SelectedMonth == 0 || SelectedDay == 0;
            if (isPageOpenedAtFirstWithoutDatePassed)
            {
                var nepaliDateEquivalentOfToday = _dateConverter.ToBS(System.DateTime.Now, NepaliDate.DateFormats.yMd);
                LastDayOfMonth = nepaliDateEquivalentOfToday.npDaysInMonth;
                SelectedYear = nepaliDateEquivalentOfToday.npYear;
                SelectedMonth = nepaliDateEquivalentOfToday.npMonth;
                SelectedDay = nepaliDateEquivalentOfToday.npDay;
                SelectedDayOfWeek = nepaliDateEquivalentOfToday.dayNumber;
                FirstDayOfWeek = _dateConverter.ToAD($"{SelectedMonth}/01/{SelectedYear}").dayNumber;
                InitialDate = (SelectedYear, SelectedMonth, SelectedDay);
            }
            else if (SelectedYear != 0 && SelectedMonth != 0 && SelectedDay != 0 && !_isCalendarNavigationPerformed)
            {
                var engEquivalentOfSelectedDate = _dateConverter.ToAD($"{SelectedMonth}/{SelectedDay}/{SelectedYear}");
                LastDayOfMonth = _nepaliDateData.getLastDayOfMonthNep(SelectedYear, SelectedMonth);
                SelectedDayOfWeek = engEquivalentOfSelectedDate.dayNumber;
                FirstDayOfWeek = _dateConverter.ToAD($"{SelectedMonth}/01/{SelectedYear}").dayNumber;
                InitialDate = (SelectedYear, SelectedMonth, SelectedDay);
            }
            else if (_isCalendarNavigationPerformed)
            {
                string firstDayOfMonth = $"{SelectedMonth}/01/{SelectedYear}";
                var englishEquivalentDate = _dateConverter.ToAD(firstDayOfMonth);
                FirstDayOfWeek = englishEquivalentDate.dayNumber;
                LastDayOfMonth = _nepaliDateData.getLastDayOfMonthNep(SelectedYear, SelectedMonth);
            }
        }

        internal void SetCalendarNavigation()
        {
            _isCalendarNavigationPerformed = true;
        }
        internal void UnsetCalendarNavigation()
        {
            _isCalendarNavigationPerformed = false;
        }

        internal void PublishDate()
        {
            MessagingCenter.Send<DateDetailDto>(new DateDetailDto()
            {
                SelectedDate = InitialDate.day,
                SelectedMonth = InitialDate.month,
                SelectedYear = InitialDate.year
            }, "date_selected");
        }

        internal void SelectYear(int year)
        {
            SelectedYear = year;
            var totalDaysInMonth = _nepaliDateData.getLastDayOfMonthNep(year, SelectedMonth);

            if (totalDaysInMonth < SelectedDay)
                SelectedDay = totalDaysInMonth;
            InitialDate = (year, SelectedMonth, SelectedDay);
            UnsetCalendarNavigation();
            ShowYearList = false;
            SetStartAndEndMonthDetail();
        }
        internal void InitAvailableYears()
        {
            this.Years.Clear();
            for (var i = 2000; i < 2100; i++)
            {
                var color = i == SelectedYear ? SelectedDateColor : Color.Black;
                var fontSize = i == SelectedYear ? (double)22 : (double)16;
                Years.Add(new AvailableYear() { Year = i, Color = color.ToHex(), TextSize = fontSize, YearInNepaliFormat = DisplayLanguage == Language.English ? string.Empty : EnglishToNepaliNumber.ConvertToNepaliNumber(i) });
            };
        }

        internal void SelectPreviousMonth()
        {
            if (SelectedMonth == 1)
            {
                SelectedMonth = 12;
                SelectedYear = SelectedYear - 1;
            }
            else
                SelectedMonth -= 1;
            SetCalendarNavigation();
            SetStartAndEndMonthDetail();
        }

        internal void SelectNextMonth()
        {
            if (SelectedMonth == 12)
            {
                SelectedMonth = 1;
                SelectedYear += 1;
            }
            else
                SelectedMonth += 1;

            SetCalendarNavigation();
            SetStartAndEndMonthDetail();
        }

        internal void OnYearListShown()
        {
            ShowYearList = true;
            InitAvailableYears();
        }
    }
}
