using Xam.Plugins.NepaliDatePicker.ViewModel;
using Xamarin.Forms.Xaml;
using System;
using Xamarin.Forms;
using System.Linq;
using Xam.Plugins.NepaliDatePicker.Dto;

namespace Xam.Plugins.NepaliDatePicker
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DatePickerPopupPage : Rg.Plugins.Popup.Pages.PopupPage
    {
        private readonly Color SelectedDateColor = Color.HotPink;

        public DatePickerPopupPage(DateDetailDto model)
        {
            InitializeComponent();
            var vm = new DatePickerPopupViewModel(model);
            BindingContext = vm;
            InitCalendarView();
        }

        private void InitCalendarView()
        {
            var vm = (DatePickerPopupViewModel)BindingContext;
            dayStackLayout.Children.Clear();

            for (var i = 0; i < 7; i++)
            {
                var label = new Label()
                {
                    Text = $"{vm.DayFirstLetters[i]}",
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
            int startColumnIndex = vm.FirstDayOfWeek - 1;
            var isSameYearAndMonthAsSelected = vm.SelectedYear == vm.InitialDate.year && vm.SelectedMonth == vm.InitialDate.month;
            for (var i = 1; i <= vm.LastDayOfMonth; i++)
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
                if (i == vm.SelectedDay && isSameYearAndMonthAsSelected)
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
            var vm = (DatePickerPopupViewModel)BindingContext;
            var frame = arg1 as Frame;
            if (frame == null)
                return;
            var dayLabel = frame.Content as Label;
            if (string.IsNullOrEmpty(dayLabel.Text))
                return;
            RemoveDateSelectionVisualDisplay();
            ((DatePickerPopupViewModel)BindingContext).SelectedDay = Convert.ToInt32(dayLabel.Text);
            ((DatePickerPopupViewModel)BindingContext).InitialDate = (vm.SelectedYear, vm.SelectedMonth, vm.SelectedDay);
            int dayIndex = Grid.GetColumn(frame);
            ((DatePickerPopupViewModel)BindingContext).SelectedDayOfWeek = dayIndex + 1;
            frame.BackgroundColor = SelectedDateColor;
            ((DatePickerPopupViewModel)BindingContext).UnsetCalendarNavigation();

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

        private void PrevMonthImageButton_Clicked(object sender, EventArgs e)
        {
            var vm = (DatePickerPopupViewModel)BindingContext;
            vm.SelectPreviousMonth();
            InitCalendarView();
        }

        private void NextMonthImageButton_Clicked(object sender, EventArgs e)
        {
            var vm = (DatePickerPopupViewModel)BindingContext;
            vm.SelectNextMonth();
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
            var selectedItem = e.Item as AvailableYear;
            if (selectedItem == null)
            {
                return;
            }
            var vm = (DatePickerPopupViewModel)BindingContext;
            vm.SelectYear(selectedItem.Year);
            InitCalendarView();
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            var vm = (DatePickerPopupViewModel)BindingContext;
            vm.OnYearListShown();           
            var selectedYear = vm.Years.Where(a => a.Year == vm.SelectedYear).Single();
            yearListView.ScrollTo(selectedYear, ScrollToPosition.Start, false);
        }
    }
}