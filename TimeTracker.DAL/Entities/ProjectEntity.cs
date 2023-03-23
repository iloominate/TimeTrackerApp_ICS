using System.Diagnostics;

namespace TimeTracker.DAL.Entities;

public record ProjectEntity : IEntity
{
    public required Guid Id { get; set; }
    public required string Name { get; set; }
    
    public ICollection<ProjectAmountEntity> Users { get; set; } = new List<ProjectAmountEntity>();

    public required UserEntity Creator { get; set; }

}