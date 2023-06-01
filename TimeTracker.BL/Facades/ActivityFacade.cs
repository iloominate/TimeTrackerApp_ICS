using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeTracker.BL.Facades.Interfaces;
using TimeTracker.BL.Facades;
using TimeTracker.BL.Mappers;
using TimeTracker.BL.Models.DetailModels;
using TimeTracker.BL.Models.ListModels;
using TimeTracker.DAL.Entities;
using TimeTracker.DAL.Mappers;
using TimeTracker.DAL.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using TimeTracker.Common.Enums;

namespace TimeTracker.BL.Facadesl;

public class ActivityFacade : FacadeBase<ActivityEntity, ActivityListModel,
    ActivityDetailModel, ActivityEntityMapper>, IActivityFacade
{
    private readonly IActivityModelMapper _activityModelMapper;
    public ActivityFacade(
        IUnitOfWorkFactory unitOfWorkFactory,
        IActivityModelMapper activityModelMapper)
        : base(unitOfWorkFactory, activityModelMapper) =>
        _activityModelMapper = activityModelMapper;


    private void CheckProjectTimeValidate(DateTime? start, DateTime? end)
    {
        if (start.HasValue && end.HasValue && end < start)
        { 
            throw new Exception("End time must be greater then Start time");
        }
    }

    private async Task CheckCrossingBetweenActivities(ActivityDetailModel model)
    {

        if (model.UserId == Guid.Empty)
        {
            throw new NullReferenceException("Activity doesn't have a User");
        }

        await using var uow = UnitOfWorkFactory.Create();

        var userActivitiesOnThisDay = uow
            .GetRepository<ActivityEntity, ActivityEntityMapper>()
            .Get()
            .Where(act => act.UserId == model.UserId &&
                act.Start <= model.End &&
                act.End >= model.Start)
            .FirstOrDefault();

        if(userActivitiesOnThisDay is not null)
        {
            throw new DbUpdateException("Activity across other user activity");
        }
    }

    public override async Task<ActivityDetailModel> SaveAsync(ActivityDetailModel model)
    {
        CheckProjectTimeValidate(model.Start, model.End);
        await CheckCrossingBetweenActivities(model);

        return await base.SaveAsync(model);
    }

    public async Task<IEnumerable<ActivityListModel>> GetFilteredAsync(Guid projectId,
                                                                    DateTime? activityStart = null,
                                                                    DateTime? activityEnd = null,
                                                                    Guid? userId = null,
                                                                    ActivityType? activityType = null)
    {
        CheckProjectTimeValidate(activityStart, activityEnd);
        await using var uow = UnitOfWorkFactory.Create();

        List<ActivityEntity> activities = uow.GetRepository<ActivityEntity, ActivityEntityMapper>().Get()
            .Where(act => 
            (act.ProjectId == projectId) &&
            (activityStart == null || act.Start >= activityStart) &&
            (activityEnd == null || act.End <= activityEnd) &&
            (userId == null || act.UserId == userId) &&
            (activityType == null || act.Type == activityType)).ToList();

        return ModelMapper.MapToListModel(activities);
    }

}