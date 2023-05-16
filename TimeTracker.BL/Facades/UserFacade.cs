using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeTracker.BL.Facades.Interfaces;
using TimeTracker.BL.Mappers;
using TimeTracker.BL.Models.DetailModels;
using TimeTracker.BL.Models.ListModels;
using TimeTracker.DAL.Entities;
using TimeTracker.DAL.Mappers;
using TimeTracker.DAL.UnitOfWork;

namespace TimeTracker.BL.Facades;



public class UserFacade : FacadeBase<UserEntity, UserListModel,
    UserDetailModel, UserEntityMapper>, IUserFacade
{
    private readonly IUserModelMapper _userModelMapper;
    public UserFacade(
        IUnitOfWorkFactory unitOfWorkFactory,
        IUserModelMapper userModelMapper
        ) : base(unitOfWorkFactory, userModelMapper) =>
        _userModelMapper = userModelMapper;
    protected override string IncludesNavigationPathDetail =>
        $"{nameof(UserEntity.Projects)}.{nameof(ProjectAmountEntity.Project)}";
}
