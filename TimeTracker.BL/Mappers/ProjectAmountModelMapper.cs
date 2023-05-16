using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeTracker.BL.Models.DetailModels;
using TimeTracker.BL.Models.ListModels;
using TimeTracker.DAL.Entities;

namespace TimeTracker.BL.Mappers;

public class ProjectAmountModelMapper : ModelMapperBase<ProjectAmountEntity, ProjectAmountListModel, ProjectAmountDetailModel>,
    IProjectAmountModelMapper
{
    public override ProjectAmountDetailModel MapToDetailModel(ProjectAmountEntity? entity)
        => entity is null
        ? ProjectAmountDetailModel.Empty
        : new ProjectAmountDetailModel
        {
            Id = entity.Id,
            UserId = entity.UserId,
            ProjectId = entity.ProjectId
        };

    public ProjectAmountDetailModel MapToNewDetailModel(ProjectListModel project, Guid userId)
        => new()
        {
            Id = Guid.NewGuid(),
            ProjectId = project.Id,
            UserId = userId
        };


    public override ProjectAmountListModel MapToListModel(ProjectAmountEntity? entity)
        => entity is null
        ? ProjectAmountListModel.Empty
        : new ProjectAmountListModel
        {
            Id = entity.Id,
            UserId = entity.UserId,
            ProjectId = entity.ProjectId
        };

    public ProjectAmountListModel MapToListModel(ProjectAmountDetailModel model)
        => new()
        {
            Id = model.Id,
            UserId = model.UserId,
            ProjectId = model.ProjectId
        };

    public override ProjectAmountEntity MapToEntity(ProjectAmountDetailModel model)
        => new()
        {
            Id = model.Id,
            UserId = model.UserId,
            ProjectId = model.ProjectId
        };
}
