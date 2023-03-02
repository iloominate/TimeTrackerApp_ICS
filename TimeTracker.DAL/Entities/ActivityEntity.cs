using System;

namespace TimeTracker.DAL.Entities;

public record ActivityEntity : IEntity
{
    public Guid Id { get; set; }
    public required DateTime Start { get; set; }
    public required DateTime End { get; set; }
    public required string ActivityType { get; set; }
    public string Description { get; set; }

    public Guid UserId { get; set; }
    public UserEntity? User { get; set; }
    public Guid ProjectId { get; set; }
    public ProjectEntity? Project { get; set; }
}