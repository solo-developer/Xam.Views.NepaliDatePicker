using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xam.Plugins.NepaliDatePicker.Enums;
using Xamarin.Forms;
using static DateConverter.Core.NepaliDate;

namespace Xam.Views.NepaliDatePicker
{
    public partial class MainPage : ContentPage, INotifyPropertyChanged
    {
        public MainPage()
        {
            InitializeComponent();
            BindingContext = this;
        }

        public List<string> Languages
        {
            get
            {
                return Enum.GetNames(typeof(Language)).Select(b => b.ToString()).ToList();
            }
        }

        private Language _selectedLanguage;
        public Language SelectedLanguage
        {
            get => _selectedLanguage;
            set
            {
                _selectedLanguage = value;
                OnPropertyChanged();
            }
        }
    }
}
