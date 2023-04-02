using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeTracker.DAL.Entities;

namespace TimeTracker.DAL.Mappers;

public class ProjectEntityMapper : IEntityMapper<ProjectEntity>
{
    public void MapToExistingEntity(ProjectEntity existingEntity, ProjectEntity newEntity)
    {
        newEntity.Name = existingEntity.Name;
        newEntity.Creator = existingEntity.Creator;
        newEntity.Id = existingEntity.Id;
        newEntity.Users = existingEntity.Users;
        newEntity.CreatorId = existingEntity.CreatorId;
        newEntity.Activities = existingEntity.Activities;
    }
}
