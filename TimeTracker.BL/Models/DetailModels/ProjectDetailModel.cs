using System;
using TimeTracker.DAL.Entities;

namespace TimeTracker.BL.Models.DetailModels;

public record ProjectDetailModel : ModelBase
{
    public required string Name { get; set; }
    
    public required Guid CreatorId { get; set; }

    public static ProjectDetailModel Empty => new()
    {
        Id = Guid.Empty,
        Name = string.Empty,
        
        CreatorId = Guid.Empty,
    };
}