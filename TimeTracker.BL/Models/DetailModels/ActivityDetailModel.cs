using System;
using TimeTracker.Common.Enums;

namespace TimeTracker.BL.Models.DetailModels;

public record ActivityDetailModel : ModelBase
{
    public required DateTime Start { get; set; }
    public required DateTime End { get; set; }
    public required ActivityType Type { get; set; }
    public string Description { get; set; }
    
    public required Guid UserId { get; set; }
    public required Guid ProjectId { get; set; }
    
    public List<UserDetailModel> Users { get; set; } = new();
    public List<ProjectDetailModel> Projects { get; set; } = new();
    
    public static ActivityDetailModel Empty => new()
    {
        Id = Guid.Empty,
        Start = DateTime.MinValue,
        End = DateTime.MinValue,
        Type = ActivityType.Other,
        Description = string.Empty,
        
        UserId = Guid.Empty,
        ProjectId = Guid.Empty,
    };
}