using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeTracker.BL.Models.DetailModels;
using TimeTracker.BL.Models.ListModels;
using TimeTracker.DAL.Entities;

namespace TimeTracker.BL.Mappers;

public class UserModelMapper : ModelMapperBase<UserEntity, UserListModel, UserDetailModel> , IUserModelMapper
{
    private readonly IProjectAmountModelMapper _projectAmountModelMapper;
    private readonly IProjectModelMapper _projectModelMapper;
    private readonly IActivityModelMapper _activityModelMapper;

    public override UserListModel MapToListModel(UserEntity? entity)
        => entity is null
            ? UserListModel.Empty
            : new UserListModel
            {
                Id = entity.Id,
                Name = entity.Name,
                Surname = entity.Surname,
            };

    public override UserDetailModel MapToDetailModel (UserEntity? entity)
        => entity is null
            ? UserDetailModel.Empty
            : new UserDetailModel
            {
                Id = entity.Id,
                Name = entity.Name,
                Surname = entity.Surname,
                PhotoUrl = entity.PhotoUrl,
                Activities = _activityModelMapper.MapToListModel(entity.Activities)
                    .ToObservableCollection(),
                Projects = _projectAmountModelMapper.MapToListModel(entity.Projects)
                    .ToObservableCollection(),
                ProjectsCreared = _projectModelMapper.MapToListModel(entity.CreatedProjects)
                    .ToObservableCollection()
            };

    public override UserEntity MapToEntity(UserDetailModel model)
        => new()
        {
            Id = model.Id,
            Name = model.Name,
            Surname = model.Surname,
            PhotoUrl = model.PhotoUrl
        };
    

}
