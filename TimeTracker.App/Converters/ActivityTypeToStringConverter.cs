using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Maui.Converters;
using TimeTracker.Common.Enums;
using System.Globalization;

namespace TimeTracker.App.Converters;

public static class ActivityTypeToStringConverter 
{
    public static string Convert(
        ActivityType type)
    {
        return type.ToString();
    }
}
