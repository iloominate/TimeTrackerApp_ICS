using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace TimeTracker.App.Converters;
public static class UserIdToColorConverter
{
    public static Color Convert(Guid userId)
    {
        // Convert the Guid to a byte array
        byte[] bytes = userId.ToByteArray();

        // Take the first three bytes from the array
        byte r = bytes[0];
        byte g = bytes[1];
        byte b = bytes[2];

        // Create a Color object from the RGB values
        Color color = Color.FromRgb(r, g, b);

        return color;
    }
}
