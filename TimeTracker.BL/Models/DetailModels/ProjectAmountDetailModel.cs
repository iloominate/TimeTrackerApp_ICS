using System;
using System.Collections.ObjectModel;

namespace TimeTracker.BL.Models.DetailModels;

public record ProjectAmountDetailModel : ModelBase
{
    public Guid ProjectId { get; set; }
    public Guid UserId { get; set; }
    
    public ObservableCollection<ProjectAmountDetailModel> Users { get; set; } = new();
    public ObservableCollection<ProjectAmountDetailModel> Projects { get; set; } = new();

    public static ProjectAmountDetailModel Empty => new()
    {
        Id = Guid.Empty,
        ProjectId = Guid.Empty,
        UserId = Guid.Empty,
    };
}