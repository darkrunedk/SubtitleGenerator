using System;
using System.Globalization;
using System.Windows.Data;

namespace SubtitleGenerator.Models.Converters
{
    public class TruncateConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return string.Empty;

            if (parameter == null)
                return value;

            int maxLength;
            if (!int.TryParse(parameter.ToString(), out maxLength))
                return value;

            string text = value.ToString();
            if (text.Length > maxLength)
                text = text.Substring(0, maxLength) + "...";

            return text;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
