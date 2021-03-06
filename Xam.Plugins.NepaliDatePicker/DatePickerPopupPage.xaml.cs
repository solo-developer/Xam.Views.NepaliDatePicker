using Xam.Plugins.NepaliDatePicker.ViewModel;
using Xamarin.Forms.Xaml;
using System;
using Xamarin.Forms;
using System.Linq;
using Xam.Plugins.NepaliDatePicker.Dto;
using Xam.Plugins.NepaliDatePicker.Library;
using Xam.Plugins.NepaliDatePicker.AttachedProperties;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Xam.Plugins.NepaliDatePicker
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DatePickerPopupPage : Rg.Plugins.Popup.Pages.PopupPage
    {
        private readonly Color _selectedDateColor = Color.HotPink;
        private readonly string[] _englishDayFirstLetters = new string[] { "S", "M", "T", "W", "T", "F", "S" };
        private readonly string[] _nepaliDayFirstLetters = new string[] { "आ", "सो", "मं", "बु", "बि", "शु", "श" };

        DateDetailDto _dto;
        public DatePickerPopupPage(DateDetailDto model)
        {
            _dto = model;
            InitializeComponent();
            var vm = new DatePickerPopupViewModel(model);
            BindingContext = vm;
            Task.Run(() => InitCalendarView());
        }

        private List<Label> _headers;
        private List<Label> Headers
        {
            get
            {
                if (_headers != null)
                    return _headers;

                double fontSize = _dto.DisplayLanguage == Enums.Language.English ? Device.GetNamedSize(NamedSize.Micro, typeof(Label)) : 12;
                _headers = new List<Label>();
                for (var i = 0; i < 7; i++)
                {
                    var firstLetterOfDay = _dto.DisplayLanguage == Enums.Language.English ? _englishDayFirstLetters[i] : _nepaliDayFirstLetters[i];
                    var label = new Label()
                    {
                        Text = $"{ firstLetterOfDay}",
                        FontSize = fontSize,
                        VerticalOptions = LayoutOptions.Center,
                        HorizontalOptions = LayoutOptions.Center,
                        TextColor = Color.Black
                    };
                    Grid.SetRow(label, 0);
                    Grid.SetColumn(label, i);
                    _headers.Add(label);
                }
                return _headers;
            }
        }
        private void InitCalendarView()
        {
            var vm = (DatePickerPopupViewModel)BindingContext;
            dayStackLayout.Children.Clear();

            foreach (var header in Headers)
            {
                dayStackLayout.Children.Add(header);
            }

            int startRowIndex = 1;
            int startColumnIndex = vm.FirstDayOfWeek - 1;
            var isSameYearAndMonthAsSelected = vm.CurrentCalendarYear == vm.UserSelection.year && vm.CurrentCalendarMonth == vm.UserSelection.month;
            for (var i = 1; i <= vm.LastDayOfMonth; i++)
            {
                var languageBasedName = vm.DisplayLanguage == Enums.Language.English ? i.ToString() : EnglishToNepaliNumber.ConvertToNepaliNumber(i);
                var label = new Label()
                {
                    Text = $"{languageBasedName}",
                    FontSize = 12,
                    VerticalOptions = LayoutOptions.Center,
                    HorizontalOptions = LayoutOptions.Center,
                    TextColor = Color.Black,
                    Padding = 10
                };
                CustomAttribute.SetId(label, i);

                var frame = new Frame() { CornerRadius = 20, Padding = 0 };
                frame.Content = label;
                frame.BackgroundColor = Color.Transparent;
                frame.IsClippedToBounds = true;
                if (i == vm.SelectedDay && isSameYearAndMonthAsSelected)
                {
                    frame.BackgroundColor = _selectedDateColor;
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
            var vm = (DatePickerPopupViewModel)BindingContext;
            var frame = arg1 as Frame;
            if (frame == null)
                return;
            var dayLabel = frame.Content as Label;
            if (string.IsNullOrEmpty(dayLabel.Text))
                return;
            RemoveDateSelectionVisualDisplay();
            ((DatePickerPopupViewModel)BindingContext).SelectedDay = CustomAttribute.GetId(dayLabel);
            ((DatePickerPopupViewModel)BindingContext).UserSelection = (vm.CurrentCalendarYear, vm.CurrentCalendarMonth, vm.SelectedDay);
            int dayIndex = Grid.GetColumn(frame);
            ((DatePickerPopupViewModel)BindingContext).SelectedDayOfWeek = dayIndex + 1;
            frame.BackgroundColor = _selectedDateColor;
            ((DatePickerPopupViewModel)BindingContext).UnsetMonthNavigation();

        }

        private void RemoveDateSelectionVisualDisplay()
        {
            var children = dayStackLayout.Children;
            foreach (var child in children)
            {
                var frame = child as Frame;
                if (frame == null)
                    continue;

                if (frame.BackgroundColor == _selectedDateColor)
                    frame.BackgroundColor = Color.Transparent;
            }
        }

        private void PrevMonthImageButton_Clicked(object sender, EventArgs e)
        {
            var vm = (DatePickerPopupViewModel)BindingContext;
            vm.NavigateToPreviousMonth();
            InitCalendarView();
        }

        private void NextMonthImageButton_Clicked(object sender, EventArgs e)
        {
            var vm = (DatePickerPopupViewModel)BindingContext;
            vm.NavigateToNextMonth();
            InitCalendarView();
        }

        private void OK_button_Clicked(object sender, EventArgs e)
        {
            var vm = (DatePickerPopupViewModel)BindingContext;
            vm.PublishDate();
        }
        private async void Cancel_Button_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }

        private void yearListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var selectedItem = e.Item as YearDetail;
            if (selectedItem == null)
            {
                return;
            }
            var vm = (DatePickerPopupViewModel)BindingContext;
            vm.NavigateToYear(selectedItem.Year);
            InitCalendarView();
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            var vm = (DatePickerPopupViewModel)BindingContext;
            vm.OnYearListShown();
            var selectedYear = vm.Years.Where(a => a.Year == vm.CurrentCalendarYear).Single();
            yearListView.ScrollTo(selectedYear, ScrollToPosition.Start, false);
        }
    }
}