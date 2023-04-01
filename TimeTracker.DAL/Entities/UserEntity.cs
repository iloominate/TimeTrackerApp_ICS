using System;
using System.Collections;
using System.Diagnostics;

namespace TimeTracker.DAL.Entities
{
    public record UserEntity : IEntity
    {
        public required Guid Id { get; set; }
        public required string Name { get; set; }
        public required string Surname { get; set; }
        public string? PhotoUrl { get; set; }

        public ICollection<ProjectAmountEntity> Projects { get; init; } = new List<ProjectAmountEntity>();

        public ICollection<ProjectEntity> CreatedProjects { get; init; } = new List<ProjectEntity>();

        public ICollection<ActivityEntity> Activities { get; init; } = new List<ActivityEntity>();
    }
}