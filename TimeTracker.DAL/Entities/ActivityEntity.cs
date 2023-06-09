﻿using System;
using TimeTracker.Common.Enums;

namespace TimeTracker.DAL.Entities;

public record ActivityEntity : IEntity
{
    public required Guid Id { get; set; }
    public required string Name { get; set; }
    public required DateTime Start { get; set; }
    public required DateTime End { get; set; }
    public required ActivityType Type { get; set; }
    public string? Description { get; set; }

    public required Guid UserId { get; set; }
    public UserEntity? User { get; init; }
    public required Guid ProjectId { get; set; }
    public ProjectEntity? Project { get; init; }
}