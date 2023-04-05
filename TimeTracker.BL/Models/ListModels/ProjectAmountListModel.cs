using System;

namespace TimeTracker.BL.Models.ListModels;

public record ProjectAmountListModel : ModelBase
{
    public Guid ProjectId { get; set; }
    public Guid UserId { get; set; }
    
    public static ProjectAmountListModel Empty => new()
    {
        Id = Guid.Empty,
        ProjectId = Guid.Empty,
        UserId = Guid.Empty,
    };

}