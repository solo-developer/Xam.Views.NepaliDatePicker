using DateConverter.Core.Library;
using System.ComponentModel;
using Xam.Views.NepaliDatePicker.ViewModel;
using Xamarin.Forms.Xaml;
using Unity;
using System;
using Xamarin.Forms;
using DateConverter.Core;
using System.Collections.ObjectModel;
using System.Linq;

namespace Xam.Views.NepaliDatePicker
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DatePickerPopupPage : Rg.Plugins.Popup.Pages.PopupPage, INotifyPropertyChanged
    {
        private readonly Color SelectedDateColor = Color.HotPink;
        private readonly iNepaliDateData _nepaliDateData;
        private readonly iDateConverter _dateConverter;
        private readonly string[] DayFirstLetters = new string[] { "S", "M", "T", "W", "T", "F", "S" };

        public DatePickerPopupPage(DateDetailViewModel model)
        {

            InitializeComponent();
            this.SelectedDay = model.SelectedDate;
            this.SelectedMonth = model.SelectedMonth;
            this.SelectedYear = model.SelectedYear;

            InitialDate = (model.SelectedYear, model.SelectedMonth, model.SelectedDate);
            var unityContainer = UnityFactory.getUnityContainer();

            this._nepaliDateData = unityContainer.Resolve<iNepaliDateData>();
            this._dateConverter = unityContainer.Resolve<iDateConverter>();

            SetStartAndEndMonthDetail();
            InitCalendarView();
            Years = new ObservableCollection<AvailableYear>();
            BindingContext = this;
        }

        private ObservableCollection<AvailableYear> _years;

        public ObservableCollection<AvailableYear> Years
        {
            get => _years;
            set
            {
                _years = value;
                OnPropertyChanged(nameof(Years));
            }
        }
        private bool _isCalendarNavigationPerformed = false;

        private (int year, int month, int day) _initialDate;
        public (int year, int month, int day) InitialDate
        {
            get => _initialDate;
            set
            {
                if (_initialDate != value)
                {
                    _initialDate = value;
                    OnPropertyChanged(nameof(InitialDate));
                }
            }
        }
        private int _selectedYear;
        public int SelectedYear
        {
            get => _selectedYear;
            set
            {
                if (value != _selectedYear)
                {
                    _selectedYear = value;
                    OnPropertyChanged(nameof(SelectedYear));
                }
            }
        }
        private int _selectedMonth;
        public int SelectedMonth
        {
            get => _selectedMonth;
            set
            {
                if (value != _selectedMonth)
                {
                    _selectedMonth = value;
                    OnPropertyChanged(nameof(SelectedMonth));
                }
            }
        }

        private int _selectedDay;
        public int SelectedDay
        {
            get => _selectedDay;
            set
            {
                if (value != _selectedDay)
                {
                    _selectedDay = value;
                    OnPropertyChanged(nameof(SelectedDay));
                }
            }
        }


        private int _lastDayOfMonth;
        public int LastDayOfMonth
        {
            get => _lastDayOfMonth;
            set
            {
                if (value != _lastDayOfMonth)
                {
                    _lastDayOfMonth = value;
                    OnPropertyChanged(nameof(LastDayOfMonth));
                }
            }
        }

        private int _firstDayOfWeek;
        public int FirstDayOfWeek
        {
            get => _firstDayOfWeek;
            set
            {
                if (value != _firstDayOfWeek)
                {
                    _firstDayOfWeek = value;
                    OnPropertyChanged(nameof(FirstDayOfWeek));
                }
            }
        }

        private int _selectedDayOfWeek;
        public int SelectedDayOfWeek
        {
            get => _selectedDayOfWeek;
            set
            {
                if (value != _selectedDayOfWeek)
                {
                    _selectedDayOfWeek = value;
                    OnPropertyChanged(nameof(SelectedDayOfWeek));
                }
            }
        }

        private bool _showYearList;
        public bool ShowYearList
        {
            get => _showYearList;
            set
            {
                _showYearList = value;
                OnPropertyChanged(nameof(ShowYearList));
            }
        }

        private void InitCalendarView()
        {
            dayStackLayout.Children.Clear();

            for (var i = 0; i < 7; i++)
            {
                var label = new Label()
                {
                    Text = $"{DayFirstLetters[i]}",
                    FontSize = Device.GetNamedSize(NamedSize.Micro, typeof(Label)),
                    VerticalOptions = LayoutOptions.Center,
                    HorizontalOptions = LayoutOptions.Center,
                    TextColor = Color.Black
                };
                Grid.SetRow(label, 0);
                Grid.SetColumn(label, i);
                dayStackLayout.Children.Add(label);
            }
            int startRowIndex = 1;
            int startColumnIndex = FirstDayOfWeek - 1;
            var isSameYearAndMonthAsSelected = SelectedYear == InitialDate.year && SelectedMonth == InitialDate.month;
            for (var i = 1; i <= LastDayOfMonth; i++)
            {
                var label = new Label()
                {
                    Text = $"{i}",
                    FontSize = Device.GetNamedSize(NamedSize.Micro, typeof(Label)),
                    VerticalOptions = LayoutOptions.Center,
                    HorizontalOptions = LayoutOptions.Center,
                    // FontAttributes = FontAttributes.Bold,
                    TextColor = Color.Black,
                    Padding = 10
                };

                var frame = new Frame() { CornerRadius = 30, Padding = 0 };
                frame.Content = label;
                frame.BackgroundColor = Color.Transparent;
                if (i == SelectedDay && isSameYearAndMonthAsSelected)
                {
                    frame.BackgroundColor = SelectedDateColor;
                }
                Grid.SetRow(frame, startRowIndex);
                Grid.SetColumn(frame, startColumnIndex);
                frame.GestureRecognizers.Add(new TapGestureRecognizer(OnDateSelected));
                dayStackLayout.Children.Add(frame);
                startColumnIndex++;
                if (startColumnIndex == 7)
                {
                    startRowIndex++;
                    startColumnIndex = 0;
                }
            }
        }

        private void OnDateSelected(View arg1, object arg2)
        {
            var frame = arg1 as Frame;
            if (frame == null)
                return;
            var dayLabel = frame.Content as Label;
            if (string.IsNullOrEmpty(dayLabel.Text))
                return;
            RemoveDateSelectionVisualDisplay();
            SelectedDay = Convert.ToInt32(dayLabel.Text);
            InitialDate = (SelectedYear, SelectedMonth, SelectedDay);
            int dayIndex = Grid.GetColumn(frame);
            SelectedDayOfWeek = dayIndex + 1;
            frame.BackgroundColor = SelectedDateColor;
            _isCalendarNavigationPerformed = false;

        }

        private void RemoveDateSelectionVisualDisplay()
        {
            var children = dayStackLayout.Children;
            foreach (var child in children)
            {
                var frame = child as Frame;
                if (frame == null)
                    continue;

                if (frame.BackgroundColor == SelectedDateColor)
                    frame.BackgroundColor = Color.Transparent;
            }
        }

        private void SetStartAndEndMonthDetail()
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

        private void PrevMonthImageButton_Clicked(object sender, EventArgs e)
        {
            if (SelectedMonth == 1)
            {
                SelectedMonth = 12;
                SelectedYear = SelectedYear - 1;
            }
            else
                SelectedMonth -= 1;
            _isCalendarNavigationPerformed = true;
            SetStartAndEndMonthDetail();
            InitCalendarView();
        }

        private void InitAvailableYears()
        {
            Years.Clear();
            for (var i = 2000; i < 2100; i++)
            {
                var color = i == SelectedYear ? SelectedDateColor : Color.Black;
                var fontSize = i == SelectedYear ?(double)22 : (double)16;
                Years.Add(new AvailableYear() { Year = i, Color = color.ToHex(), TextSize = fontSize });
            };
        }

        private void NextMonthImageButton_Clicked(object sender, EventArgs e)
        {
            if (SelectedMonth == 12)
            {
                SelectedMonth = 1;
                SelectedYear += 1;
            }
            else
                SelectedMonth += 1;

            _isCalendarNavigationPerformed = true;
            SetStartAndEndMonthDetail();
            InitCalendarView();
        }

        private void OK_button_Clicked(object sender, EventArgs e)
        {
            MessagingCenter.Send<DateDetailViewModel>(new DateDetailViewModel()
            {
                SelectedDate = SelectedDay,
                SelectedMonth = SelectedMonth,
                SelectedYear = SelectedYear
            }, "date_selected");
        }
        private async void Cancel_Button_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }


        private void yearListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var selectedItem = e.Item as AvailableYear;
            if (selectedItem == null)
            {
                return;
            }
            this.SelectedYear = selectedItem.Year;
            var totalDaysInMonth = _nepaliDateData.getLastDayOfMonthNep(selectedItem.Year, SelectedMonth);

            if (totalDaysInMonth < SelectedDay)
                this.SelectedDay = totalDaysInMonth;
            this.InitialDate = (selectedItem.Year, SelectedMonth, SelectedDay);
            _isCalendarNavigationPerformed = false;
            this.ShowYearList = false;
            SetStartAndEndMonthDetail();
            InitCalendarView();
          
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            InitAvailableYears();
            ShowYearList = true;
            var selectedYear = Years.Where(a => a.Year == SelectedYear).Single();
            yearListView.ScrollTo(selectedYear, ScrollToPosition.Center, true);
        }
    }
}