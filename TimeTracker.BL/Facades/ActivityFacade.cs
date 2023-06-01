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


    private void CheckProjectTimeValidate(ActivityDetailModel model)
    {
        if( model.End < model.Start)
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
                act.End >= model.Start &&
                act.Id != model.Id)
            .FirstOrDefault();

        if(userActivitiesOnThisDay is not null)
        {
            throw new DbUpdateException("Activity across other user activity");
        }
    }

    public override async Task<ActivityDetailModel> SaveAsync(ActivityDetailModel model)
    {
        CheckProjectTimeValidate(model);
        await CheckCrossingBetweenActivities(model);

        return await base.SaveAsync(model);
    }

    public async Task<IEnumerable<ActivityListModel>> FilterAsync(DateTime? activityStart = null,
                                                                    DateTime? activityEnd = null,
                                                                    Guid? userId = null,
                                                                    ActivityType? activityType = null)
    {
        await using var uow = UnitOfWorkFactory.Create();

        var query = uow.GetRepository<ActivityEntity, ActivityEntityMapper>().Get()
            .Where(act => 
            (activityStart == null || act.Start >= activityStart) &&
            (activityEnd == null || act.End <= activityEnd) &&
            (userId == null || act.UserId == userId) &&
            (activityType == null || act.Type == activityType));

        return  ModelMapper.MapToListModel(query);
    }

}