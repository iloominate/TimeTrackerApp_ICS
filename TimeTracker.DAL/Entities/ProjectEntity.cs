using System.Diagnostics;

namespace TimeTracker.DAL.Entities;

public record ProjectEntity : IEntity
{
    public required Guid Id { get; set; }
    public required string Name { get; set; }
    
    public ICollection<ProjectAmountEntity> Users { get; init; } = new List<ProjectAmountEntity>();

    public ICollection<ActivityEntity> Activities { get; init; } = new List<ActivityEntity>();

    public required Guid CreatorId { get; set; }
    public UserEntity? Creator { get; init; }

}