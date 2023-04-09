using System;
using TimeTracker.Common.Enums;
using TimeTracker.DAL.Entities;

namespace TimeTracker.BL.Models.ListModels;

public record ActivityListModel : ModelBase
{
    public required DateTime Start { get; set; }
    public required DateTime End { get; set; }
    public required ActivityType Type { get; set; }

    public required Guid UserId { get; set; }
    public required Guid ProjectId { get; set; }
    
    public static ActivityListModel Empty => new()
    {
        Id = Guid.Empty,
        Start = DateTime.MinValue,
        End = DateTime.MinValue,
        Type = ActivityType.Other,
        
        UserId = Guid.Empty,
        ProjectId = Guid.Empty
    };
}