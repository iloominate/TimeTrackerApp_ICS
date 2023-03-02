using System.Diagnostics;

namespace TimeTracker.DAL.Entities;

public record ProjectEntity : IEntity
{
    public Guid Id { get; set; }
    public required string Name { get; set; }

    public ICollection<ActivityEntity> Activities { get; set; } = new List<ActivityEntity>();
    public ICollection<UserEntity> Users { get; set; } = new List<UserEntity>();

}