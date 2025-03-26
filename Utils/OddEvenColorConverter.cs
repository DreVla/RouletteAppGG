using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace RouletteApp.Utils
{
    /// <summary>
    /// Converts a roulette position to a corresponding color.
    /// </summary>
    public class OddEvenColorConverter : IValueConverter
    {
        /// <summary>
        /// Converts a roulette position to a SolidColorBrush based on the position's value.
        /// </summary>
        /// <param name="value">The roulette position as an integer.</param>
        /// <param name="targetType">The target type (not used).</param>
        /// <param name="parameter">Additional parameter (not used).</param>
        /// <param name="culture">The culture information (not used).</param>
        /// <returns>A SolidColorBrush representing the color for the given position.</returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is int position)
            {
                if ((position >= 1 && position <= 10) || (position >= 19 && position <= 28))
                {
                    return (position % 2 == 0) ? new SolidColorBrush(Colors.Black) : new SolidColorBrush(Colors.Red);
                }
                else if ((position >= 11 && position <= 18) || (position >= 29 && position <= 36))
                {
                    return (position % 2 == 0) ? new SolidColorBrush(Colors.Red) : new SolidColorBrush(Colors.Black);
                }
                else if (position == 0)
                {
                    return new SolidColorBrush(Colors.Green);
                }
            }
            return new SolidColorBrush(Colors.Black);
        }

        /// <summary>
        /// Method not implemented. Converts back from a SolidColorBrush to a roulette position.
        /// </summary>
        /// <param name="value">The value to convert back (not used).</param>
        /// <param name="targetType">The target type (not used).</param>
        /// <param name="parameter">Additional parameter (not used).</param>
        /// <param name="culture">The culture information (not used).</param>
        /// <returns>Throws NotImplementedException.</returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}