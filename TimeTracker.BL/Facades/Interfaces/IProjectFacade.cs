﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeTracker.BL.Models.DetailModels;
using TimeTracker.BL.Models.ListModels;
using TimeTracker.DAL.Entities;

namespace TimeTracker.BL.Facades.Interfaces;

public interface IProjectFacade : IFacade<ProjectEntity, ProjectListModel, ProjectDetailModel>
{
}
