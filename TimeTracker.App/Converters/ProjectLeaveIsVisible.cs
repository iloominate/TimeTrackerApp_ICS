using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using TimeTracker.BL.Models.DetailModels;

namespace TimeTracker.App;

public class ProjectLeaveIsVisible : IMultiValueConverter
{
    public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
    {
        bool isVisible = false;
        if (values[0] != null && values[1] != null)
        {
            UserDetailModel user = (UserDetailModel)values[0];
            Guid projectId = (Guid)values[1];

            if (user.Projects.Any(project => project.ProjectId == projectId) 
                && !user.ProjectsCreared.Any(createdProject => createdProject.Id == projectId))
            {
                isVisible = true;
                return isVisible;
            }

        }
        return isVisible;
    }

    public object[] ConvertBack(object value, Type[] targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
