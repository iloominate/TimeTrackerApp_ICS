using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeTracker.App;

public class ActivityDeleteIsVisableConverter : IMultiValueConverter
{
    public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
    {
        bool isVisable = false;
        if (values[0] != null && values[1] != null && values[2] != null)
        {
            return isVisable = values[0].Equals(values[1]) || values[0].Equals(values[2]);
        }
        return isVisable;
    }
    public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
