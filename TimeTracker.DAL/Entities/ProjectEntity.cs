using System.Diagnostics;

namespace TimeTracker.DAL.Entities;

public class ProjectEntity : IEntity
{
    public Guid ID { get; set; }
    public string Name { get; set; }
    public ICollection<ActivityEntity> Activities { get; set; }
    public ICollection<UserEntity> Users { get; set; }

}