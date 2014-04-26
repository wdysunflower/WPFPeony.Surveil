using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace WPFPeony.Surveil
{
    /// <summary>
    /// 比较参数以设定Visibility
    /// </summary>
    [ValueConversion(typeof(object), typeof(Visibility))]
    public class EqualToVisConverter : IValueConverter
    {
        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value.Equals(parameter) ? Visibility.Visible : Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
