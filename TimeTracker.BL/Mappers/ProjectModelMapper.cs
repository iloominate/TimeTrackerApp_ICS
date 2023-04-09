using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeTracker.BL.Models.DetailModels;
using TimeTracker.BL.Models.ListModels;
using TimeTracker.DAL.Entities;

namespace TimeTracker.BL.Mappers;

public class ProjectModelMapper : ModelMapperBase<ProjectEntity, ProjectListModel, ProjectDetailModel>, 
    IProjectModelMapper
{
    private readonly IActivityModelMapper _activityModelMapper;
    private readonly IProjectAmountModelMapper _projectAmountModelMapper;

    public override ProjectDetailModel MapToDetailModel(ProjectEntity? entity)
        => entity is null
            ? ProjectDetailModel.Empty
            : new ProjectDetailModel
            {
                Id = entity.Id,
                Name = entity.Name,
                CreatorId = entity.CreatorId,
                Activities = _activityModelMapper.MapToListModel(entity.Activities)
                    .ToObservableCollection(),
                Users = _projectAmountModelMapper.MapToListModel(entity.Users)
                    .ToObservableCollection()
            };

    public override ProjectListModel MapToListModel(ProjectEntity? entity)
        => entity is null
            ? ProjectListModel.Empty
            : new ProjectListModel
            {
                Id = entity.Id,
                Name = entity.Name,
                CreatorId = entity.CreatorId
            };

    public override ProjectEntity MapToEntity(ProjectDetailModel model)
        => new()
        {
            Id = model.Id,
            Name = model.Name,
            CreatorId = model.CreatorId
        };
}
