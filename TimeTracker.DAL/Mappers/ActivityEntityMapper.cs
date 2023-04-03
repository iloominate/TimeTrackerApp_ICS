using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeTracker.DAL.Entities;

namespace TimeTracker.DAL.Mappers;

public class ActivityEntityMapper : IEntityMapper<ActivityEntity>
{
    public void MapToExistingEntity(ActivityEntity existingEntity, ActivityEntity newEntity)
    {
        newEntity.ProjectId = existingEntity.ProjectId;
        newEntity.UserId = existingEntity.UserId;
        newEntity.Type = existingEntity.Type;
        newEntity.Start = existingEntity.Start;
        newEntity.End = existingEntity.End;
        newEntity.Description = existingEntity.Description;

    }
}
