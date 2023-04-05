using System;

namespace TimeTracker.BL.Models.ListModels;

public record ProjectListModel : ModelBase
{
    public required string Name { get; set; }
    
    public required Guid CreatorId { get; set; }
    
    public static ProjectListModel Empty => new()
    {
        Id = Guid.Empty,
        Name = string.Empty,
        
        CreatorId = Guid.Empty,
    };
}