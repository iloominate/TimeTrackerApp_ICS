using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeTracker.BL.Models;
using TimeTracker.BL.Models.DetailModels;
using TimeTracker.BL.Models.ListModels;
using TimeTracker.DAL.Entities;

namespace TimeTracker.BL.Mappers;

public class ActivityModelMapper : ModelMapperBase<ActivityEntity, ActivityListModel, ActivityDetailModel>,
    IActivityModelMapper
{

    public override ActivityListModel MapToListModel(ActivityEntity? entity)
        => entity is null
            ? ActivityListModel.Empty
            : new ActivityListModel { 
                Id = entity.Id,
                Start = entity.Start,
                End = entity.End,
                Type = entity.Type,
                UserId = entity.UserId,
                ProjectId = entity.ProjectId
            };

    public override ActivityDetailModel MapToDetailModel(ActivityEntity? entity)
        => entity is null
            ? ActivityDetailModel.Empty
            : new ActivityDetailModel
            {
                Id = entity.Id,
                Start = entity.Start,
                End = entity.End,
                Type = entity.Type,
                UserId = entity.UserId,
                ProjectId = entity.ProjectId,
            };

    public override ActivityEntity MapToEntity(ActivityDetailModel model)
        => new() 
        {
            Id = model.Id, 
            Start = model.Start,
            End = model.End,
            Type = model.Type,
            UserId = model.UserId,
            ProjectId = model.ProjectId
        };


}
    