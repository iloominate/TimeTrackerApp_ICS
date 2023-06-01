using CommunityToolkit.Maui.Converters;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeTracker.BL.Models;

namespace TimeTracker.App;

public class UserIsProjectOwnerToIsVisibleConverter : IMultiValueConverter
{

    public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
    {
        bool isVisible = false;
        if (values.Length >= 2 && values[0] != null && values[1] != null)
        {
            return isVisible = values[0].Equals(values[1]);
        }
        return isVisible;
    }

    public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }

}
