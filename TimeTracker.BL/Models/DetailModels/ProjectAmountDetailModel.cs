using System;
using System.Collections.ObjectModel;

namespace TimeTracker.BL.Models.DetailModels;

public record ProjectAmountDetailModel : ModelBase
{
    public Guid ProjectId { get; set; }
    public Guid UserId { get; set; }

    public static ProjectAmountDetailModel Empty => new()
    {
        Id = Guid.Empty,
        ProjectId = Guid.Empty,
        UserId = Guid.Empty,
    };
}