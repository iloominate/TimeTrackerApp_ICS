using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeTracker.DAL.Entities;

namespace TimeTracker.DAL.Mappers;

public class ProjectAmountEntityMapper : IEntityMapper<ProjectAmountEntity>
{
    public void MapToExistingEntity(ProjectAmountEntity existingEntity,
        ProjectAmountEntity newEntity)
    {
        newEntity.ProjectId = existingEntity.ProjectId; 
        newEntity.UserId = existingEntity.UserId;
    }
}
