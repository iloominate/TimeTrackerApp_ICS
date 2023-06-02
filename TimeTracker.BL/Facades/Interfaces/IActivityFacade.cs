using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeTracker.BL.Models.DetailModels;
using TimeTracker.BL.Models.ListModels;
using TimeTracker.DAL.Entities;
using TimeTracker.Common.Enums;

namespace TimeTracker.BL.Facades.Interfaces;

public interface IActivityFacade : IFacade<ActivityEntity, ActivityListModel, ActivityDetailModel>
{
    public Task<IEnumerable<ActivityListModel>> GetFilteredAsync(
        Guid projectId,
        DateTime? activityStart = null,
        DateTime? activityEnd = null,
        Guid? userId = null,
        ActivityType? activityType = null);
}
