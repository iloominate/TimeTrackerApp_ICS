using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Maui.Graphics;

namespace TimeTracker.App;
public class UserIdToColorConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        Color returnColor = new Color(255, 255, 255);
        if (value is Guid id)
        {
            int hash = id.GetHashCode();
            returnColor = GetColorFromHash(hash);
        }
        return returnColor;
    }
    private Color GetColorFromHash(int hash)
    {
        byte r = (byte)(hash & 0xFF);
        byte g = (byte)((hash >> 8) & 0xFF);
        byte b = (byte)((hash >> 16) & 0xFF);

        Color color = new Color((float)(r / 255.0), (float)(g / 255.0), (float)(b / 255.0));

        return color;
    }
    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }

}
