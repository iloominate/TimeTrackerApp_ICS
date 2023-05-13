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
using TimeTracker.DAL.Repository;
using TimeTracker.DAL.UnitOfWork;

namespace TimeTracker.BL.Facades;

public class ProjectAmountFacade : FacadeBase<ProjectAmountEntity, ProjectAmountListModel, 
    ProjectAmountDetailModel, ProjectAmountEntityMapper>, IProjectAmountFacade
{
    private readonly IProjectAmountModelMapper _projectAmountModelMapper;
    public ProjectAmountFacade(
        IUnitOfWorkFactory unitOfWorkFactory,
        IProjectAmountModelMapper projectAmountModelMapper)
        : base(unitOfWorkFactory, projectAmountModelMapper) =>
        _projectAmountModelMapper = projectAmountModelMapper;

    public async Task SaveAsync(ProjectAmountDetailModel model)
    {
        ProjectAmountEntity entity = _projectAmountModelMapper.MapToEntity(model);

        await using IUnitOfWork uow = UnitOfWorkFactory.Create();
        IRepository<ProjectAmountEntity> repository =
            uow.GetRepository<ProjectAmountEntity, ProjectAmountEntityMapper>();

        await repository.InsertAsync(entity);
        await uow.CommitAsync();
    }

}
