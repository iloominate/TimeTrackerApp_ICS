using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using TimeTracker.DAL.Entities;

namespace TimeTracker.DAL.Mappers;

public interface IEntityMapper <in TEntity> 
    where TEntity : IEntity
{
    void MapToExistingEntity(TEntity existingEntity, TEntity newEntity);
}
