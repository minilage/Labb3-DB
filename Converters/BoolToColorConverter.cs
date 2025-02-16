using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace TinasLabb03.Converters
{
    public class BoolToColorConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            // Hanterar singelbindningar
            if (values.Length == 1 && values[0] is bool singleSelected)
            {
                return singleSelected ? Brushes.Green : Brushes.LightGray;
            }

            // Hanterar multibindningar
            if (values.Length >= 3)
            {
                bool isMultiSelected = values[0] is bool && (bool)values[0];
                bool isMultiCorrect = values[1] is bool && (bool)values[1];
                bool isMultiWrong = values[2] is bool && (bool)values[2];

                if (isMultiSelected && isMultiCorrect)
                    return Brushes.Green;
                if (isMultiWrong)
                    return Brushes.Red;
            }

            return Brushes.LightGray;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}