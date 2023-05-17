using Microsoft.EntityFrameworkCore;
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

public class ProjectFacade : FacadeBase<ProjectEntity, ProjectListModel,
    ProjectDetailModel, ProjectEntityMapper>, IProjectFacade
{
    private readonly IProjectModelMapper _projectModelMapper;

    public ProjectFacade(
        IUnitOfWorkFactory unitOfWorkFactory,
        IProjectModelMapper projectModelMapper
    ) : base(unitOfWorkFactory, projectModelMapper) =>
        _projectModelMapper = projectModelMapper;

    protected override string IncludesNavigationPathDetail =>
        $"{nameof(ProjectEntity.Users)}.{nameof(ProjectAmountEntity.User)}";

    public override async Task<ProjectDetailModel?> GetAsync(Guid id)
    {
        await using IUnitOfWork uow = UnitOfWorkFactory.Create();

        IQueryable<ProjectEntity> query = uow.GetRepository<ProjectEntity, ProjectEntityMapper>().Get();

        if (string.IsNullOrWhiteSpace(IncludesNavigationPathDetail) is false)
        {
            query = query.Include(IncludesNavigationPathDetail);
        }

        ProjectEntity? entity = await query.Include(e => e.Activities).SingleOrDefaultAsync(e => e.Id == id);

        return entity is null
            ? null
            : ModelMapper.MapToDetailModel(entity);
    }
}