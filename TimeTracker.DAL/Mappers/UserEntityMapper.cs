﻿using System;
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
        newEntity.PhotoUrl = existingEntity.PhotoUrl;
        newEntity.Name = existingEntity.Name;
        newEntity.Surname = existingEntity.Surname;
    }
}
