using System;

using System.Diagnostics;

namespace TimeTracker.DAL.Entities
{
    public record UserEntity : IEntity
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public required string Surname { get; set; }
        public string? PhotoUrl { get; set; }

        public ICollection<ActivityEntity> Activities { get; set; } = new List<ActivityEntity>();
        public ICollection<ProjectEntity> Projects { get; set; } = new List<ProjectEntity>();
    }
}