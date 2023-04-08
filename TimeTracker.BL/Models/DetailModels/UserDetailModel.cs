using System;
using System.Collections.ObjectModel;
using TimeTracker.BL.Models.ListModels;
using TimeTracker.DAL.Entities;

namespace TimeTracker.BL.Models.DetailModels;

public record UserDetailModel : ModelBase
{
    public required string Name { get; set; }
    public required string Surname { get; set; }
    public string? PhotoUrl { get; set; }

    public ObservableCollection<ProjectAmountListModel> Projects { get; init; } = new();
    public ObservableCollection<ProjectListModel> ProjectsCreared { get; init; } = new();
    public ObservableCollection<ActivityListModel> Activities { get; init; } = new();


    public static UserDetailModel Empty => new()
    {
        Id = Guid.Empty,
        Name = string.Empty,
        Surname = string.Empty,
        PhotoUrl = string.Empty,
    };
}