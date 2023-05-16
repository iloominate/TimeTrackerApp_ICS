using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeTracker.DAL.Entities;

namespace TimeTracker.DAL.Mappers; 

public class UserEntityMapper : IEntityMapper<UserEntity>
{
    public void MapToExistingEntity(UserEntity existingEntity, UserEntity newEntity)
    {
        existingEntity.PhotoUrl = newEntity.PhotoUrl;
        existingEntity.Name = newEntity.Name;
        existingEntity.Surname = newEntity.Surname;
    }
}
