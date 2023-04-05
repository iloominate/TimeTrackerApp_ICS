using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeTracker.DAL.Entities;
using TimeTracker.BL.Models;
using TimeTracker.BL.Models.DetailModels;
using TimeTracker.BL.Models.ListModels;

namespace TimeTracker.BL.Mappers;

public interface IActivityModelMapper 
    : IModelMapper <ActivityEntity, ActivityListModel, ActivityDetailModel>
{
}
