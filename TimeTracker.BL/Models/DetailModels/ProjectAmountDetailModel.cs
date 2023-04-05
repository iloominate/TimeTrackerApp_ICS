using System;

namespace TimeTracker.BL.Models.DetailModels;

public record ProjectAmountDetailModel : ModelBase
{
    public Guid ProjectId { get; set; }
    public Guid UserId { get; set; }
    
    public List<ProjectAmountDetailModel> Users { get; set; } = new();
    public List<ProjectAmountDetailModel> Projects { get; set; } = new();

    public static ProjectAmountDetailModel Empty => new()
    {
        Id = Guid.Empty,
        ProjectId = Guid.Empty,
        UserId = Guid.Empty,
    };
}