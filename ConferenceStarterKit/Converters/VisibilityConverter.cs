using System;
using System.Windows;
using System.Globalization;
using System.Windows.Data;

namespace ConferenceStarterKit.Converters
{
    public class VisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter,CultureInfo culture)
        {
            if (value != null)
            {
                bool Value = (bool)value;

                if (parameter != null)
                {
                    if (parameter.ToString() == "i")
                        return Value ? Visibility.Collapsed : Visibility.Visible;                }
                else
                    return Value ? Visibility.Visible : Visibility.Collapsed;
            }

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

}
