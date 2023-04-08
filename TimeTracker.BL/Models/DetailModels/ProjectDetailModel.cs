using System;
using System.Collections.ObjectModel;
using TimeTracker.BL.Models.ListModels;
using TimeTracker.DAL.Entities;

namespace TimeTracker.BL.Models.DetailModels;

public record ProjectDetailModel : ModelBase
{
    public required string Name { get; set; }
    
    public required Guid CreatorId { get; set; }

    public ObservableCollection<ActivityListModel> Activities = new();

    public ObservableCollection<ProjectAmountListModel> Users = new();

    public static ProjectDetailModel Empty => new()
    {
        Id = Guid.Empty,
        Name = string.Empty,
        
        CreatorId = Guid.Empty,
    };
}