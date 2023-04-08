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
}