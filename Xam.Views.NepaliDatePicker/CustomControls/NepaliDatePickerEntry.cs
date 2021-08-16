using System;
using Xam.Views.NepaliDatePicker.ViewModel;
using Xamarin.Forms;
using static DateConverter.Core.EnglishDate;

namespace Xam.Views.NepaliDatePicker.CustomControls
{
    public class NepaliDatePickerEntry : Entry
    {
        private const string DATE_SELECTED_EVENT = "date_selected";
        public static readonly BindableProperty CurrentDateProperty = BindableProperty.Create(nameof(CurrentDate), typeof(string), typeof(NepaliDatePickerEntry), defaultValue: string.Empty, propertyChanged: CurrentDatePropertyChanged);


        public static readonly BindableProperty DateFormatProperty = BindableProperty.Create(nameof(DateFormat), typeof(DateFormats), typeof(NepaliDatePickerEntry), defaultValue: DateFormats.yMd, propertyChanged: DateFormatPropertyChanged);


        public static readonly BindableProperty SeparatorProperty = BindableProperty.Create(nameof(Separator), typeof(Char), typeof(NepaliDatePickerEntry), defaultValue: '-', propertyChanged: SeparatorPropertyChanged);

        public NepaliDatePickerEntry()
        {
            this.Focused += openPopupEntry_Focused;
        }

        ~NepaliDatePickerEntry()
        {
            this.Focused -= openPopupEntry_Focused;
        }
        public string CurrentDate
        {
            get => (string)GetValue(CurrentDateProperty);
            set => SetValue(CurrentDateProperty, value);
        }
        public DateFormats DateFormat
        {
            get => (DateFormats)GetValue(DateFormatProperty);
            set => SetValue(DateFormatProperty, value);
        }
        public char Separator
        {
            get => (char)GetValue(SeparatorProperty);
            set => SetValue(SeparatorProperty, value);
        }

        public int SelectedYear { get; set; }

        public int SelectedMonth { get; set; }

        public int SelectedDay { get; set; }

        private void SetDateParts(string date)
        {
            bool isSeparatorPresent = date.IndexOf(Separator) == -1;
            if (isSeparatorPresent)
                return;
            var datePartsByFormat = GetDateParts(date, DateFormat);
            this.SelectedYear = datePartsByFormat.year;
            this.SelectedMonth = datePartsByFormat.month;
            this.SelectedDay = datePartsByFormat.day;
        }

        private static void CurrentDatePropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            ((NepaliDatePickerEntry)bindable).SetDateParts(newValue.ToString());
            ((NepaliDatePickerEntry)bindable).Text = newValue.ToString();
        }
        private static void DateFormatPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var obj = ((NepaliDatePickerEntry)bindable);
            if (!string.IsNullOrWhiteSpace(obj.CurrentDate))
                obj.SetDateParts(newValue.ToString());
        }
        private static void SeparatorPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var obj = ((NepaliDatePickerEntry)bindable);
            if (!string.IsNullOrWhiteSpace(obj.CurrentDate))
                obj.SetDateParts(newValue.ToString());
        }

        private (int year, int month, int day) GetDateParts(string date, DateFormats dateFormat)
        {
            var dateParts = date.Split(Separator);
            if (dateParts.Length != 3)
                return (0, 0, 0);

            int year = DateTime.Now.Year;
            int month, day;
            switch (dateFormat)
            {
                case DateFormats.mDy:
                    Int32.TryParse(dateParts[2], out year);
                    Int32.TryParse(dateParts[1], out day);
                    Int32.TryParse(dateParts[0], out month);
                    break;
                case DateFormats.dMy:
                    Int32.TryParse(dateParts[2], out year);
                    Int32.TryParse(dateParts[1], out month);
                    Int32.TryParse(dateParts[0], out day);
                    break;
                case DateFormats.yMd:
                    Int32.TryParse(dateParts[2], out day);
                    Int32.TryParse(dateParts[1], out month);
                    Int32.TryParse(dateParts[0], out year);
                    break;
                default:
                    return (0, 0, 0);
            }
            return (year, month, day);


        }

        private void openPopupEntry_Focused(object sender, FocusEventArgs e)
        {
            this.Unfocus();
            var model = new DateDetailViewModel()
            {
                SelectedDate = this.SelectedDay,
                SelectedMonth = this.SelectedMonth,
                SelectedYear = this.SelectedYear,
            };
            MessagingCenter.Subscribe<DateDetailViewModel>(this, DATE_SELECTED_EVENT, OnDateSelected);
            Navigation.PushModalAsync(new DatePickerPopupPage(model));
        }

        private async void OnDateSelected(DateDetailViewModel data)
        {
            MessagingCenter.Unsubscribe<DateDetailViewModel>(this, DATE_SELECTED_EVENT);
            await Navigation.PopModalAsync();
            this.SelectedDay = data.SelectedDate;
            this.SelectedMonth = data.SelectedMonth;
            this.SelectedYear = data.SelectedYear;
            var date = GetFormattedDate(data, this.Separator, this.DateFormat);
            this.Text = date;
        }

        private string GetFormattedDate(DateDetailViewModel data, char separator, DateFormats format)
        {
            switch (format)
            {
                case DateFormats.mDy:
                    return $"{data.SelectedMonth.ToString("00")}{separator}{data.SelectedDate.ToString("00")}{separator}{data.SelectedYear}";
                case DateFormats.dMy:
                    return $"{data.SelectedDate.ToString("00")}{separator}{data.SelectedMonth.ToString("00")}{separator}{data.SelectedYear}";
                case DateFormats.yMd:
                    return $"{data.SelectedYear}{separator}{data.SelectedMonth.ToString("00")}{separator}{data.SelectedDate.ToString("00")}";
                default:
                    return string.Empty;
            }
        }

    }
}
