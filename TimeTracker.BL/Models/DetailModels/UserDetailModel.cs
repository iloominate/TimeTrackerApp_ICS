using System;

namespace TimeTracker.BL.Models.DetailModels;

public record UserDetailModel : ModelBase
{
    public required string Name { get; set; }
    public required string Surname { get; set; }
    public string? PhotoUrl { get; set; }

    public static UserDetailModel Empty => new()
    {
        Id = Guid.Empty,
        Name = string.Empty,
        Surname = string.Empty,
        PhotoUrl = string.Empty,
    };
}