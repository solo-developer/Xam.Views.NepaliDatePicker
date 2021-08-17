using Xamarin.Forms;

namespace Xam.Plugins.NepaliDatePicker.AttachedProperties
{
    public class CustomAttribute
    {
        public static readonly BindableProperty IdProperty =
      BindableProperty.CreateAttached("Id", typeof(int), typeof(CustomAttribute), defaultValue: -1);

        public static int GetId(BindableObject view)
        {
            return (int)view.GetValue(IdProperty);
        }

        public static void SetId(BindableObject view, int value)
        {
            view.SetValue(IdProperty, value);
        }
    }
}
