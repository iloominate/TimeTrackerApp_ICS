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

    public override async Task<ProjectAmountDetailModel> SaveAsync(ProjectAmountDetailModel model)
    {
        var entity = ModelMapper.MapToEntity(model);

        //var newModel = 
        //if (await GetAsync(entity.Id) is not null)
        //{
        //    throw new InvalidOperationException("This relationship already exist");
        //}

        return await base.SaveAsync(model);
    }


    //private Task<ProjectAmountDetailModel> GetByProject

}
